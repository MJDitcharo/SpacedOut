using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenuButtons : UIStore
{
    int skrap = GameManager.instance.skrapCount.GetQuantity();
    #region Shop Buttons
    public void ExitShop()
    {
        GameManager.instance.shopUI.Deactivate();
    }

    // Allows you to buy anything in the shop taking away the proper amount of skrap while adding the item
    public void BuyBoardWipe()
    {
        if (skrap >= defaultPickupCosts[(int)PickupCosts.BoardWipe])
        {
            GameManager.instance.boardWipeCount.Add();
            GameManager.instance.skrapCount.Subtract(defaultPickupCosts[(int)PickupCosts.BoardWipe]);
        }
        else
            purchaseFailed = true;
    }
    public void BuyGrenade()
    {
        if (skrap >= defaultPickupCosts[(int)PickupCosts.Grenade])
        {
            GameManager.instance.grenadeCount.Add();
            GameManager.instance.skrapCount.Subtract(defaultPickupCosts[(int)PickupCosts.Grenade]);
        }
        else
            purchaseFailed = true;


    }
    public void BuyArmor()
    {
        if (skrap >= defaultPickupCosts[(int)PickupCosts.Armor])
        {
            //GameManager.instance.armorCount.Add();
            GameManager.instance.skrapCount.Subtract(defaultPickupCosts[(int)PickupCosts.Armor]);
        }
        else
            purchaseFailed = true;

    }
    public void BuyHealth()
    {
        if (skrap >= defaultPickupCosts[(int)PickupCosts.Armor])
        {
            GameManager.instance.playerHealth.AddHealth(25);
            GameManager.instance.skrapCount.Subtract(defaultPickupCosts[(int)PickupCosts.Health]);
        }
        else
            purchaseFailed = true;
    }
    #endregion
}
