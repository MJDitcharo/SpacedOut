using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : WeaponBase
{
    //uses the base Update function
    private void Awake()
    {
        weaponID = WeaponID.Pistol;
    }

    public override void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextShotFired && ammoCount != 0 && Time.timeScale > 0) //if the first mouse button is down
        {
            nextShotFired = Time.time + 1f / fireRate * fireRateMultiplier; //delay for the next bullet fired
            Shoot(); //shoot method
        }
    }
}
