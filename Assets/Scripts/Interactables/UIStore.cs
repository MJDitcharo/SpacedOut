using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStore : PopUpMenu
{
    [SerializeField]
    public GameObject pickupsPage;
    [SerializeField]
    public GameObject weaponsPage;
    [SerializeField]
    private GameObject storeVisual;

    //pickup page
    public enum PickupCosts { Health, Ammo, Grenade, BoardWipe, Armor };
    public int[] defaultPickupCosts = { 200, 200, 300, 1500, 100 };
    protected List<TMPro.TextMeshProUGUI> pickupText = new List<TMPro.TextMeshProUGUI>();
    protected List<int> pickupCosts = new List<int>();


    protected int boardWipeCost, healthCost, ammoCost, grenadeCost, armorCost;
    protected TMPro.TextMeshProUGUI boardWipeCostStr, healthCostStr, ammoCostStr, grenadeCostStr, armorCoststr;

    protected Dictionary<TMPro.TextMeshProUGUI, int> pickups;
    protected enum WeaponUpgradeCosts { Pistol, Shotgun, Heavy, Rifle, Melee };
    protected int[] defaultWeaponUpgradeCosts = { 1000, 1250, 1250, 1250, 1000 };
    protected List<TMPro.TextMeshProUGUI> weaponText = new List<TMPro.TextMeshProUGUI>();
    protected List<int> weaponCost = new List<int>();



    protected bool purchaseFailed = false;

    private void Start()
    {

        if (pickupsPage == null || weaponsPage == null)
            return;
        int i = 0;
        foreach (Transform item in pickupsPage.transform)
        {
            //2 lists - one for textmesh, one for cost
            pickupCosts.Add(defaultPickupCosts[i]);
            pickupText.Add(item.Find("Cost").gameObject.GetComponent<TMPro.TextMeshProUGUI>());
        }

        foreach (Transform item in weaponsPage.transform)
        {
            weaponCost.Add(defaultWeaponUpgradeCosts[i]);
            weaponText.Add(item.Find("Cost").gameObject.GetComponent<TMPro.TextMeshProUGUI>());
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

    public void NextPage()
    {
        if (!weaponsPage.activeInHierarchy && pickupsPage.activeInHierarchy)
        {
            weaponsPage.SetActive(true);
            pickupsPage.SetActive(false);
        }
        else
        {
            pickupsPage.SetActive(true);
            weaponsPage.SetActive(false);
        }
    }

    #region PickupButtons
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
        if (GameManager.instance.skrapCount.GetQuantity() >= defaultPickupCosts[(int)PickupCosts.Ammo])
        {
            GameManager.instance.ammoCount.Add(5);
            GameManager.instance.skrapCount.Subtract(defaultPickupCosts[(int)PickupCosts.Ammo]);
            Debug.Log("Skrap Taken");
        }
        else
            purchaseFailed = true;
    }
    #endregion

    #region WeaponButtons
    public void UpgradePistol()
    {
        //WeaponHolder.instance.UpgradeDamage("Pistol", 1.5f);
        //WeaponHolder.instance.UpgradeFireRate("Pistol", 1.5f);
        if (GameManager.instance.skrapCount.GetQuantity() >= defaultWeaponUpgradeCosts[(int)WeaponUpgradeCosts.Pistol])
        {
            WeaponHolder.instance.UpgradeDamage(0, 1.5f);
            WeaponHolder.instance.UpgradeFireRate(0, 1.5f);
            GameManager.instance.skrapCount.Subtract(defaultPickupCosts[(int)WeaponUpgradeCosts.Pistol]);
        }
    }

    public void UpgradeShotgun()
    {
        //WeaponHolder.instance.UpgradeDamage("Shotgun", 1.5f);
        //WeaponHolder.instance.UpgradeFireRate("Shotgun", 1.5f);
        if (GameManager.instance.skrapCount.GetQuantity() >= defaultWeaponUpgradeCosts[(int)WeaponUpgradeCosts.Shotgun])
        {
            WeaponHolder.instance.UpgradeDamage(1, 1.5f);
            WeaponHolder.instance.UpgradeFireRate(1, 1.5f);
            GameManager.instance.skrapCount.Subtract(defaultPickupCosts[(int)WeaponUpgradeCosts.Shotgun]);

        }
    }

    public void UpgradeHeavy()
    {
        //WeaponHolder.instance.UpgradeDamage("Heavy", 1.5f);
        //WeaponHolder.instance.UpgradeFireRate("Heavy", 1.5f);
        if (GameManager.instance.skrapCount.GetQuantity() >= defaultWeaponUpgradeCosts[(int)WeaponUpgradeCosts.Heavy])
        {
            WeaponHolder.instance.UpgradeDamage(2, 1.5f);
            WeaponHolder.instance.UpgradeFireRate(2, 1.5f);
            GameManager.instance.skrapCount.Subtract(defaultPickupCosts[(int)WeaponUpgradeCosts.Heavy]);
        }
    }

    public void UpgradeRifle()
    {
        //WeaponHolder.instance.UpgradeDamage("Rifle", 1.5f);
        //WeaponHolder.instance.UpgradeFireRate("Rifle", 1.5f);
        if (GameManager.instance.skrapCount.GetQuantity() >= defaultWeaponUpgradeCosts[(int)WeaponUpgradeCosts.Rifle])
        {
            WeaponHolder.instance.UpgradeDamage(3, 1.5f);
            WeaponHolder.instance.UpgradeFireRate(3, 1.5f);
            GameManager.instance.skrapCount.Subtract(defaultPickupCosts[(int)WeaponUpgradeCosts.Rifle]);
        }
    }

    #endregion
    #endregion
}