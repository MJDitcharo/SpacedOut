using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : Pickups
{
    private void Start()
    {
        itemStr = "Grenade x " + quantity.ToString();
    }
    override protected void Increment()
    {
        GameManager.instance.grenadeCount.Add(quantity);
    }
}
