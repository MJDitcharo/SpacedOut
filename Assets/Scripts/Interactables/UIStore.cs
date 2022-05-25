using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStore : PopUpMenu
{

    [SerializeField]
    private GameObject pages = null;
    [SerializeField]
    private GameObject storeVisual;

    //pickup page
    public enum PickupCosts { Health, Ammo, Grenade, BoardWipe, Armor };
    public int[] defaultPickupCosts = { 200, 200, 300, 1500, 100 };

    protected int boardWipeCost, healthCost, ammoCost, grenadeCost, armorCost;
    protected TMPro.TextMeshProUGUI boardWipeCostStr, healthCostStr, ammoCostStr, grenadeCostStr, armorCoststr;


    protected Dictionary<TMPro.TextMeshProUGUI, int> pickups;
    protected enum WeaponUpgradeCosts { Pistol, Shotgun, Heavy, Rifle, Melee };
    protected int[] defaultWeaponUpgradeCosts = { 200, 200, 300, 1500, 100 };
    protected Dictionary<TMPro.TextMeshProUGUI, int> weaponUpgrades;




    protected bool purchaseFailed = false;

    private void Start()
    {
        if (pages == null)
            return;
        foreach (Transform item in pages.transform)
        {

            if (item.name == "Health")
            {
                Debug.Log("Inside" + item.name);
                healthCost = defaultPickupCosts[(int)PickupCosts.Health];
                healthCostStr = item.Find("Cost").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
                Debug.Log(healthCostStr.text);
            }
            else if(item.name == "Board Wipe")
            {
                boardWipeCost = defaultPickupCosts[(int)PickupCosts.BoardWipe];
                boardWipeCostStr = item.Find("Cost").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            }
            else if (item.name == "Ammo")
            {
                ammoCost = defaultPickupCosts[(int)PickupCosts.Ammo];
                ammoCostStr = item.Find("Cost").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            }
            else if (item.name == "Grenade")
            {
                grenadeCost = defaultPickupCosts[(int)PickupCosts.Grenade];
                grenadeCostStr = item.Find("Cost").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
            }
        }

        //add all 
        //int i = 0;
        //foreach (Transform transform in pages[i].transform) //get to the children of the page
        //{
        //    int j = 0;
        //    Debug.Log(transform.name);
        //    if (transform.parent.name == "Pickups Page")
        //    {
        //        foreach(Transform nextTrans in transform)
        //        {
        //            pickups.Add(nextTrans.Find("Cost").gameObject.GetComponent<TMPro.TextMeshProUGUI>(), defaultPickupCosts[j]);
        //            break;
        //        }
        //    }
        //    //else if (transform.parent.name == "Weapon Upgrade Page")
        //    //{
        //    //    weaponUpgrades.Add(transform.Find("Cost").gameObject.GetComponent<TMPro.TextMeshProUGUI>(), defaultWeaponUpgradeCosts[j]);
        //    //}
        //    j++;
        //    i++;
        //}



    }

    private void Update()
    {
        //check to see if the player has enough money
        //if not, make the text red
        if (purchaseFailed)
            ShowPurchaseFailed();
    }



    public void Activate()
    {
        storeVisual.SetActive(true);
        //GameObject.Find("Weapon Upgrades Page").gameObject.SetActive(false);
        FreezeWorld();
    }
    public void Deactivate()
    {
        storeVisual.SetActive(false);
        UnfreezeWorld();
    }

    private void ShowPurchaseFailed()
    {

    }

    #region Shop Buttons
    public void ExitShop()
    {
        GameManager.instance.shopUI.Deactivate();
    }

    // Allows you to buy anything in the shop taking away the proper amount of skrap while adding the item
    public void BuyBoardWipe()
    {
        if (GameManager.instance.skrapCount.GetQuantity() >= defaultPickupCosts[(int)PickupCosts.BoardWipe])
        {
            GameManager.instance.boardWipeCount.Add();
            GameManager.instance.skrapCount.Subtract(defaultPickupCosts[(int)PickupCosts.BoardWipe]);
            Debug.Log("Skrap Taken");
        }
        else
            purchaseFailed = true;

    }
    public void BuyGrenade()
    {
        if (GameManager.instance.skrapCount.GetQuantity() >= defaultPickupCosts[(int)PickupCosts.Grenade])
        {
            GameManager.instance.grenadeCount.Add();
            GameManager.instance.skrapCount.Subtract(defaultPickupCosts[(int)PickupCosts.Grenade]);
            Debug.Log("Skrap Taken");
        }
        else
            purchaseFailed = true;


    }
    public void BuyArmor()
    {
        if (GameManager.instance.skrapCount.GetQuantity() >= defaultPickupCosts[(int)PickupCosts.Armor])
        {
            //GameManager.instance.armorCount.Add();
            GameManager.instance.skrapCount.Subtract(defaultPickupCosts[(int)PickupCosts.Armor]);
            Debug.Log("Skrap Taken");
        }
        else
            purchaseFailed = true;

    }
    public void BuyHealth()
    {
        if (GameManager.instance.skrapCount.GetQuantity() >= defaultPickupCosts[(int)PickupCosts.Armor])
        {
            GameManager.instance.playerHealth.AddHealth(25);
            GameManager.instance.skrapCount.Subtract(defaultPickupCosts[(int)PickupCosts.Health]);
            Debug.Log("Skrap Taken");
            
        }
        else
            purchaseFailed = true;
    }
    public void BuyAmmo()
    {
        if (GameManager.instance.skrapCount.GetQuantity() >= defaultPickupCosts[(int)PickupCosts.Armor])
        {
            GameManager.instance.ammoCount.Add(25);
            GameManager.instance.skrapCount.Subtract(defaultPickupCosts[(int)PickupCosts.Ammo]);
            Debug.Log("Skrap Taken");
        }
        else
            purchaseFailed = true;
    }

    #endregion

}