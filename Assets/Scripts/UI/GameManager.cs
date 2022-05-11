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
    }
    
    public void SaveGame()
    {

    }
}
