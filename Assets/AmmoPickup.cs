using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickups
{
    // Start is called before the first frame update
    override protected void ItemToIncrement()
    {
        GameManager.instance.ammoCount.Add(quantity);
    }
}
