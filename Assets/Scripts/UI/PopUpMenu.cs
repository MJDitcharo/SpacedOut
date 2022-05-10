using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMenu : MonoBehaviour
{
    public void FreezeWorld()
    {
        //freeze time and player
            Time.timeScale = 0;
            GameManager.instance.movement.enabled = false;
    }

    public void UnfreezeWorld()
    {
        Time.timeScale = 1;
        GameManager.instance.movement.enabled = true;
    }
}
