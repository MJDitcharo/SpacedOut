using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStore : PopUpMenu
{

    [SerializeField]
    private GameObject slotParent;
    [SerializeField]
    private GameObject storeVisual;

    private enum PickupCosts { Health, Ammo, Grenade, BoardWipe, Armor };

    private void Start()
    {
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

}