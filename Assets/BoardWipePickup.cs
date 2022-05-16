using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardWipePickup : Pickups
{
    protected override void ItemToIncrement()
    {
        GameManager.instance.boardWipeCount.AddAmmo();
    }
}
