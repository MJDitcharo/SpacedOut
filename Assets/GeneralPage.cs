using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPage : StorePage
{
    public int health, maxHealth, grenade, boardWipe;
    public int healthQuantity;
    public static GeneralPage instance;

    private void Awake()
    {
        instance = this;
        healthQuantity = 25;
    }
    protected override void SetInitialPrices()
    {
        pricesInt.Add(health);
        pricesInt.Add(maxHealth);
        pricesInt.Add(grenade);
        pricesInt.Add(boardWipe);
    }


}