using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStore : PopUpMenu
{

    [SerializeField]
    private List<GameObject> pages;
    private List<Dictionary<TMPro.TextMeshProUGUI, int>> thing;
    [SerializeField]
    private GameObject storeVisual;

    //pickup page
    protected enum PickupCosts { Health, Ammo, Grenade, BoardWipe, Armor };
    protected int[] defaultPickupCosts = { 200, 200, 300, 1500, 100 };

    protected Dictionary<TMPro.TextMeshProUGUI, int> pickups;
    protected enum WeaponUpgradeCosts { Pistol, Shotgun, Heavy, Rifle, Melee };
    protected int[] defaultWeaponUpgradeCosts = { 200, 200, 300, 1500, 100 };
    protected Dictionary<TMPro.TextMeshProUGUI, int> weaponUpgrades;
    

    protected bool purchaseFailed = false;

    private void Start()
    {

        //add all 

        int i = 0;
        foreach (Transform transform in pages[i].transform) //get to the children of the page
        {
            int j = 0;
            foreach (Transform cost in transform) //get to the child of the page
            {
                if (cost.parent.name == "Pickups Page")
                    pickups.Add(cost.GetComponent<TMPro.TextMeshProUGUI>(), defaultPickupCosts[j]);
                else if (cost.parent.name == "Weapon Upgrade Page")
                    weaponUpgrades.Add(cost.GetComponent<TMPro.TextMeshProUGUI>(), defaultWeaponUpgradeCosts[j]);
                j++;
            }
            i++;
        }
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
        GameObject.Find("Weapon Upgrades Page").gameObject.SetActive(false);
        FreezeWorld();
    }
    public void Deactivate()
    {
        storeVisual.SetActive(false);
        foreach (Transform transform in storeVisual.transform)
            transform.gameObject.SetActive(false);
        UnfreezeWorld();
    }

    private void ShowPurchaseFailed()
    {

    }

}