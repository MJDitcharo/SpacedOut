using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //put gameIsPaused into gamemanager 

    //false by default
    [SerializeField]
    bool gameIsPaused = false;
    [SerializeField]
     GameObject pauseMenuVisual;

    private void Update()
    {
        PauseGame();
    }

    public void PauseGame()
    {
        if (Input.GetButtonUp("Cancel"))
        {
            gameIsPaused = !gameIsPaused;
            Debug.Log("Pause Pressed");
            pauseMenuVisual.SetActive(gameIsPaused);
        }
    }
}
