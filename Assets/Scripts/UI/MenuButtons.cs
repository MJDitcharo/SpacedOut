using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : PopUpMenu
{

    public void ResumeClicked()
    {
        Debug.Log("Resume clicked");
        //UnfreezeWorld();
    }

    public void QuitClicked()
    {
        Application.Quit();
    }

    public void RestartLevelClicked()
    {

    }
}
