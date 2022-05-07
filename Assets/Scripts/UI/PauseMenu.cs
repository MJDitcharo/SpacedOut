using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //put gameIsPaused into gamemanager 

    //false by default
    [SerializeField]
    GameObject pauseMenuVisual;
    bool gameIsPaused = false;
    private void Update()
    {
        PauseGame();
    }

    public void PauseGame()
    {
        if (Input.GetButtonUp("Cancel"))
        {
            gameIsPaused = !gameIsPaused;
            Debug.Log("Paused?" + gameIsPaused);
            pauseMenuVisual.SetActive(gameIsPaused);

            //freeze time and player
            if (gameIsPaused)
            {
                Time.timeScale = 0;
                GameManager.instance.movement.enabled = false; 
            }
            else
            {
                Time.timeScale = 1;
                GameManager.instance.movement.enabled = true;
            }
        }
    }
}
