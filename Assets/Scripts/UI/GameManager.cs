using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public PlayerMovement movement;
    public GameObject pauseMenu;
    public int enemyCount;
    public HealthBar healthBar;

    public int sceneIndex;
    public int checkpointIndex = 0;
    public GameObject[] checkpoints;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<PlayerMovement>();
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        //checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
    }

    private void Update()
    {

    }

    public void Respawn()
    {
        player.transform.position = checkpoints[checkpointIndex].transform.position;
        player.GetComponent<playerHealth>().currHealth = player.GetComponent<playerHealth>().maxHealth;

        for (int i  = checkpoints.Length - 1; i >= 0; i--)
        {
            if (i == checkpointIndex)
            {
                checkpoints[i].GetComponent<RoomManager>().EndLockDown();
                continue;
            }
            checkpoints[i].GetComponent<RoomManager>().LockDownRoom();
            checkpoints[i].GetComponent<RoomManager>().collider.enabled = true;
        }
    }
    
    public void SaveGame()
    {

    }
}
