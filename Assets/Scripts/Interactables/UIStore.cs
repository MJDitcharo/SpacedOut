using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStore : PopUpMenu
{
    [SerializeField]
    private GameObject OneShopVisual;
    static private GameObject shopVisual;
    [SerializeField]
    private GameObject purchaseMessageObj;
    static private GameObject statPurchaseMessage;
    private TMPro.TextMeshProUGUI purchaseMessage;
    private List<GameObject> pages = new();


    //pickup page
    public enum PickupCosts { Health, Ammo, Grenade, BoardWipe, Armor };
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
        if (shopVisual == null)
            shopVisual = OneShopVisual;
        if (purchaseMessageObj == null)
            purchaseMessageObj = statPurchaseMessage;
        else
            statPurchaseMessage = purchaseMessageObj;

        //grab textmesh component for purchase message
        purchaseMessage = purchaseMessageObj.GetComponent<TMPro.TextMeshProUGUI>();

        //new pages system
        bool first = true;
        foreach (Transform page in shopVisual.transform)
        {
            if (page.name.Contains("Page"))
            {
                pages.Add(page.gameObject);
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
    public void Activate()
    {
        if (!GameManager.instance.pmenu.gameIsPaused)
        {
            shopVisual.SetActive(true);
            tempCurrentSkrap = GameManager.instance.skrapCount.GetQuantity();
            GameManager.instance.menuIsActive = true;
            //GameObject.Find("Weapon Upgrades Page").gameObject.SetActive(false);
            FreezeWorld();
            Debug.Log("menu is active");
        }
    }
    public void Deactivate()
    {
        shopVisual.SetActive(false);
        GameManager.instance.menuIsActive = false;
        UnfreezeWorld();
        Debug.Log("menu is deactive");

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
        StartCoroutine(HandlePurchaseMessage(purchaseFailed));
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
        StartCoroutine(HandlePurchaseMessage(purchaseFailed));
    }
    private void CheckPurchaseItem(int weaponIndex, float multiplier, int cost)
    {
        //bool purchaseFailed;
        ////WeaponHolder.instance.UpgradeFireRate("Pistol", 1.5f);
        //if (GameManager.instance.skrapCount.GetQuantity() >= cost)
        //{
        //    WeaponHolder.instance.UpgradeDamage(weaponIndex, multiplier);
        //    WeaponHolder.instance.UpgradeFireRate(weaponIndex, multiplier);
        //    GameManager.instance.skrapCount.Subtract(defaultWeaponUpgradeCosts[weaponIndex]);
        //    purchaseFailed = false;
        //}
        //else
        //    purchaseFailed = true;
        //StartCoroutine(HandlePurchaseMessage(purchaseFailed));

    }
    private IEnumerator HandlePurchaseMessage(bool purchaseFailed, string message = "")
    {
        if (!purchaseFailed)
        {
            purchaseMessage.color = Color.green;
            message = "Transaction Success!";
        }
        else
        {
            purchaseMessage.color = Color.red;
            message = "Transaction Failed!";
        }
        purchaseMessage.text = message;

        statPurchaseMessage.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        statPurchaseMessage.SetActive(false);
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
            if (pages[i].activeInHierarchy)
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
        CheckPurchaseItem(GameManager.instance.boardWipeCount, GeneralPage.instance.boardWipe);
    }
    public void BuyGrenade()
    {
        CheckPurchaseItem(GameManager.instance.grenadeCount, GeneralPage.instance.grenade);
    }
    public void BuyArmor()
    {
        //armor doesn't work
    }
    public void BuyHealth()
    {
        bool purchaseFailed;
        if (GameManager.instance.skrapCount.GetQuantity() >= GeneralPage.instance.health)
        {
            GameManager.instance.playerHealth.AddHealth((float)(GeneralPage.instance.healthQuantity * .01f));
            GameManager.instance.skrapCount.Subtract(GeneralPage.instance.health);
            purchaseFailed = false;
        }
        else
            purchaseFailed = true;
        StartCoroutine(HandlePurchaseMessage(purchaseFailed));
    }

    public void BuyMaxHealth()
    {
        bool purchaseFailed;
        if (GameManager.instance.skrapCount.GetQuantity() >= GeneralPage.instance.maxHealth)
        {
            GameManager.instance.playerHealth.AddMaxHealth(10);
            GameManager.instance.skrapCount.Subtract(GeneralPage.instance.maxHealth);
            purchaseFailed = false;
        }
        else
            purchaseFailed = true;
        StartCoroutine(HandlePurchaseMessage(purchaseFailed));
    }

    #endregion

    #region WeaponButtons
    public void UpgradePistol()
    {
    }

    public void BuyPistolAmmo()
    {
    }

    public void UpgradeShotgun()
    {

    }

    public void BuyShotgunAmmo()
    {
    }

    public void UpgradeHeavy()
    {
    }
    public void BuyHeavyAmmo()
    {
    }
    public void UpgradeRifle()
    {
    }
    public void BuyRifleAmmo()
    {
    }

    #endregion

    #endregion
}