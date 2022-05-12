using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : PopUpMenu
{
    //put gameIsPaused into gamemanager 

    //false by default
    [SerializeField]
    GameObject pauseMenuVisual;


    public bool gameIsPaused = false;

    private void Awake()
    {
        //false by default
        pauseMenuVisual.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetButtonUp("Cancel"))
        {
            if (gameIsPaused)
                UnpauseGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        //if (Input.GetButtonUp("Cancel"))
        //{
        //    gameIsPaused = !gameIsPaused;
        //    Debug.Log("Paused?" + gameIsPaused);
        //    pauseMenuVisual.SetActive(gameIsPaused);

        //    if (gameIsPaused)
        //        FreezeWorld(pauseMenuVisual);
        //    else
        //        UnfreezeWorld(pauseMenuVisual);
        //}
        gameIsPaused = true;
        FreezeWorld();
        pauseMenuVisual.SetActive(gameIsPaused);
        Debug.Log("Paused: " + gameIsPaused);

    }

    public void UnpauseGame()
    {
        gameIsPaused = false;
        UnfreezeWorld(pauseMenuVisual);
        pauseMenuVisual.SetActive(gameIsPaused);
        Debug.Log("Paused: " + gameIsPaused);


    }
}
