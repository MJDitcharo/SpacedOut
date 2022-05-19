using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickups
{
    float health;
    protected override void Increment()
    {
        GameManager.instance.playerHealth.AddHealth(health);
    }
    protected override void InitialValues()
    {
        drop.ItemName = "Health x " + health;
        health = quantity * .01f;
    }
}
