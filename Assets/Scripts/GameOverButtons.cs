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
        GameManager.instance.player.gameObject.SetActive(true);
        GameManager.instance.Respawn();
        Time.timeScale = 1;
        GameManager.instance.SetFightingCursor();
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
        PlayerPrefs.SetInt("Pistol Ammo", 1000);
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
        PlayerPrefs.SetInt("Pistol Ammo", 1000);
        PlayerPrefs.SetInt("Pistol Page", 0);
        PlayerPrefs.SetInt("Shotgun Page", 0);
        PlayerPrefs.SetInt("Rifle Page", 0);
        PlayerPrefs.SetInt("Heavy Page", 0);

        //store data
        PlayerPrefs.SetInt("PistolPage", 1);
        PlayerPrefs.SetInt("ShotgunPage", 0);
        PlayerPrefs.SetInt("HeavyPage", 0);
        PlayerPrefs.SetInt("RiflePage", 0);

        //-1 is for locked items
        PlayerPrefs.SetFloat("Pistol Damage", 1);
        PlayerPrefs.SetFloat("Pistol Fire Rate", 1);

        PlayerPrefs.SetFloat("Shotgun Damage", 1);
        PlayerPrefs.SetFloat("Shotgun Fire Rate", 1);

        PlayerPrefs.SetFloat("Rifle Damage", 1);
        PlayerPrefs.SetFloat("Rifle Fire Rate", 1);

        PlayerPrefs.SetFloat("Heavy Damage", 1);
        PlayerPrefs.SetFloat("Heavy Fire Rate", 1);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
