using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgradePage : StorePage
{
    // Start is called before the first frame update
    bool weaponOwned; //see if player has bought the weapon


    void Start()
    {
        SetInitialPrices();
    }

    protected override void SetInitialPrices()
    {
        
    }

}
