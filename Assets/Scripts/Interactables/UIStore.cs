using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStore : PopUpMenu
{
    [SerializeField]
    private GameObject oneShopVisual = null;
    [SerializeField]
    private bool isButton = false;
    static private GameObject shopVisual;
    [SerializeField]
    private TMPro.TextMeshProUGUI purchaseMessage;
    private List<GameObject> pages = new();


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

        //new pages system
        bool first = true;
            Debug.Log("children" + shopVisual.transform.childCount);
        if(!isButton)
        {
            foreach (Transform page in shopVisual.transform)
            {
                if (page.name.Contains("Page"))
                {

                    Debug.Log("Added" + page.name);
                    //pages.Add(page.gameObject);
                    if (first) //the first page in the hierarchy will be the only one shown
                    {
                        page.gameObject.SetActive(true);
                        first = false;
                    }
                    else
                        page.gameObject.SetActive(false);
                }
            }
        }
    }

    public void Activate()
    {
        if (isButton)
            return;
        shopVisual.SetActive(true);
        tempCurrentSkrap = GameManager.instance.skrapCount.GetQuantity();
        //GameObject.Find("Weapon Upgrades Page").gameObject.SetActive(false);
        FreezeWorld();
    }
    public void Deactivate()
    {
        if (isButton)
            return;
        shopVisual.SetActive(false);
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
        //get the index of the element
        for (int i = 0; i < pages.Count; i++)
        {
            if(pages[i].activeInHierarchy)
            {
                //deactivate current page
                pages[i].SetActive(false);
                if (i + 1 < pages.Count)//set the next page as active.
                    pages[i + 1].SetActive(true);
                else //if the next is out of bounds, go to the first page
                    pages[0].SetActive(true);
                break;
            }
        }

    }

    public void PreviousPage()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            if (pages[i].activeInHierarchy)
            {
                //deactivate current page
                pages[i].SetActive(false);//set the next page as active.
                if (i - 1 < 0)
                    pages[pages.Count - 1].SetActive(true);
                else
                    pages[i - 1].SetActive(true);
            }
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