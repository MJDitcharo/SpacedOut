using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkrapPickup : Pickups
{
    private void Start()
    {
        itemStr = "Skrap x " + quantity.ToString();
    }
    protected override void Increment()
    {
        Debug.Log("Max: " + quantity);
        GameManager.instance.skrapCount.Add(quantity);
    }

}
