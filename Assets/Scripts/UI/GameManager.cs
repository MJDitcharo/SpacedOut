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
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");

        //sort the array of checkpoints by name
        for(int i = 0; i < checkpoints.Length; i++)
        {
            for(int j = i; j < checkpoints.Length; j++)
            {
                if(checkpoints[i].name.CompareTo(checkpoints[j].name) > 0)
                {
                    GameObject temp = checkpoints[i];
                    checkpoints[i] = checkpoints[j];
                    checkpoints[j] = temp;
                }
            }
        }

        //ui stuff
        pmenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>();
        ammoCount = GameObject.FindGameObjectWithTag("AmmoCount").GetComponent<ItemCount>();
        boardWipeCount = GameObject.FindGameObjectWithTag("BoardWipeCount").GetComponent<ItemCount>();
        grenadeCount = GameObject.FindGameObjectWithTag("GrenadeCount").GetComponent<ItemCount>();
        skrapCount = GameObject.Find("Skrap Count").GetComponent<ItemCount>();
        prompt = GameObject.Find("UIPrompt").GetComponent<UIPrompt>();
        chestUI = GameObject.Find("Chest UI").GetComponent<UIChest>();
        shopUI = GameObject.Find("Shop UI").GetComponent<UIStore>();

        checkpointIndex = PlayerPrefs.GetInt("Checkpoint Index");
        skrapCount.SetQuantity(PlayerPrefs.GetInt("Skrap Count"));
        player.GetComponent<PlayerMovement>().WarpToPosition(checkpoints[checkpointIndex].GetComponent<RoomManager>().spawnPoint.position);
        
        //NOTE
        //THIS IS A TEMPERARY FIX
        if (checkpointIndex == 0)
        {
            PlayerPrefs.SetInt("Pistol Ammo", 45);
            PlayerPrefs.SetInt("Shotgun Ammo", 15);
            PlayerPrefs.SetInt("Rifle Ammo", 100);
            PlayerPrefs.SetInt("Heavy Ammo", 7);
        }
    }

    private void Start()
    {
        for (int i = 0; i < WeaponHolder.instance.transform.childCount; i++)
        {
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Pistol>() != null && PlayerPrefs.HasKey("Pistol Ammo"))
            {
                //Debug.Log("loaded the pistol");
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

    private void LateUpdate()
    {

    }

    public void Respawn()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
        enemyCount = 0;

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i]);
        }
        skrapCount.SetQuantity(PlayerPrefs.GetInt("Skrap Count"));
        player.GetComponent<PlayerMovement>().WarpToPosition(checkpoints[checkpointIndex].GetComponent<RoomManager>().spawnPoint.position);
        player.GetComponent<playerHealth>().currHealth = player.GetComponent<playerHealth>().maxHealth;
        healthBar.SetHealth(1);

        for(int i = 0; i < WeaponHolder.instance.transform.childCount; i++)
        {
            WeaponBase currentWeapon = WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>();
            currentWeapon.ammoCount = currentWeapon.maxAmmo;
            currentWeapon.UpdateVisual();
        }

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

        for(int i = 0; i < WeaponHolder.instance.transform.childCount; i++)
        {
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Pistol>() != null)
            {
                PlayerPrefs.SetInt("Pistol Ammo", WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().ammoCount);
                Debug.Log("saved the pistol");
            }
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Shotgun>() != null)
                PlayerPrefs.SetInt("Shotgun Ammo", WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().ammoCount);
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Rifle>() != null)
                PlayerPrefs.SetInt("Rifle Ammo", WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().ammoCount);
            if (WeaponHolder.instance.transform.GetChild(i).GetComponent<Heavy>() != null)
                PlayerPrefs.SetInt("Heavy Ammo", WeaponHolder.instance.transform.GetChild(i).GetComponent<WeaponBase>().ammoCount);
        }
    }
}
