using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : Pickups
{
    override protected void ItemToIncrement()
    {
        GameManager.instance.grenadeCount.Add(quantity);
    }
}
