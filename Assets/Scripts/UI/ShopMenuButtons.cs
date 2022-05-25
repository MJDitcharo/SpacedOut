//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ShopMenuButtons : UIStore
//{
//    #region Shop Buttons
//    public void ExitShop()
//    {
//        GameManager.instance.shopUI.Deactivate();
//    }

//    public void NextPage()
//    {

//    }

//    // Allows you to buy anything in the shop taking away the proper amount of skrap while adding the item
//    public void BuyBoardWipe()
//    {
//        if (GameManager.instance.skrapCount.GetQuantity() >= defaultPickupCosts[(int)PickupCosts.BoardWipe])
//        {
//            GameManager.instance.boardWipeCount.Add();
//            GameManager.instance.skrapCount.Subtract(defaultPickupCosts[(int)PickupCosts.BoardWipe]);
//        }
//        else
//            purchaseFailed = true;

//    }
//    public void BuyGrenade()
//    {
//        if (GameManager.instance.skrapCount.GetQuantity() >= defaultPickupCosts[(int)PickupCosts.Grenade])
//        {
//            GameManager.instance.grenadeCount.Add();
//            GameManager.instance.skrapCount.Subtract(defaultPickupCosts[(int)PickupCosts.Grenade]);
//        }
//        else
//            purchaseFailed = true;


//    }
//    public void BuyArmor()
//    {
//        if (GameManager.instance.skrapCount.GetQuantity() >= defaultPickupCosts[(int)PickupCosts.Armor])
//        {
//            //GameManager.instance.armorCount.Add();
//            GameManager.instance.skrapCount.Subtract(defaultPickupCosts[(int)PickupCosts.Armor]);
//        }
//        else
//            purchaseFailed = true;

//    }
//    public void BuyHealth()
//    {
//        if (GameManager.instance.skrapCount.GetQuantity() >= defaultPickupCosts[(int)PickupCosts.Armor])
//        {
//            GameManager.instance.playerHealth.AddHealth(25);
//            GameManager.instance.skrapCount.Subtract(defaultPickupCosts[(int)PickupCosts.Health]);
//        }
//        else
//            purchaseFailed = true;
//    }
//    #endregion
//}
