using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkrapPickup : Pickups
{
    protected override void Increment()
    {
        GameManager.instance.skrapCount.Add(quantity); 
    }
    protected override void InitialValues()
    {
        drop.ItemName = "Skrap x " + quantity.ToString();
    }
}
