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

    private void Start()
    {
        //false by default
        pauseMenuVisual.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetButtonUp("Cancel") && !GameManager.instance.shopIsActive)
        {
            if (gameIsPaused)
                UnpauseGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        gameIsPaused = true;
        FreezeWorld();
        pauseMenuVisual.SetActive(gameIsPaused);
        Debug.Log("menu is active");
    }

    public void UnpauseGame()
    {
        gameIsPaused = false;
        UnfreezeWorld(pauseMenuVisual);
        pauseMenuVisual.SetActive(gameIsPaused);
        Debug.Log("menu is deactive");
    }

    override public bool IsActive()
    {
        return pauseMenuVisual.activeSelf;
    }

  

}
