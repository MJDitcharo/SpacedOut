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

    public void FreezeWorld()
    {
        Time.timeScale = 0;
        GameManager.instance.movement.enabled = false;
    }
    public void UnfreezeWorld()
    {
        Time.timeScale = 1;
        GameManager.instance.movement.enabled = true;
    }


    //virtual protected void AcceptButtonPress(KeyCode key, bool check)
    //{
    //    if (Input.GetKeyDown(key) && check)
            

    //}

    virtual public bool IsActive()
    {
        return false;
    }
   
    
}
