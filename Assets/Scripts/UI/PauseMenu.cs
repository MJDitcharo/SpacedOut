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
        if(Input.GetButtonUp("Cancel") && !GameManager.instance.shopIsActive && !GameManager.instance.chestUI.VisualIsActive())
        {
            if (gameIsPaused)
                UnpauseGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        GameManager.instance.SetNormalCursor();
        gameIsPaused = true;
        FreezeWorld();
        pauseMenuVisual.SetActive(gameIsPaused);
        Debug.Log("menu is active");
        ScreenShake.instance.StopAllCoroutines();
    }

    public void UnpauseGame()
    {
        GameManager.instance.SetFightingCursor();
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
