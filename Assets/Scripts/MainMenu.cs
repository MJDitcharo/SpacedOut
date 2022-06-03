using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    bool savedGame = true;
    [SerializeField] GameObject noDataPopUp = null;
    [SerializeField] GameObject savedDataPopUp = null;
    public void QuitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {

    }

    public void LoadGame()
    {
        if (savedGame == false)
        {

        }
        else
        {
            noDataPopUp.SetActive(true);
            savedDataPopUp.SetActive(false);

        }
    }
}
