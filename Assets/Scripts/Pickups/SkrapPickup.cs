using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkrapPickup : Pickups
{
    protected override void Increment()
    {
        Debug.Log("Max: " + quantity);
        GameManager.instance.skrapCount.Add(quantity);
    }
}
