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
        foreach(Transform transform in storeVisual.transform)
        {
            if (transform.name != "Weapon Upgrades Page")
                transform.gameObject.SetActive(true);
        }
    }

    public void Activate()
    {
        storeVisual.SetActive(true);
        FreezeWorld();
    }

    public void Deactivate()
    {
        storeVisual.SetActive(false);
        UnfreezeWorld();
    }

}