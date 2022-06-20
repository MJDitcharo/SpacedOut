using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStoreButtons : MonoBehaviour
{
    static public int purchaseIndex = 1;
    private string plasma = " Plasma";
    private string voidUpgrade = " Void";
    [SerializeField] GameObject closePage;
    [SerializeField] GameObject openPage;

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
        string message = "";
        if (GameManager.instance.skrapCount.GetQuantity() >= cost) //check for money
        {
            if(GameManager.instance.ammoCount.GetQuantity() == GameManager.instance.ammoCount.GetMaximumQuantity()) //check for max ammo already owned
            {
                message = "Already have max ammo!";
                purchaseFailed = true;
            }
            else
            {
                WeaponBase currentWeapon = WeaponHolder.instance.transform.Find(weaponName).GetComponent<WeaponBase>();
                //GameManager.instance.ammoCount.Add(quantity);
                currentWeapon.AddAmmo(quantity);
                GameManager.instance.skrapCount.Subtract(cost);
                purchaseFailed = false;
            }
        }
        else
            purchaseFailed = true;
        StartCoroutine(UIStore.instance.HandlePurchaseMessage(purchaseFailed, message));
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

    /*private bool UpgradeFireRate(string weaponName, int cost, float fireRate)
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
        return purchaseFailed;
    }

    private bool UpgradeDamage(string weaponName, int cost, float damage)
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
        return purchaseFailed;
    }*/



    /*public bool Tier2Upgrade(string baseWeapon, string tier2Weapon, int cost)
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
        return purchaseFailed;
    }

    private void UnlockWeapon(string weaponName)
    {
        if (WeaponHolder.instance.AddToUnlockedItems(weaponName))
            Debug.Log("Weapon Unlocked");
        else
            Debug.Log("Weapon NOT Unlocked");
        WeaponHolder.instance.ArrangeHierarchy(weaponName, purchaseIndex++);
    }*/

    #region Shop Buttons
    public void ExitShop()
    {
        GameManager.instance.SetFightingCursor();
        UIStore.instance.purchaseMessageObj.SetActive(false);
        UIStore.instance.Deactivate();
        //show prompt for entering the store
        GameManager.instance.prompt.ShowPrompt("Press F To Enter");
    }
    #region PickupButtons
    // Allows you to buy anything in the shop taking away the proper amount of skrap while adding the item
    public void BuyBoardWipe()
    {
        CheckPurchaseItem(GameManager.instance.boardWipeCount, GeneralPage.instance.boardWipe);
        GameManager.instance.SaveGame();
    }
    public void BuyGrenade()
    {
        CheckPurchaseItem(GameManager.instance.grenadeCount, GeneralPage.instance.grenade);
    }
    public void BuyHealth()
    {
        bool purchaseFailed;
        if (GameManager.instance.skrapCount.GetQuantity() >= GeneralPage.instance.healthCost && GameManager.instance.playerHealth.currHealth < GameManager.instance.playerHealth.maxHealth)
        {
            GameManager.instance.playerHealth.AddHealth((float)(GeneralPage.instance.healthQuantity * .01f));
            GameManager.instance.skrapCount.Subtract(GeneralPage.instance.healthCost);
            purchaseFailed = false;
            UIStore.instance.StartMessageCoroutine(purchaseFailed);
        }
        else if (GameManager.instance.playerHealth.currHealth >= GameManager.instance.playerHealth.maxHealth)
        {
            purchaseFailed = true;
            UIStore.instance.StartMessageCoroutine(purchaseFailed, "At Max health");
        }
        else
        {
            purchaseFailed = true;
            UIStore.instance.StartMessageCoroutine(purchaseFailed);
        }
        
        GameManager.instance.SaveGame();
    }

    public void BuyMaxHealth()
    {
        bool purchaseFailed;
        if (GameManager.instance.skrapCount.GetQuantity() >= GeneralPage.instance.maxHealth)
        {
            GameManager.instance.healthBar.IncreaseMaxHealth();
            GameManager.instance.skrapCount.Subtract(GeneralPage.instance.maxHealth);
            purchaseFailed = false;
        }
        else
            purchaseFailed = true;
        UIStore.instance.StartMessageCoroutine(purchaseFailed);
        GameManager.instance.SaveGame();
    }



    #endregion

    /*
    #region WeaponButtons

    #region Pistol Page
    public void PistolAmmo()
    {
        PurchaseAmmo("Pistol", PistolPage.instance.ammoCost, PistolPage.instance.ammoQuantity);
        GameManager.instance.SaveGame();
    }

    public void PistolUpgradeFireRate()
    {
        bool purchaseFailed = UpgradeFireRate("Pistol", PistolPage.instance.tier1Upgrade, PistolPage.instance.fireRateQuantity);
        if (!purchaseFailed)
            PistolPage.instance.NextTier();
        GameManager.instance.SaveGame();
    }

    public void PistolUpgradeDamage()
    {
        bool purchaseFailed = UpgradeDamage("Pistol", PistolPage.instance.tier1Upgrade, PistolPage.instance.damageQuantity);
        if (!purchaseFailed)
            PistolPage.instance.NextTier();
        GameManager.instance.SaveGame();
    }

    public void PistolDeagleUpgrade()
    {
        bool purchaseFailed = Tier2Upgrade("Pistol", "Deagle", PistolPage.instance.tier2Upgrade);
        if (!purchaseFailed)
            PistolPage.instance.NextTier();
        PistolPage.instance.SetTier2Choice("Deagle");
        GameManager.instance.SaveGame();
        WeaponHolder.instance.SwitchWeapons(WeaponBase.WeaponID.Pistol);
    }

    public void PistolDualWieldUpgrade()
    {
        bool purchaseFailed = Tier2Upgrade("Pistol", "Dual Wield", PistolPage.instance.tier2Upgrade);
        if (!purchaseFailed)
        {
            PistolPage.instance.NextTier();
            PistolPage.instance.SetTier2Choice("Dual Wield");
        }
        GameManager.instance.SaveGame();
        WeaponHolder.instance.SwitchWeapons(WeaponBase.WeaponID.Pistol);
    }
    public void PistolVoidUpgrade()
    {
        bool purchaseFailed = Tier2Upgrade(PistolPage.instance.GetTeir2Choice(), PistolPage.instance.GetTeir2Choice() + voidUpgrade, PistolPage.instance.tier3Upgrade);
        if (!purchaseFailed)
            PistolPage.instance.NextTier();
        GameManager.instance.SaveGame();
        WeaponHolder.instance.SwitchWeapons(WeaponBase.WeaponID.Pistol);
    }
    public void PistolPlasmaUpgrade()
    {
        bool purchaseFailed = Tier2Upgrade(PistolPage.instance.GetTeir2Choice(), PistolPage.instance.GetTeir2Choice() + plasma, PistolPage.instance.tier3Upgrade);
        if (!purchaseFailed)
            PistolPage.instance.NextTier();
        GameManager.instance.SaveGame();
        WeaponHolder.instance.SwitchWeapons(WeaponBase.WeaponID.Pistol);
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
            WeaponHolder.instance.currentChildCount++;
        }
        else
            purchaseFailed = true;
        StartCoroutine(UIStore.instance.HandlePurchaseMessage(purchaseFailed));
        if (!purchaseFailed)
            ShotgunPage.instance.NextTier();
        GameManager.instance.SaveGame();
        WeaponHolder.instance.SwitchWeapons(WeaponBase.WeaponID.Shotgun);
    }

    public void ShotgunAmmo()
    {
        PurchaseAmmo("Shotgun", ShotgunPage.instance.ammoCost, ShotgunPage.instance.ammoQuantity);
        GameManager.instance.SaveGame();
    }

    public void ShotgunUpgradeFireRate()
    {
        bool purchaseFailed = UpgradeFireRate("Shotgun", ShotgunPage.instance.tier1Upgrade, ShotgunPage.instance.fireRateQuantity);
        if (!purchaseFailed)
            ShotgunPage.instance.NextTier();
        GameManager.instance.SaveGame();
    }

    public void ShotgunUpgradeDamage()
    {
        bool purchaseFailed = UpgradeDamage("Shotgun", ShotgunPage.instance.tier1Upgrade, ShotgunPage.instance.damageQuantity);
        if (!purchaseFailed)
            ShotgunPage.instance.NextTier();
        GameManager.instance.SaveGame();
    }

    public void ShotgunUpgradeSlug()
    {
        bool purchaseFailed = Tier2Upgrade("Shotgun", "Slug", ShotgunPage.instance.tier2Upgrade);
        if (!purchaseFailed)
        {
            ShotgunPage.instance.NextTier();
            ShotgunPage.instance.SetTier2Choice("Slug");
        }
        GameManager.instance.SaveGame();
        WeaponHolder.instance.SwitchWeapons(WeaponBase.WeaponID.Shotgun);
    }
    public void ShotgunUpgradeSawedOff()
    {
        bool purchaseFailed = Tier2Upgrade("Shotgun", "Sawed-Off", ShotgunPage.instance.tier2Upgrade);
        if (!purchaseFailed)
        {
            ShotgunPage.instance.NextTier();
            ShotgunPage.instance.SetTier2Choice("Sawed-Off");
        }
        GameManager.instance.SaveGame();
        WeaponHolder.instance.SwitchWeapons(WeaponBase.WeaponID.Shotgun);
    }
    public void ShotgunPlasmaUpgrade()
    {
        bool purchaseFailed = Tier2Upgrade(ShotgunPage.instance.GetTeir2Choice(), ShotgunPage.instance.GetTeir2Choice() + plasma, ShotgunPage.instance.tier3Upgrade);
        if (!purchaseFailed)
            ShotgunPage.instance.NextTier();
        GameManager.instance.SaveGame();
        WeaponHolder.instance.SwitchWeapons(WeaponBase.WeaponID.Shotgun);
    }

    public void ShotgunVoidUpgrade()
    {
        bool purchaseFailed = Tier2Upgrade(ShotgunPage.instance.GetTeir2Choice(), ShotgunPage.instance.GetTeir2Choice() + voidUpgrade, ShotgunPage.instance.tier3Upgrade);
        if (!purchaseFailed)
            ShotgunPage.instance.NextTier();
        GameManager.instance.SaveGame();
        WeaponHolder.instance.SwitchWeapons(WeaponBase.WeaponID.Shotgun);
    }
    #endregion

    #region Rifle Page

    public void RifleBuyWeapon()
    {
        bool purchaseFailed;
        string message = "";
        if (GameManager.instance.skrapCount.GetQuantity() >= RiflePage.instance.weaponCost)
        {
            UnlockWeapon("Rifle");
            GameManager.instance.skrapCount.Subtract(RiflePage.instance.weaponCost);
            purchaseFailed = false;
            WeaponHolder.instance.currentChildCount++;
        }
        else
        {
            if (GameManager.instance.skrapCount.GetQuantity() < RiflePage.instance.weaponCost)
                message = "Not enough skrap!";
            purchaseFailed = true;
        }
        StartCoroutine(UIStore.instance.HandlePurchaseMessage(purchaseFailed, message));
        if (!purchaseFailed)
            RiflePage.instance.NextTier();
        GameManager.instance.SaveGame();
        WeaponHolder.instance.SwitchWeapons(WeaponBase.WeaponID.Rifle);
    }
    public void RifleAmmo()
    {
        PurchaseAmmo("Rifle", RiflePage.instance.ammoCost, RiflePage.instance.ammoQuantity);
        GameManager.instance.SaveGame();
    }

    public void RifleUpgradeDamage()
    {
        bool purchaseFailed = UpgradeDamage("Rifle", RiflePage.instance.tier1Upgrade, RiflePage.instance.damageQuantity);
        if (!purchaseFailed)
            RiflePage.instance.NextTier();
        GameManager.instance.SaveGame();
    }
    public void RifleUpgradeFireRate()
    {
        bool purchaseFailed = UpgradeFireRate("Rifle", RiflePage.instance.tier1Upgrade, RiflePage.instance.fireRateQuantity);
        if (!purchaseFailed)
            RiflePage.instance.NextTier();
        GameManager.instance.SaveGame();
    }


    public void RifleBurst()
    {
        bool purchaseFailed = Tier2Upgrade("Rifle", "Burst", RiflePage.instance.tier2Upgrade);
        if (!purchaseFailed)
        {
            RiflePage.instance.NextTier();
            RiflePage.instance.SetTier2Choice("Burst");
        }
        GameManager.instance.SaveGame();
        WeaponHolder.instance.SwitchWeapons(WeaponBase.WeaponID.Rifle);
    }

    public void RifleAssault()
    {
        bool purchaseFailed = Tier2Upgrade("Rifle", "Assault", RiflePage.instance.tier2Upgrade);
        if (!purchaseFailed)
        {
            RiflePage.instance.NextTier();
            RiflePage.instance.SetTier2Choice("Assault");
        }
        GameManager.instance.SaveGame();
        WeaponHolder.instance.SwitchWeapons(WeaponBase.WeaponID.Rifle);
    }

    public void RiflePlasmaUpgrade()
    {
        bool purchaseFailed = Tier2Upgrade(RiflePage.instance.GetTeir2Choice(), RiflePage.instance.GetTeir2Choice() + plasma, RiflePage.instance.tier3Upgrade);
        if (!purchaseFailed)
            RiflePage.instance.NextTier();
        GameManager.instance.SaveGame();
        WeaponHolder.instance.SwitchWeapons(WeaponBase.WeaponID.Rifle);
    }
    public void RifleVoidUpgrade()
    {
        bool purchaseFailed = Tier2Upgrade(RiflePage.instance.GetTeir2Choice(), RiflePage.instance.GetTeir2Choice() + voidUpgrade, RiflePage.instance.tier3Upgrade);
        if (!purchaseFailed)
            RiflePage.instance.NextTier();
        GameManager.instance.SaveGame();
        WeaponHolder.instance.SwitchWeapons(WeaponBase.WeaponID.Rifle);
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
            WeaponHolder.instance.currentChildCount++;
        }
        else
            purchaseFailed = true;
        StartCoroutine(UIStore.instance.HandlePurchaseMessage(purchaseFailed));
        if (!purchaseFailed)
            HeavyPage.instance.NextTier();
        GameManager.instance.SaveGame();
        WeaponHolder.instance.SwitchWeapons(WeaponBase.WeaponID.Heavy);
    }

    public void HeavyAmmo()
    {
        PurchaseAmmo("Heavy", HeavyPage.instance.ammoCost, HeavyPage.instance.ammoQuantity);
        GameManager.instance.SaveGame();
    }
    public void HeavyUpgradeDamage()
    {
        bool purchaseFailed = UpgradeDamage("Heavy", HeavyPage.instance.tier1Upgrade, HeavyPage.instance.damageQuantity);
        if (!purchaseFailed)
            HeavyPage.instance.NextTier();
        GameManager.instance.SaveGame();
    }


    public void HeavyUpgradeFireRate()
    {
        bool purchaseFailed = UpgradeFireRate("Heavy", HeavyPage.instance.tier1Upgrade, HeavyPage.instance.fireRateQuantity);
        if (!purchaseFailed)
            HeavyPage.instance.NextTier();
        GameManager.instance.SaveGame();
    }

    public void HeavyGrenadeLauncher()
    {
        bool purchaseFailed = Tier2Upgrade("Heavy", "Grenade Launcher", HeavyPage.instance.tier2Upgrade);
        if (!purchaseFailed)
        {
            HeavyPage.instance.NextTier();
            HeavyPage.instance.SetTier2Choice("Grenade Launcher");
        }
        GameManager.instance.SaveGame();
        WeaponHolder.instance.SwitchWeapons(WeaponBase.WeaponID.Heavy);
    }

    public void HeavyMinigun()
    {
        bool purchaseFailed = Tier2Upgrade("Heavy", "Minigun", HeavyPage.instance.tier2Upgrade);
        if (!purchaseFailed)
        {
            HeavyPage.instance.NextTier();
            HeavyPage.instance.SetTier2Choice("Minigun");
        }
        GameManager.instance.SaveGame();
        WeaponHolder.instance.SwitchWeapons(WeaponBase.WeaponID.Heavy);
    }

    public void HeavyVoidUpgrade()
    {
        bool purchaseFailed = Tier2Upgrade(HeavyPage.instance.GetTeir2Choice(), HeavyPage.instance.GetTeir2Choice() + voidUpgrade, HeavyPage.instance.tier3Upgrade);
        if (!purchaseFailed)
            HeavyPage.instance.NextTier();
        GameManager.instance.SaveGame();
        WeaponHolder.instance.SwitchWeapons(WeaponBase.WeaponID.Heavy);
    }

    public void HeavyPlasmaUpgrade()
    {
        bool purchaseFailed = Tier2Upgrade(HeavyPage.instance.GetTeir2Choice(), HeavyPage.instance.GetTeir2Choice() + plasma, HeavyPage.instance.tier3Upgrade); ;
        if (!purchaseFailed)
            HeavyPage.instance.NextTier();
        GameManager.instance.SaveGame();
        WeaponHolder.instance.SwitchWeapons(WeaponBase.WeaponID.Heavy);
    }
    #endregion
    #endregion
    */
    #endregion
    



}
