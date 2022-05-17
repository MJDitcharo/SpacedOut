using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickups
{
    float health;
    private void Start()
    {
        health = quantity * .01f;
        itemStr = "Health x " + health;
    }

    protected override void Increment()
    {
        GameManager.instance.playerHealth.AddHealth(health);
    }
}
