using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopFunctions : MonoBehaviour
{
    public GameObject[] pistolUpgrades;
    public GameObject[] shotgunUpgrades;
    public GameObject[] rifleUpgrades;
    public GameObject[] heavyUpgrades;
    public int price;
    public bool ammoOverride;
    GameObject previous;
    GameObject next;

    public GameObject[] pistolPages;
    public GameObject[] shotgunPages;
    public GameObject[] riflePages;
    public GameObject[] heavyPages;

    private void Start()
    {
        for (int i = 0; i < pistolPages.Length; i++)
        {
            if (PlayerPrefs.GetInt("Pistol Page") == i)
                pistolPages[i].SetActive(true);
            else
                pistolPages[i].SetActive(false);
            
            if (PlayerPrefs.GetInt("Shotgun Page") == i)
                shotgunPages[i].SetActive(true);
            else
                shotgunPages[i].SetActive(false);
            
            if (PlayerPrefs.GetInt("Rifle Page") == i)
                riflePages[i].SetActive(true);
            else
                riflePages[i].SetActive(false);
            
            if (PlayerPrefs.GetInt("Heavy Page") == i)
                heavyPages[i].SetActive(true);
            else
                heavyPages[i].SetActive(false);
        }
    }

    public void BuyPistol(int gunIndex)
    {
        if (GameManager.instance.skrapCount.GetQuantity() <= price)
            return;

        WeaponBase weapon;
        for (int i = 0; i < WeaponHolder.instance.transform.childCount; i++)
        {
            Pistol gun = WeaponHolder.instance.transform.GetChild(i).GetComponent<Pistol>();
            
            if (gun != null)
            {
                int ammo = gun.ammoCount;
                Destroy(WeaponHolder.instance.transform.GetChild(i).gameObject);
                weapon = Instantiate(pistolUpgrades[gunIndex], WeaponHolder.instance.transform).GetComponent<WeaponBase>();
                
                if(!ammoOverride)
                   weapon.ammoCount = ammo;

                GameManager.instance.skrapCount.SetQuantity(GameManager.instance.skrapCount.GetQuantity() - price);
                PlayerPrefs.SetInt("Weapon 0", gunIndex);
                weapon.SetDamageMultiplier(PlayerPrefs.GetFloat("Pistol Damage"));
                weapon.SetFireRateMultiplier(PlayerPrefs.GetFloat("Pistol Fire Rate"));
                next.SetActive(true);
                previous.SetActive(false);
                return;
            }
        }
        weapon = Instantiate(pistolUpgrades[gunIndex], WeaponHolder.instance.transform).GetComponent<WeaponBase>();
        GameManager.instance.skrapCount.SetQuantity(GameManager.instance.skrapCount.GetQuantity() - price);
        PlayerPrefs.SetInt("Weapon 0", gunIndex);
        weapon.SetDamageMultiplier(PlayerPrefs.GetFloat("Pistol Damage"));
        weapon.SetFireRateMultiplier(PlayerPrefs.GetFloat("Pistol Fire Rate"));
        next.SetActive(true);
        previous.SetActive(false);
    }

    public void BuyShotgun(int gunIndex)
    {
        if (GameManager.instance.skrapCount.GetQuantity() <= price)
            return;

        WeaponBase weapon;
        for (int i = 0; i < WeaponHolder.instance.transform.childCount; i++)
        {
            Shotgun gun = WeaponHolder.instance.transform.GetChild(i).GetComponent<Shotgun>();

            if (gun != null)
            {
                int ammo = gun.ammoCount;
                Destroy(WeaponHolder.instance.transform.GetChild(i).gameObject);
                weapon = Instantiate(shotgunUpgrades[gunIndex], WeaponHolder.instance.transform).GetComponent<WeaponBase>();

                if (!ammoOverride)
                    weapon.ammoCount = ammo;

                GameManager.instance.skrapCount.SetQuantity(GameManager.instance.skrapCount.GetQuantity() - price);
                PlayerPrefs.SetInt("Weapon 1", gunIndex);
                weapon.SetDamageMultiplier(PlayerPrefs.GetFloat("Shotgun Damage"));
                weapon.SetFireRateMultiplier(PlayerPrefs.GetFloat("Shotgun Fire Rate"));
                next.SetActive(true);
                previous.SetActive(false);
                return;
            }
        }
        weapon = Instantiate(shotgunUpgrades[gunIndex], WeaponHolder.instance.transform).GetComponent<WeaponBase>();
        GameManager.instance.skrapCount.SetQuantity(GameManager.instance.skrapCount.GetQuantity() - price);
        PlayerPrefs.SetInt("Weapon 1", gunIndex);
        weapon.SetDamageMultiplier(PlayerPrefs.GetFloat("Shotgun Damage"));
        weapon.SetFireRateMultiplier(PlayerPrefs.GetFloat("Shotgun Fire Rate"));
        next.SetActive(true);
        previous.SetActive(false);
    }

    public void BuyRifle(int gunIndex)
    {
        if (GameManager.instance.skrapCount.GetQuantity() <= price)
            return;

        WeaponBase weapon;
        for (int i = 0; i < WeaponHolder.instance.transform.childCount; i++)
        {
            Rifle gun = WeaponHolder.instance.transform.GetChild(i).GetComponent<Rifle>();

            if (gun != null)
            {
                int ammo = gun.ammoCount;
                Destroy(WeaponHolder.instance.transform.GetChild(i).gameObject);
                weapon = Instantiate(rifleUpgrades[gunIndex], WeaponHolder.instance.transform).GetComponent<WeaponBase>();

                if (!ammoOverride)
                    weapon.ammoCount = ammo;

                GameManager.instance.skrapCount.SetQuantity(GameManager.instance.skrapCount.GetQuantity() - price);
                PlayerPrefs.SetInt("Weapon 2", gunIndex);
                weapon.SetDamageMultiplier(PlayerPrefs.GetFloat("Rifle Damage"));
                weapon.SetFireRateMultiplier(PlayerPrefs.GetFloat("Rifle Fire Rate"));
                next.SetActive(true);
                previous.SetActive(false);
                return;
            }
        }
        weapon = Instantiate(rifleUpgrades[gunIndex], WeaponHolder.instance.transform).GetComponent<WeaponBase>();
        GameManager.instance.skrapCount.SetQuantity(GameManager.instance.skrapCount.GetQuantity() - price);
        PlayerPrefs.SetInt("Weapon 2", gunIndex);
        weapon.SetDamageMultiplier(PlayerPrefs.GetFloat("Rifle Damage"));
        weapon.SetFireRateMultiplier(PlayerPrefs.GetFloat("Rifle Fire Rate"));
        next.SetActive(true);
        previous.SetActive(false);
    }

    public void BuyHeavy(int gunIndex)
    {
        if (GameManager.instance.skrapCount.GetQuantity() <= price)
            return;

        WeaponBase weapon;
        for (int i = 0; i < WeaponHolder.instance.transform.childCount; i++)
        {
            Heavy gun = WeaponHolder.instance.transform.GetChild(i).GetComponent<Heavy>();

            if (gun != null)
            {
                int ammo = gun.ammoCount;
                Destroy(WeaponHolder.instance.transform.GetChild(i).gameObject);
                weapon = Instantiate(heavyUpgrades[gunIndex], WeaponHolder.instance.transform).GetComponent<WeaponBase>();

                if (!ammoOverride)
                    weapon.ammoCount = ammo;

                GameManager.instance.skrapCount.SetQuantity(GameManager.instance.skrapCount.GetQuantity() - price);
                PlayerPrefs.SetInt("Weapon 3", gunIndex);
                weapon.SetDamageMultiplier(PlayerPrefs.GetFloat("Heavy Damage"));
                weapon.SetFireRateMultiplier(PlayerPrefs.GetFloat("Heavy Fire Rate"));
                next.SetActive(true);
                previous.SetActive(false);
                return;
            }
        }
        weapon = Instantiate(heavyUpgrades[gunIndex], WeaponHolder.instance.transform).GetComponent<WeaponBase>();
        GameManager.instance.skrapCount.SetQuantity(GameManager.instance.skrapCount.GetQuantity() - price);
        PlayerPrefs.SetInt("Weapon 3", gunIndex);
        weapon.SetDamageMultiplier(PlayerPrefs.GetFloat("Heavy Damage"));
        weapon.SetFireRateMultiplier(PlayerPrefs.GetFloat("Heavy Fire Rate"));
        next.SetActive(true);
        previous.SetActive(false);
    }

    public void SetPrice(int price)
    {
        this.price = price;
    }
    public void SetAmmoOverride(bool ammoOverride)
    {
        this.ammoOverride = ammoOverride;
    }

    public void UpgradeFireRatePistol(float newFireRate)
    {
        if (GameManager.instance.skrapCount.GetQuantity() <= price)
            return;

        for (int i = 0; i < WeaponHolder.instance.transform.childCount; i++)
        {
            Pistol gun = WeaponHolder.instance.transform.GetChild(i).GetComponent<Pistol>();
            if (gun != null)
            {
                gun.SetFireRateMultiplier(newFireRate);
                GameManager.instance.skrapCount.SetQuantity(GameManager.instance.skrapCount.GetQuantity() - price);
                PlayerPrefs.SetFloat("Pistol Fire Rate", newFireRate);
                next.SetActive(true);
                previous.SetActive(false);
                return;
            }
        }
    }

    public void UpgradeFireRateShotgun(float newFireRate)
    {
        if (GameManager.instance.skrapCount.GetQuantity() <= price)
            return;

        for (int i = 0; i < WeaponHolder.instance.transform.childCount; i++)
        {
            Shotgun gun = WeaponHolder.instance.transform.GetChild(i).GetComponent<Shotgun>();
            if (gun != null)
            {
                gun.SetFireRateMultiplier(newFireRate);
                GameManager.instance.skrapCount.SetQuantity(GameManager.instance.skrapCount.GetQuantity() - price);
                PlayerPrefs.SetFloat("Shotgun Fire Rate", newFireRate);
                next.SetActive(true);
                previous.SetActive(false);
                return;
            }
        }
    }
    public void UpgradeFireRateRifle(float newFireRate)
    {
        if (GameManager.instance.skrapCount.GetQuantity() <= price)
            return;

        for (int i = 0; i < WeaponHolder.instance.transform.childCount; i++)
        {
            Rifle gun = WeaponHolder.instance.transform.GetChild(i).GetComponent<Rifle>();
            if (gun != null)
            {
                gun.SetFireRateMultiplier(newFireRate);
                GameManager.instance.skrapCount.SetQuantity(GameManager.instance.skrapCount.GetQuantity() - price);
                PlayerPrefs.SetFloat("Rifle Fire Rate", newFireRate);
                next.SetActive(true);
                previous.SetActive(false);
                return;
            }
        }
    }
    public void UpgradeFireRateHeavy(float newFireRate)
    {
        if (GameManager.instance.skrapCount.GetQuantity() <= price)
            return;

        for (int i = 0; i < WeaponHolder.instance.transform.childCount; i++)
        {
            Heavy gun = WeaponHolder.instance.transform.GetChild(i).GetComponent<Heavy>();
            if (gun != null)
            {
                gun.SetFireRateMultiplier(newFireRate);
                GameManager.instance.skrapCount.SetQuantity(GameManager.instance.skrapCount.GetQuantity() - price);
                PlayerPrefs.SetFloat("Heavy Fire Rate", newFireRate);
                next.SetActive(true);
                previous.SetActive(false);
                return;
            }
        }
    }
    public void UpgradeDamagePistol(float newDamage)
    {
        if (GameManager.instance.skrapCount.GetQuantity() <= price)
            return;

        for (int i = 0; i < WeaponHolder.instance.transform.childCount; i++)
        {
            Pistol gun = WeaponHolder.instance.transform.GetChild(i).GetComponent<Pistol>();
            if (gun != null)
            {
                gun.SetFireRateMultiplier(newDamage);
                GameManager.instance.skrapCount.SetQuantity(GameManager.instance.skrapCount.GetQuantity() - price);
                PlayerPrefs.SetFloat("Pistol Damage", newDamage);
                next.SetActive(true);
                previous.SetActive(false);
                return;
            }
        }
    }
    public void UpgradeDamageShotgun(float newDamage)
    {
        if (GameManager.instance.skrapCount.GetQuantity() <= price)
            return;

        for (int i = 0; i < WeaponHolder.instance.transform.childCount; i++)
        {
            Shotgun gun = WeaponHolder.instance.transform.GetChild(i).GetComponent<Shotgun>();
            if (gun != null)
            {
                gun.SetFireRateMultiplier(newDamage);
                GameManager.instance.skrapCount.SetQuantity(GameManager.instance.skrapCount.GetQuantity() - price);
                PlayerPrefs.SetFloat("Shotgun Damage", newDamage);
                next.SetActive(true);
                previous.SetActive(false);
                return;
            }
        }
    }
    public void UpgradeDamageRifle(float newDamage)
    {
        if (GameManager.instance.skrapCount.GetQuantity() <= price)
            return;

        for (int i = 0; i < WeaponHolder.instance.transform.childCount; i++)
        {
            Rifle gun = WeaponHolder.instance.transform.GetChild(i).GetComponent<Rifle>();
            if (gun != null)
            {
                gun.SetFireRateMultiplier(newDamage);
                GameManager.instance.skrapCount.SetQuantity(GameManager.instance.skrapCount.GetQuantity() - price);
                PlayerPrefs.SetFloat("Rifle Damage", newDamage);
                next.SetActive(true);
                previous.SetActive(false);
                return;
            }
        }
    }
    public void UpgradeDamageHeavy(float newDamage)
    {
        if (GameManager.instance.skrapCount.GetQuantity() <= price)
            return;

        for (int i = 0; i < WeaponHolder.instance.transform.childCount; i++)
        {
            Heavy gun = WeaponHolder.instance.transform.GetChild(i).GetComponent<Heavy>();
            if (gun != null)
            {
                gun.SetFireRateMultiplier(newDamage);
                GameManager.instance.skrapCount.SetQuantity(GameManager.instance.skrapCount.GetQuantity() - price);
                PlayerPrefs.SetFloat("Heavy Damage", newDamage);
                next.SetActive(true);
                previous.SetActive(false);
                return;
            }
        }
    }

    public void SavePages()
    {
        for (int i = 0; i < pistolPages.Length; i++)
        {
            if (pistolPages[i].activeInHierarchy)
                PlayerPrefs.SetInt("Pistol Page", i);
            if (shotgunPages[i].activeInHierarchy)
                PlayerPrefs.SetInt("Shotgun Page", i);
            if (riflePages[i].activeInHierarchy)
                PlayerPrefs.SetInt("Rifle Page", i);
            if (heavyPages[i].activeInHierarchy)
                PlayerPrefs.SetInt("Heavy Page", i);
        }
    }

    public void ClosePrevious(GameObject previous)
    {
        this.previous = previous;
    }
    public void OpenNext(GameObject next)
    {
        this.next = next;
    }
}
