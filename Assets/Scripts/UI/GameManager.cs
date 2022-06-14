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
        ammoCount = GameObject.FindGameObjectWithTag("AmmoCount").GetComponent<ItemCount>();
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

        AudioManager.Instance.PlaySFX("GameMusic");
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Max Player Health") == 0)
        {
            PlayerPrefs.SetInt("Max Player Health", 100);
        }
        SetFightingCursor();
        LoadGame();
    }

    public void Respawn()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
        enemyCount = 0;

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
        checkpoints[checkpointIndex + 1].GetComponent<RoomManager>().collider.enabled = true;
        checkpoints[checkpointIndex + 1].GetComponent<RoomManager>().doorEnter.SetActive(false);

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
            }
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Shotgun>() != null)
                PlayerPrefs.SetInt("Shotgun Ammo", WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().ammoCount);
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Rifle>() != null)
                PlayerPrefs.SetInt("Rifle Ammo", WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().ammoCount);
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Heavy>() != null)
                PlayerPrefs.SetInt("Heavy Ammo", WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().ammoCount);
        }

       WeaponHolder.instance.SaveLoadout();
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
        for (int i = 0; i < WeaponHolder.instance.unlockedWeapons.Count; i++)
        {
            PlayerPrefs.SetString("Weapon " + i, WeaponHolder.instance.unlockedWeapons[i]);
        }
        PlayerPrefs.SetInt("Child Count", WeaponHolder.instance.currentChildCount);

        //save the store datat
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

    }

    public void LoadGame()
    {
        checkpointIndex = PlayerPrefs.GetInt("Checkpoint Index");
        skrapCount.SetQuantity(PlayerPrefs.GetInt("Skrap Count"));
        movement.WarpToPosition(checkpoints[checkpointIndex].GetComponent<RoomManager>().spawnPoint.position);
        playerHealth.currHealth = PlayerPrefs.GetInt("Player Health");
        playerHealth.maxHealth = PlayerPrefs.GetInt("Max Player Health");
        healthBar.SetHealth((float)playerHealth.currHealth / playerHealth.maxHealth);
        boardWipeCount.SetQuantity(PlayerPrefs.GetInt("Board Wipes"));

        WeaponHolder.instance.currentChildCount = PlayerPrefs.GetInt("Child Count");
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

        //tiers for pages
        for (int i = 0; i < WeaponHolder.instance.transform.childCount; i++)
        {
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Pistol>() != null && PlayerPrefs.HasKey("Pistol Ammo"))
            {
                WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().ammoCount = PlayerPrefs.GetInt("Pistol Ammo");

            }
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Shotgun>() != null && PlayerPrefs.HasKey("Shotgun Ammo"))
                WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().ammoCount = PlayerPrefs.GetInt("Shotgun Ammo");
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Rifle>() != null && PlayerPrefs.HasKey("Rifle Ammo"))
                WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().ammoCount = PlayerPrefs.GetInt("Rifle Ammo");
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Heavy>() != null && PlayerPrefs.HasKey("Heavy Ammo"))
                WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().ammoCount = PlayerPrefs.GetInt("Heavy Ammo");
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
