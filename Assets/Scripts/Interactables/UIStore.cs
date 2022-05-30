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
    [SerializeField]
    private TMPro.TextMeshProUGUI purchaseMessage;


    //pickup page
    public enum PickupCosts { Health, Ammo, Grenade, BoardWipe, Armor };
    public int[] defaultPickupCosts = { 200, 200, 300, 1500, 100 };
    protected List<TMPro.TextMeshProUGUI> pickupText = new List<TMPro.TextMeshProUGUI>();
    protected List<int> pickupCosts = new List<int>();


    protected int boardWipeCost, healthCost, ammoCost, grenadeCost, armorCost;
    protected TMPro.TextMeshProUGUI boardWipeCostStr, healthCostStr, ammoCostStr, grenadeCostStr, armorCoststr;

    protected Dictionary<TMPro.TextMeshProUGUI, int> pickups;
    protected enum WeaponUpgradeCosts { Pistol, Shotgun, Heavy, Rifle, Melee };
    protected int[] defaultWeaponUpgradeCosts = { 1000, 1250, 1250, 2000, 1000 };
    protected List<TMPro.TextMeshProUGUI> weaponText = new List<TMPro.TextMeshProUGUI>();
    protected List<int> weaponCost = new List<int>();
    int tempCurrentSkrap;


    private void Start()
    {
        if (purchaseMessage != null)
            purchaseMessage.text = string.Empty;
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
    }

    public void Activate()
    {
        storeVisual.SetActive(true);
        tempCurrentSkrap = GameManager.instance.skrapCount.GetQuantity();
        //GameObject.Find("Weapon Upgrades Page").gameObject.SetActive(false);
        FreezeWorld();
    }
    public void Deactivate()
    {
        storeVisual.SetActive(false);
        UnfreezeWorld();
    }

    private void CheckPurchaseItem(ItemCount itemCount, int cost, int quantity = 0)
    {
        bool purchaseFailed;
        if (GameManager.instance.skrapCount.GetQuantity() >= cost)
        {
            itemCount.Add(quantity);
            GameManager.instance.skrapCount.Subtract(cost);
            purchaseFailed = false;
        }
        else
            purchaseFailed = true;
        ShowPurchaseMessage(purchaseFailed);
    }
    private void PurchaseAmmo(ItemCount itemCount, string weaponName, int cost, int quantity = 0)
    {
        bool purchaseFailed;
        if (GameManager.instance.skrapCount.GetQuantity() >= cost)
        {
            WeaponBase currentWeapon = WeaponHolder.instance.transform.Find(weaponName).GetComponent<WeaponBase>();
            currentWeapon.AddAmmo(quantity);
            GameManager.instance.skrapCount.Subtract(cost);
            purchaseFailed = true;
        }
        else
            purchaseFailed = false;
        ShowPurchaseMessage(purchaseFailed);
    }
    private void CheckPurchaseItem(playerHealth health, int cost, int quantity)
    {
        bool purchaseFailed;
        if (GameManager.instance.skrapCount.GetQuantity() >= cost)
        {
            health.AddHealth(quantity);
            GameManager.instance.skrapCount.Subtract(cost);
            purchaseFailed = false;
        }
        else
            purchaseFailed = true;
        ShowPurchaseMessage(purchaseFailed);
    }

    private void CheckPurchaseItem(int weaponIndex, float multiplier)
    {
        bool purchaseFailed;
        //WeaponHolder.instance.UpgradeFireRate("Pistol", 1.5f);
        if (GameManager.instance.skrapCount.GetQuantity() >= defaultWeaponUpgradeCosts[weaponIndex])
        {
            WeaponHolder.instance.UpgradeDamage(weaponIndex, multiplier);
            WeaponHolder.instance.UpgradeFireRate(weaponIndex, multiplier);
            GameManager.instance.skrapCount.Subtract(defaultWeaponUpgradeCosts[weaponIndex]);
            purchaseFailed = false;
        }
        else
            purchaseFailed = true;
        ShowPurchaseMessage(purchaseFailed);
    }
    private void ShowPurchaseMessage(bool purchaseFailed)
    {
        if (!purchaseFailed)
        {
            //purchaseMessage.color = Color.green;
            //purchaseMessage.text = "Thank You!";
        }
        else 
        {
            //purchaseMessage.color = Color.red;
            //purchaseMessage.text = "Transaction Failed!";
        }
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
        CheckPurchaseItem(GameManager.instance.boardWipeCount, defaultPickupCosts[(int)PickupCosts.BoardWipe]);
    }
    public void BuyGrenade()
    {
        CheckPurchaseItem(GameManager.instance.grenadeCount, defaultPickupCosts[(int)PickupCosts.Grenade]);
    }
    public void BuyArmor()
    {
        //armor doesn't work
    }
    public void BuyHealth()
    {
        CheckPurchaseItem(GameManager.instance.playerHealth, defaultPickupCosts[(int)PickupCosts.Health], 25);
    }
    #endregion

    #region WeaponButtons
    public void UpgradePistol()
    {
        CheckPurchaseItem(0, 1.5f);
    }

    public void BuyPistolAmmo()
    {
        PurchaseAmmo(GameManager.instance.ammoCount, "Pistol", defaultPickupCosts[(int)PickupCosts.Ammo], 25);
    }

    public void UpgradeShotgun()
    {

    }

    public void BuyShotgunAmmo()
    {
        PurchaseAmmo(GameManager.instance.ammoCount, "Shotgun", defaultPickupCosts[(int)PickupCosts.Ammo], 10);
    }

    public void UpgradeHeavy()
    {
        CheckPurchaseItem(2, 1.5f);
    }
    public void BuyHeavyAmmo()
    {
        PurchaseAmmo(GameManager.instance.ammoCount, "Heavy", defaultPickupCosts[(int)PickupCosts.Ammo], 5);
    }
    public void UpgradeRifle()
    {
        CheckPurchaseItem(3, 1.5f);
    }
    public void BuyRifleAmmo()
    {
        PurchaseAmmo(GameManager.instance.ammoCount, "Rifle", defaultPickupCosts[(int)PickupCosts.Ammo], 10);
    }

    #endregion

    #endregion
}