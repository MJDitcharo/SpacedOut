using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : PopUpMenu
{
    //button objects
    [SerializeField]
    GameObject menuVisual;
    #region Pause/Game Over Buttons
    static public void ResumeClicked()
    {
        GameManager.instance.pmenu.UnpauseGame();
    }

    public void QuitClicked()
    {
        GameManager.instance.pmenu.UnpauseGame();
        Application.Quit();
    }

    public void RestartLevelClicked()
    {
        //making sure the game is unpaused
        GameManager.instance.pmenu.UnpauseGame();
        //GameManager.instance.Respawn();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    
    public void OKChestClicked()
    {
        GameManager.instance.chestUI.Deactivate();
    }

    #endregion

    #region Shop Buttons
    public void ExitShop()
    {
        GameManager.instance.shopUI.Deactivate();
    }


    // Allows you to buy anything in the shop taking away the proper amount of skrap while adding the item
    public void BuyBoardWipe()
    {
         GameManager.instance.boardWipeCount.Add();

         GameManager.instance.skrapCount.Subtract();

    }
    public void BuyGrenade()
    {
        GameManager.instance.grenadeCount.Add();

        GameManager.instance.skrapCount.Subtract();

    }
    public void BuyArmor()
    {
        //GameManager.instance.armorCount.Add();

        GameManager.instance.skrapCount.Subtract();
        
    }
    public void BuyHealth()
    {
        GameManager.instance.playerHealth.AddHealth(25);
        
        GameManager.instance.skrapCount.Subtract();
    }
    #endregion

}
