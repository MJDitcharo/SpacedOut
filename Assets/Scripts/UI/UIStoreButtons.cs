using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStoreButtons : MonoBehaviour
{
    static public int purchaseIndex = 1;
    private void CheckPurchaseItem(ItemCount itemCount, int cost, int quantity = 1)
    {
        bool purchaseFailed;
        if (GameManager.instance.skrapCount.GetQuantity() >= cost)
        {
            Debug.Log("quantity: " + quantity);

            itemCount.Add(quantity);
            GameManager.instance.skrapCount.Subtract(cost);
            purchaseFailed = false;
        }
        else
            purchaseFailed = true;
        StartCoroutine(UIStore.instance.HandlePurchaseMessage(purchaseFailed));
    }
    private void PurchaseAmmo(string weaponName, int cost, int quantity = 0)
    {
        bool purchaseFailed;
        if (GameManager.instance.skrapCount.GetQuantity() >= cost)
        {
            WeaponBase currentWeapon = WeaponHolder.instance.transform.Find(weaponName).GetComponent<WeaponBase>();
            GameManager.instance.ammoCount.SetQuantity(quantity);
            currentWeapon.AddAmmo(quantity);
            GameManager.instance.skrapCount.Subtract(cost);
            purchaseFailed = false;
        }
        else
            purchaseFailed = true;
        StartCoroutine(UIStore.instance.HandlePurchaseMessage(purchaseFailed));
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

    private void UpgradeFireRate(string weaponName, int cost, float fireRate)
    {
        bool purchaseFailed;
        if (GameManager.instance.skrapCount.GetQuantity() >= cost)
        {
            WeaponHolder.instance.UpgradeFireRate(weaponName, fireRate);
            GameManager.instance.skrapCount.Subtract(cost);
            purchaseFailed = false;
            Debug.Log("Fire rate upgraded");
        }
        else
            purchaseFailed = true;
        StartCoroutine(UIStore.instance.HandlePurchaseMessage(purchaseFailed));
    }

    private void UpgradeDamage(string weaponName, int cost, float damage)
    {
        bool purchaseFailed;
        if (GameManager.instance.skrapCount.GetQuantity() >= cost)
        {
            WeaponHolder.instance.UpgradeDamage(weaponName, damage);
            GameManager.instance.skrapCount.Subtract(cost);
            Debug.Log("Damage upgraded");
            purchaseFailed = false;
        }
        else
            purchaseFailed = true;
        StartCoroutine(UIStore.instance.HandlePurchaseMessage(purchaseFailed));
    }

    

    public void Tier2Upgrade(string baseWeapon, string tier2Weapon, int cost)
    {
        bool purchaseFailed;
        if (GameManager.instance.skrapCount.GetQuantity() >= cost)
        {
            if (WeaponHolder.instance.AddToUnlockedItems(tier2Weapon))
                Debug.Log("Weapon Unlocked");
            else
                Debug.Log("Weapon NOT Unlocked");

            GameManager.instance.skrapCount.Subtract(cost);
            WeaponHolder.instance.Tier2Upgrade(tier2Weapon, baseWeapon, WeaponHolder.instance.transform.Find(baseWeapon).GetSiblingIndex());
            purchaseFailed = false;
        }
        else
            purchaseFailed = true;
        
        StartCoroutine(UIStore.instance.HandlePurchaseMessage(purchaseFailed));

        
        Debug.Log(purchaseIndex);
        WeaponHolder.instance.currentChildCount++;
    }

    private void UnlockWeapon(string weaponName)
    {
        if (WeaponHolder.instance.AddToUnlockedItems(weaponName))
            Debug.Log("Weapon Unlocked");
        else
            Debug.Log("Weapon NOT Unlocked");
        WeaponHolder.instance.ArrangeHierarchy(weaponName, purchaseIndex++);
        WeaponHolder.instance.currentChildCount++;
    }

    #region Shop Buttons
    public void ExitShop()
    {
        UIStore.instance.Deactivate();
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
    public void BuyHealth()
    {
        bool purchaseFailed;
        if (GameManager.instance.skrapCount.GetQuantity() >= GeneralPage.instance.healthCost)
        {
            GameManager.instance.playerHealth.AddHealth((float)(GeneralPage.instance.healthQuantity * .01f));
            GameManager.instance.skrapCount.Subtract(GeneralPage.instance.healthCost);
            purchaseFailed = false;
        }
        else
            purchaseFailed = true;
        StartCoroutine(UIStore.instance.HandlePurchaseMessage(purchaseFailed));
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
        StartCoroutine(UIStore.instance.HandlePurchaseMessage(purchaseFailed));
    }

  

    #endregion

    #region WeaponButtons

    #region Pistol Page
    public void PistolAmmo()
    {
        PurchaseAmmo("Pistol", PistolPage.instance.ammoCost, PistolPage.instance.ammoQuantity);
    }

    public void PistolUpgradeFireRate()
    {
        UpgradeFireRate("Pistol", PistolPage.instance.tier1Upgrade, PistolPage.instance.fireRateQuantity);
        PistolPage.instance.NextTier();
    }

    public void PistolUpgradeDamage()
    {
        UpgradeDamage("Pistol", PistolPage.instance.tier1Upgrade, PistolPage.instance.damageQuantity);
        PistolPage.instance.NextTier();
    }

    public void PistolDeagleUpgrade()
    {
        Tier2Upgrade("Pistol", "Deagle", PistolPage.instance.tier2Upgrade);
        PistolPage.instance.NextTier();
    }

    public void PistolDualWeildUpgrade()
    {
        Tier2Upgrade("Pistol", "Dual Weild", PistolPage.instance.tier2Upgrade);
        PistolPage.instance.NextTier();
    }
    public void PistolVoidUpgrade()
    {

    }
    public void PistolPlasmaUpgrade()
    {

    }
    #endregion

    #region Shotgun Page
    public void ShotgunBuyWeapon()
    {
        bool purchaseFailed;
        if (GameManager.instance.skrapCount.GetQuantity() >= ShotgunPage.instance.weaponCost)
        {
            UnlockWeapon("Shotgun");
            GameManager.instance.skrapCount.Subtract(ShotgunPage.instance.weaponCost);
            purchaseFailed = false;
        }
        else
            purchaseFailed = true;
        StartCoroutine(UIStore.instance.HandlePurchaseMessage(purchaseFailed));
        if(!purchaseFailed)
            ShotgunPage.instance.FirstTier();

    }

    public void ShotgunAmmo()
    {
        PurchaseAmmo("Shotgun", ShotgunPage.instance.ammoCost, ShotgunPage.instance.ammoQuantity);
    }

    public void ShotgunUpgradeFireRate()
    {
        UpgradeFireRate("Shotgun", ShotgunPage.instance.tier1Upgrade, ShotgunPage.instance.fireRateQuantity);
        ShotgunPage.instance.NextTier();
    }

    public void ShotgunUpgradeDamage()
    {
        UpgradeDamage("Shotgun", ShotgunPage.instance.tier1Upgrade, ShotgunPage.instance.damageQuantity);
        ShotgunPage.instance.NextTier();
    }

    public void ShotgunUpgradeSlug()
    {
        Tier2Upgrade("Shotgun", "Slug", ShotgunPage.instance.tier2Upgrade);
        ShotgunPage.instance.NextTier();
    }
    public void ShotgunUpgradeSawedOff()
    {
        Tier2Upgrade("Shotgun", "Sawed-Off", ShotgunPage.instance.tier2Upgrade);
        ShotgunPage.instance.NextTier();
    }
    public void ShotgunPlasmaUpgrade()
    {

    }

    public void ShotgunVoidUpgrade()
    {

    }
    #endregion

    #region Rifle Page

    public void RifleBuyWeapon()
    {
        bool purchaseFailed;
        if (GameManager.instance.skrapCount.GetQuantity() >= RiflePage.instance.weaponCost)
        {
            UnlockWeapon("Rifle");
            GameManager.instance.skrapCount.Subtract(RiflePage.instance.weaponCost);
            purchaseFailed = false;
        }
        else
            purchaseFailed = true;
        StartCoroutine(UIStore.instance.HandlePurchaseMessage(purchaseFailed));
        if (!purchaseFailed)
            RiflePage.instance.FirstTier();
    }
    public void RifleAmmo()
    {
        PurchaseAmmo("Rifle", RiflePage.instance.ammoCost, RiflePage.instance.ammoQuantity);
    }

    public void RifleUpgradeDamage()
    {
        UpgradeDamage("Rifle", RiflePage.instance.tier1Upgrade, RiflePage.instance.damageQuantity);
        //RiflePage.instance.NextTier();
    }
    public void RifleUpgradeFireRate()
    {
        UpgradeFireRate("Rifle", RiflePage.instance.tier1Upgrade, RiflePage.instance.fireRateQuantity);
        RiflePage.instance.NextTier();
    }


    public void RifleBurst()
    {
        Tier2Upgrade("Rifle", "Burst", RiflePage.instance.tier2Upgrade);
        RiflePage.instance.NextTier();
    }

    public void RifleAssault()
    {
        Tier2Upgrade("Rifle", "Assault", RiflePage.instance.tier2Upgrade);
        RiflePage.instance.NextTier();
    }

    public void RiflePlasmaUpgrade()
    {

    }
    public void RifleVoidUpgrade()
    {

    }

    #endregion

    #region Heavy Page

    public void HeavyBuyWeapon()
    {
        bool purchaseFailed;
        if (GameManager.instance.skrapCount.GetQuantity() >= HeavyPage.instance.weaponCost)
        {
            UnlockWeapon("Heavy");
            GameManager.instance.skrapCount.Subtract(HeavyPage.instance.weaponCost);
            purchaseFailed = false;
        }
        else
            purchaseFailed = true;
        StartCoroutine(UIStore.instance.HandlePurchaseMessage(purchaseFailed));
        if (!purchaseFailed)
            HeavyPage.instance.FirstTier();
    }

    public void HeavyAmmo()
    {
        PurchaseAmmo("Heavy", HeavyPage.instance.ammoCost, HeavyPage.instance.ammoQuantity);
    }
    public void HeavyUpgradeDamage()
    {
        UpgradeDamage("Heavy", HeavyPage.instance.tier1Upgrade, HeavyPage.instance.damageQuantity);
        HeavyPage.instance.NextTier();
    }


    public void HeavyUpgradeFireRate()
    {
        UpgradeFireRate("Heavy", HeavyPage.instance.tier1Upgrade, HeavyPage.instance.fireRateQuantity);
        HeavyPage.instance.NextTier();
    }

    public void HeavyGrenadeLauncher()
    {
        HeavyPage.instance.NextTier();
    }

    public void HeavyMinigun()
    {
        HeavyPage.instance.NextTier();
    }

    public void HeavyVoidUpgrade()
    {

    }

    public void HeavyPlasmaUpgrade()
    {

    }
    #endregion
    #endregion

    #endregion




}
