using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardWipePickup : Pickups
{
    private void Start()
    {
        itemStr = "Board Wipe x " + quantity.ToString();
    }
    protected override void Increment()
    {
        GameManager.instance.boardWipeCount.Add(quantity);
    }
}
