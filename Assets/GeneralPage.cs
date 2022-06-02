using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPage : StorePage
{
    [SerializeField]
    int health, maxHealth, grenade, boardWipe;

    protected override void SetInitialPrices()
    {
        pricesInt.Add(health);
        pricesInt.Add(maxHealth);
        pricesInt.Add(grenade);
        pricesInt.Add(boardWipe);
    }
}