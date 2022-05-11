using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : PopUpMenu
{
    //button objects
    [SerializeField]
    GameObject menuVisual;
    public void ResumeClicked()
    {
        Debug.Log("Resume clicked");
        UnfreezeWorld(menuVisual);
        menuVisual.SetActive(false);
    }

    public void QuitClicked()
    {
        Application.Quit();
    }

    public void RestartLevelClicked()
    {
        //making sure the game is unpaused
        UnfreezeWorld();
        GameManager.instance.Respawn();

        //UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
