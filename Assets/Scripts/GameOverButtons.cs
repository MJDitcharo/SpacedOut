using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : PopUpMenu
{
    // Start is called before the first frame update
    public void Quit()
    {
        Application.Quit();
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void LastCheckpoint()
    {
        GameManager.instance.Respawn();
        Time.timeScale = 1;
    }

    private void Update()
    {
        if(gameObject.activeInHierarchy == false)
        {
            Time.timeScale = 1;
        }
    }

    public void WinningMainMenu()
    {
        //reset the player prefs
        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.DeleteKey("Weapon " + i);
        }

        PlayerPrefs.SetInt("SavedGame", 1);
        PlayerPrefs.SetInt("Scene Index", 1);
        PlayerPrefs.SetInt("Checkpoint Index", 0);
        PlayerPrefs.SetInt("Skrap Count", 0);
        PlayerPrefs.SetInt("Player Health", 100);
        PlayerPrefs.SetInt("Max Player Health", 100);
        PlayerPrefs.SetInt("Board Wipes", 0);
        PlayerPrefs.SetInt("Chest Opened", 0);
        PlayerPrefs.SetInt("Child Count", 0);
        PlayerPrefs.SetString("Weapon 0", "Pistol");
        PlayerPrefs.SetInt("Pistol Ammo", 45);
        
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
