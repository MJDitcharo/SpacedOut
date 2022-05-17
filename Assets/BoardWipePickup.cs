using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardWipePickup : Pickups
{
    protected override void Increment()
    {
        GameManager.instance.boardWipeCount.Add(quantity);
    }
}
