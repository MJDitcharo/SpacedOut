using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
    public AmmoCount ammoCount;
    public BoardWipeCount boardWipeCount;
    public GrenadeCount grenadeCount;




    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<PlayerMovement>();
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        //checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        pmenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenu>();
        ammoCount = GameObject.FindGameObjectWithTag("AmmoCount").GetComponent<AmmoCount>();
        boardWipeCount = GameObject.FindGameObjectWithTag("BoardWipeCount").GetComponent<BoardWipeCount>();
        grenadeCount = GameObject.FindGameObjectWithTag("GrenadeCount").GetComponent<GrenadeCount>();

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

        player.GetComponent<PlayerMovement>().WarpToPosition(checkpoints[checkpointIndex].transform.position);
        player.GetComponent<playerHealth>().currHealth = player.GetComponent<playerHealth>().maxHealth;
        healthBar.SetHealth(1);

        checkpoints[checkpointIndex].GetComponent<RoomManager>().EndLockDown();
        checkpoints[checkpointIndex + 1].GetComponent<RoomManager>().collider.enabled = true;
        checkpoints[checkpointIndex + 1].GetComponent<RoomManager>().doorEnter.SetActive(false);
    }
    
    public void SaveGame()
    {

    }
}
