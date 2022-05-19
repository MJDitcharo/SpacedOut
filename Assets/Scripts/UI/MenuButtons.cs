using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : PopUpMenu
{
    //button objects
    [SerializeField]
    GameObject menuVisual;
    static public void ResumeClicked()
    {
        GameManager.instance.pmenu.UnpauseGame();
    }

    public void QuitClicked()
    {
        GameManager.instance.pmenu.UnpauseGame();
        Application.Quit();
    }

    public void RestartLevelClicked()
    {
        //making sure the game is unpaused
        GameManager.instance.pmenu.UnpauseGame();
        //GameManager.instance.Respawn();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    
    public void OKChestClicked()
    {
        GameManager.instance.chestUI.Deactivate();
    }
}
