using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMenu : MonoBehaviour
{
    public void FreezeWorld(GameObject objectToToggle)
    {
        Time.timeScale = 0;
        GameManager.instance.movement.enabled = false;
        objectToToggle.SetActive(true);
    }

    public void UnfreezeWorld(GameObject objectToToggle)
    {
        Time.timeScale = 1;
        GameManager.instance.movement.enabled = true;
        objectToToggle.SetActive(false);
    }
}
