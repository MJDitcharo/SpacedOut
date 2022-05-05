using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public playerMovement movement;
    public GameObject pauseMenu;
    public int enemyCount;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
