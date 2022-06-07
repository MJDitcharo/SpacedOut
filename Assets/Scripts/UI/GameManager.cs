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
    public bool menuIsActive = false;

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
        bossHealthBar.SetActive(false);
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
        boardWipeCount = GameObject.FindGameObjectWithTag("BoardWipeCount").GetComponent<ItemCount>();
        grenadeCount = GameObject.FindGameObjectWithTag("GrenadeCount").GetComponent<ItemCount>();
        skrapCount = GameObject.Find("Skrap Count").GetComponent<ItemCount>();
        prompt = GameObject.Find("UIPrompt").GetComponent<UIPrompt>();
        chestUI = GameObject.Find("Chest UI").GetComponent<UIChest>();
        shopUI = GameObject.Find("Shop UI").GetComponent<UIStore>();

        AudioManager.Instance.PlaySFX("GameMusic");
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Max Player Health") == 0)
        {
            PlayerPrefs.SetInt("Max Player Health", 100);
        }
        LoadGame();
    }

    private void LateUpdate()
    {

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

        LoadGame();

        Debug.Log("End lockdown");
        checkpoints[checkpointIndex].GetComponent<RoomManager>().EndLockDown();
        checkpoints[checkpointIndex + 1].GetComponent<RoomManager>().collider.enabled = true;
        checkpoints[checkpointIndex + 1].GetComponent<RoomManager>().doorEnter.SetActive(false);
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("Scene Index", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("Checkpoint Index", checkpointIndex);
        PlayerPrefs.SetInt("Skrap Count", skrapCount.GetQuantity());
        PlayerPrefs.SetInt("Player Health", playerHealth.currHealth);
        PlayerPrefs.SetInt("Max Player Health", playerHealth.maxHealth);
        PlayerPrefs.SetInt("Board Wipes", boardWipeCount.GetQuantity());

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

        for(int i = 0; i < WeaponHolder.instance.unlockedWeapons.Count; i++)
        {
            PlayerPrefs.SetString("Weapon " + i, WeaponHolder.instance.unlockedWeapons[i]);
        }
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

        //NOTE
        //THIS IS A TEMPERARY FIX
        if (checkpointIndex == 0)
        {
            PlayerPrefs.SetInt("Pistol Ammo", 45);
            PlayerPrefs.SetInt("Shotgun Ammo", 15);
            PlayerPrefs.SetInt("Rifle Ammo", 100);
            PlayerPrefs.SetInt("Heavy Ammo", 7);
        }

        WeaponHolder.instance.unlockedWeapons.Clear();
        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.HasKey("Weapon " + i))
            {
                string name = PlayerPrefs.GetString("Weapon " + i);
                Debug.Log(name);
                WeaponHolder.instance.AddToUnlockedItems(name);
                //WeaponHolder.instance.ArrangeHierarchy(name, UIStoreButtons.purchaseIndex++);
                //WeaponHolder.instance.currentChildCount++;
            }
        }

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
}
