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
    }

    public void LastCheckpoint()
    {
        GameManager.instance.Respawn();
    }

    private void Update()
    {
        if(gameObject.activeInHierarchy == false)
        {
            Time.timeScale = 1;
        }
    }
}
