using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStoreButtons : MonoBehaviour
{
    static private int purchaseIndex = 1;
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
        StartCoroutine(HandlePurchaseMessage(purchaseFailed));
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
        StartCoroutine(HandlePurchaseMessage(purchaseFailed));
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
        StartCoroutine(HandlePurchaseMessage(purchaseFailed));
    }

    private IEnumerator HandlePurchaseMessage(bool purchaseFailed, string message = "")
    {
        if (!purchaseFailed)
        {
            UIStore.instance.purchaseMessage.color = Color.green;
            message = "Transaction Success!";
        }
        else
        {
            UIStore.instance.purchaseMessage.color = Color.red;
            message = "Transaction Failed!";
        }
        UIStore.instance.purchaseMessage.text = message;
        UIStore.instance.purchaseMessageObj.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        UIStore.instance.purchaseMessageObj.SetActive(false);
    }

    public void BuyWeapons()
    {
        
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

    private void UnlockWeapon(string weaponName)
    {
        if (WeaponHolder.instance.AddToUnlockedItems(weaponName))
            Debug.Log("Weapon Added");
        else
            Debug.Log("Weapon NOT Added");
        Debug.Log(purchaseIndex);
        WeaponHolder.instance.ArrangeHierarchy(weaponName, purchaseIndex++);
        WeaponHolder.instance.currentChildCount++;
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
    }

    public void PistolDeagleUpgrade()
    {

    }

    public void PistolDualWeildUpgrade()
    {

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
        UnlockWeapon("Shotgun");
        ShotgunPage.instance.FirstTier();
    }

    public void ShotgunAmmo()
    {
        PurchaseAmmo("Shotgun", ShotgunPage.instance.ammoCost, ShotgunPage.instance.ammoQuantity);
    }

    public void ShotgunUpgradeFireRate()
    {
        UpgradeFireRate("Shotgun", ShotgunPage.instance.tier1Upgrade, ShotgunPage.instance.fireRateQuantity);
    }

    public void ShotgunUpgradeDamage()
    {
        UpgradeDamage("Shotgun", ShotgunPage.instance.tier1Upgrade, ShotgunPage.instance.damageQuantity);
    }


    public void ShotgunUpgradeSlug()
    {

    }
    public void ShotgunUpgradeSawedOff()
    {

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
        UnlockWeapon("Rifle");
        RiflePage.instance.FirstTier();
    }
    public void RifleAmmo()
    {
        PurchaseAmmo("Rifle", RiflePage.instance.ammoCost, RiflePage.instance.ammoQuantity);
    }

    public void RifleUpgradeDamage()
    {
        UpgradeDamage("Rifle", RiflePage.instance.tier1Upgrade, RiflePage.instance.damageQuantity);
    }
    public void RifleUpgradeFireRate()
    {
        UpgradeFireRate("Rifle", RiflePage.instance.tier1Upgrade, RiflePage.instance.fireRateQuantity);
    }


    public void RifleBurst()
    {

    }

    public void RifleAssault()
    {

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
        UnlockWeapon("Heavy");
        HeavyPage.instance.FirstTier();
    }

    public void HeavyAmmo()
    {
        PurchaseAmmo("Heavy", HeavyPage.instance.ammoCost, HeavyPage.instance.ammoQuantity);
    }
    public void HeavyUpgradeDamage()
    {
        UpgradeDamage("Heavy", HeavyPage.instance.tier1Upgrade, HeavyPage.instance.damageQuantity);
    }


    public void HeavyUpgradeFireRate()
    {
        UpgradeFireRate("Heavy", HeavyPage.instance.tier1Upgrade, HeavyPage.instance.fireRateQuantity);
    }

    public void HeavyGrenadeLauncher()
    {

    }

    public void HeavyMinigun()
    {

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
