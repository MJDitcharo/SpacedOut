using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<GameObject> bullets;
    public GameObject player;
    public PlayerMovement movement;
    public int enemyCount;
    public PauseMenu pmenu;
    public int sceneIndex;
    public int checkpointIndex = 0;
    public GameObject[] checkpoints;

    //UI
    public HealthBar healthBar;
    public GameObject bossHealthBar;
    public GameObject pauseMenu;
    public ItemCount ammoCount;
    public ItemCount boardWipeCount;
    public ItemCount grenadeCount;
    public ItemCount skrapCount;
    public playerHealth playerHealth;
    public UIPrompt prompt;
    public UIChest chestUI;
    public UIStore shopUI;
    public bool shopIsActive = false;
    public GameObject gameOverScreen;
    public ShopFunctions shopFunctions;

    public Texture2D fightingCursor;
    public Texture2D normalCursor;

    public GameObject gunImages;

    public bool Lockdown { get; set; } = false;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<playerHealth>();
        movement = player.GetComponent<PlayerMovement>();
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        bossHealthBar = GameObject.FindGameObjectWithTag("Boss Health Bar");
        bossHealthBar.transform.parent.parent.gameObject.SetActive(false);
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");

        //sort the array of checkpoints by name
        for (int i = 0; i < checkpoints.Length; i++)
        {
            for (int j = i; j < checkpoints.Length; j++)
            {
                if (checkpoints[i].name.CompareTo(checkpoints[j].name) > 0)
                {
                    GameObject temp = checkpoints[i];
                    checkpoints[i] = checkpoints[j];
                    checkpoints[j] = temp;
                }
            }
        }

        //ui stuff
        //ammoCount = GameObject.FindGameObjectWithTag("AmmoCount").GetComponent<ItemCount>();
        pmenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>();
        //boardWipeCount = GameObject.FindGameObjectWithTag("BoardWipeCount").GetComponent<ItemCount>();
        //grenadeCount = GameObject.FindGameObjectWithTag("GrenadeCount").GetComponent<ItemCount>();
        skrapCount = GameObject.Find("Skrap Count").GetComponent<ItemCount>();
        prompt = GameObject.Find("UIPrompt").GetComponent<UIPrompt>();
        chestUI = GameObject.Find("Chest UI").GetComponent<UIChest>();
        shopUI = GameObject.Find("Shop UI").GetComponent<UIStore>();
        gameOverScreen = GameObject.FindGameObjectWithTag("GameOver");
        gunImages = GameObject.FindGameObjectWithTag("Gun Images");
        gameOverScreen.SetActive(false);
        shopFunctions = GameObject.FindGameObjectWithTag("Shop").GetComponent<ShopFunctions>();

        
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Max Player Health") == 0)
        {
            PlayerPrefs.SetInt("Max Player Health", 100);
        }
        SetFightingCursor();
        Respawn();
        AudioManager.Instance.PlaySFX("GameMusic");
    }

    public void Respawn()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
        enemyCount = 0;

        GameObject[] fire = GameObject.FindGameObjectsWithTag("BossFire");
        for (int i = 0; i < fire.Length; i++)
        {
            Destroy(fire[i]);
        }

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i]);
        }

        GameObject[] particles = GameObject.FindGameObjectsWithTag("Death Particles");
        for (int i = 0; i < particles.Length; i++)
        {
            Debug.Log(i);
            Destroy(particles[i]);
        }

        LoadGame();

        Debug.Log("End lockdown");
        checkpoints[checkpointIndex].GetComponent<RoomManager>().EndLockDown();
        checkpoints[checkpointIndex].GetComponent<RoomManager>().collider.enabled = false;
        checkpoints[checkpointIndex].GetComponent<RoomManager>().doorEnter.SetActive(true);
        checkpoints[checkpointIndex + 1].GetComponent<RoomManager>().collider.enabled = true;
        checkpoints[checkpointIndex + 1].GetComponent<RoomManager>().doorEnter.SetActive(false);
        ammoCount.SetQuantity(WeaponHolder.instance.transform.GetChild(0).GetComponent<WeaponBase>().ammoCount);
        ammoCount.UpdateVisual();

        EnemyFlashRed flasher = playerHealth.GetComponent<EnemyFlashRed>();
        for (int i = 0; i < flasher.normalColor.Length; i++)
        {
            flasher.rend.materials[i].color = flasher.normalColor[i];
        }
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("Scene Index", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("Checkpoint Index", checkpointIndex);
        PlayerPrefs.SetInt("Skrap Count", skrapCount.GetQuantity());
        PlayerPrefs.SetInt("Player Health", playerHealth.currHealth);
        PlayerPrefs.SetInt("Max Player Health", playerHealth.maxHealth);
        //PlayerPrefs.SetInt("Board Wipes", boardWipeCount.GetQuantity());

        for (int i = 0; i < WeaponHolder.instance.transform.childCount; i++)
        {
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Pistol>() != null)
            {
                PlayerPrefs.SetInt("Pistol Ammo", WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().ammoCount);
                PlayerPrefs.SetFloat("Pistol Fire Rate", WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().GetFireRateMultiplier());
                PlayerPrefs.SetFloat("Pistol Damage", WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().GetDamageMultiplier());
            }
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Shotgun>() != null)
            {
                PlayerPrefs.SetInt("Shotgun Ammo", WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().ammoCount);
                PlayerPrefs.SetFloat("Shotgun Fire Rate", WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().GetFireRateMultiplier());
                PlayerPrefs.SetFloat("Shotgun Damage", WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().GetDamageMultiplier());
            }
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Rifle>() != null)
            {
                PlayerPrefs.SetInt("Rifle Ammo", WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().ammoCount);
                PlayerPrefs.SetFloat("Rifle Fire Rate", WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().GetFireRateMultiplier());
                PlayerPrefs.SetFloat("Rifle Damage", WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().GetDamageMultiplier());
            }
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Heavy>() != null)
            {
                PlayerPrefs.SetInt("Heavy Ammo", WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().ammoCount);
                PlayerPrefs.SetFloat("Heavy Fire Rate", WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().GetFireRateMultiplier());
                PlayerPrefs.SetFloat("Heavy Damage", WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().GetDamageMultiplier());
            }
        }

        //WeaponHolder.instance.SaveLoadout();
        //save the tier index of each page for the store
        //if (!firstSave)
        //{
        //    if (PistolPage.instance != null)
        //        PlayerPrefs.SetInt("Pistol Page", PistolPage.instance.GetCurrentTier());
        //    if (ShotgunPage.instance != null)
        //        PlayerPrefs.SetInt("Shogun Page", ShotgunPage.instance.GetCurrentTier());
        //    if (RiflePage.instance!= null)
        //        PlayerPrefs.SetInt("Rifle Page", RiflePage.instance.GetCurrentTier());
        //    if (HeavyPage.instance != null)
        //        PlayerPrefs.SetInt("Heavy Page", HeavyPage.instance.GetCurrentTier());
        //}
        
        
        /*for (int i = 0; i < WeaponHolder.instance.unlockedWeapons.Count; i++)
        {
            PlayerPrefs.SetString("Weapon " + i, WeaponHolder.instance.unlockedWeapons[i]);
        }
        PlayerPrefs.SetInt("Child Count", WeaponHolder.instance.currentChildCount);*/

        //save the store datat
        /*
        if (PistolPage.instance != null)
            PlayerPrefs.SetInt("PistolPage", PistolPage.instance.GetCurrentTier());
        if (ShotgunPage.instance != null)
            PlayerPrefs.SetInt("ShotgunPage", ShotgunPage.instance.GetCurrentTier());
        if (RiflePage.instance != null)
            PlayerPrefs.SetInt("RiflePage", RiflePage.instance.GetCurrentTier());
        if (HeavyPage.instance != null)
            PlayerPrefs.SetInt("HeavyPage", HeavyPage.instance.GetCurrentTier());

        //firerate and damage for each weapon
        PlayerPrefs.SetFloat("Pistol Damage", WeaponHolder.instance.GetWeaponDamage(WeaponBase.WeaponID.Pistol));
        PlayerPrefs.SetFloat("Pistol Fire Rate", WeaponHolder.instance.GetWeaponFireRate(WeaponBase.WeaponID.Pistol));

        PlayerPrefs.SetFloat("Shotgun Damage", WeaponHolder.instance.GetWeaponDamage(WeaponBase.WeaponID.Shotgun));
        PlayerPrefs.SetFloat("Shotgun Fire Rate", WeaponHolder.instance.GetWeaponFireRate(WeaponBase.WeaponID.Shotgun));

        PlayerPrefs.SetFloat("Rifle Damage", WeaponHolder.instance.GetWeaponDamage(WeaponBase.WeaponID.Rifle));
        PlayerPrefs.SetFloat("Rifle Fire Rate", WeaponHolder.instance.GetWeaponFireRate(WeaponBase.WeaponID.Rifle));

        PlayerPrefs.SetFloat("Heavy Damage", WeaponHolder.instance.GetWeaponDamage(WeaponBase.WeaponID.Heavy));
        PlayerPrefs.SetFloat("Heavy Fire Rate", WeaponHolder.instance.GetWeaponFireRate(WeaponBase.WeaponID.Heavy));
        */
    }

    public void LoadGame()
    {
        checkpointIndex = PlayerPrefs.GetInt("Checkpoint Index");
        skrapCount.SetQuantity(PlayerPrefs.GetInt("Skrap Count"));
        movement.WarpToPosition(checkpoints[checkpointIndex].GetComponent<RoomManager>().spawnPoint.position);
        playerHealth.currHealth = PlayerPrefs.GetInt("Player Health");
        playerHealth.maxHealth = PlayerPrefs.GetInt("Max Player Health");
        healthBar.SetHealth((float)playerHealth.currHealth / playerHealth.maxHealth);
        //boardWipeCount.SetQuantity(PlayerPrefs.GetInt("Board Wipes"));

        /*WeaponHolder.instance.currentChildCount = PlayerPrefs.GetInt("Child Count");
        WeaponHolder.instance.unlockedWeapons.Clear();
        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.HasKey("Weapon " + i))
            {
                string name = PlayerPrefs.GetString("Weapon " + i);
                WeaponHolder.instance.AddToUnlockedItems(name);
                WeaponHolder.instance.ArrangeHierarchy(name, i);
            }
        }
        */

        if (!PlayerPrefs.HasKey("Weapon 0"))
        {
            Debug.Log("no pistol key");
            PlayerPrefs.SetInt("Weapon 0", -1);
            PlayerPrefs.SetFloat("Pistol Damage", 1);
            PlayerPrefs.SetFloat("Pistol Fire Rate", 1);
        }
            
        if (!PlayerPrefs.HasKey("Weapon 1"))
        { 
            PlayerPrefs.SetInt("Weapon 1", -1);
            PlayerPrefs.SetFloat("Shotgun Damage", 1);
            PlayerPrefs.SetFloat("Shotgun Fire Rate", 1);
        }
            
        if (!PlayerPrefs.HasKey("Weapon 2"))
        {
            PlayerPrefs.SetInt("Weapon 2", -1);
            PlayerPrefs.SetFloat("Rifle Damage", 1);
            PlayerPrefs.SetFloat("Rifle Fire Rate", 1);
        }
            
        if (!PlayerPrefs.HasKey("Weapon 3"))
        {
            PlayerPrefs.SetInt("Weapon 3", -1);
            PlayerPrefs.SetFloat("Heavy Damage", 1);
            PlayerPrefs.SetFloat("Heavy Fire Rate", 1);
        }
            

        int weaponZero = PlayerPrefs.GetInt("Weapon 0");
        int weaponOne = PlayerPrefs.GetInt("Weapon 1");
        int weaponTwo = PlayerPrefs.GetInt("Weapon 2");
        int weaponThree = PlayerPrefs.GetInt("Weapon 3");

        if (weaponZero != -1)
            WeaponHolder.instance.AddWeapon(shopFunctions.pistolUpgrades[weaponZero]); 
        if (weaponOne != -1)
            WeaponHolder.instance.AddWeapon(shopFunctions.shotgunUpgrades[weaponOne]);
        if (weaponTwo != -1)
            WeaponHolder.instance.AddWeapon(shopFunctions.rifleUpgrades[weaponTwo]);
        if (weaponThree != -1)
            WeaponHolder.instance.AddWeapon(shopFunctions.heavyUpgrades[weaponThree]);

        for (int i = 0; i < WeaponHolder.instance.transform.childCount; i++)
        {
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Pistol>() != null && PlayerPrefs.HasKey("Pistol Ammo"))
            {
                WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().ammoCount = PlayerPrefs.GetInt("Pistol Ammo");
                WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().SetDamageMultiplier(PlayerPrefs.GetFloat("Pistol Damage"));
                WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().SetFireRateMultiplier(PlayerPrefs.GetFloat("Pistol Fire Rate"));
            }
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Shotgun>() != null && PlayerPrefs.HasKey("Shotgun Ammo"))
            {
                WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().ammoCount = PlayerPrefs.GetInt("Shotgun Ammo");
                WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().SetDamageMultiplier(PlayerPrefs.GetFloat("Shotgun Damage"));
                WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().SetFireRateMultiplier(PlayerPrefs.GetFloat("Shotgun Fire Rate"));
            }
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Rifle>() != null && PlayerPrefs.HasKey("Rifle Ammo"))
            {
                WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().ammoCount = PlayerPrefs.GetInt("Rifle Ammo");
                WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().SetDamageMultiplier(PlayerPrefs.GetFloat("Rifle Damage"));
                WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().SetFireRateMultiplier(PlayerPrefs.GetFloat("Rifle Fire Rate"));
            }
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Heavy>() != null && PlayerPrefs.HasKey("Heavy Ammo"))
            {
                WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().ammoCount = PlayerPrefs.GetInt("Heavy Ammo");
                WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().SetDamageMultiplier(PlayerPrefs.GetFloat("Heavy Damage"));
                WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().SetFireRateMultiplier(PlayerPrefs.GetFloat("Heavy Fire Rate"));
            }
        }
    }

    public void SetFightingCursor()
    {
        Cursor.SetCursor(fightingCursor, new Vector3(32,32,32), CursorMode.ForceSoftware);
    }

    public void SetNormalCursor()
    {
        Cursor.SetCursor(normalCursor, Vector3.zero, CursorMode.ForceSoftware);
    }
}
