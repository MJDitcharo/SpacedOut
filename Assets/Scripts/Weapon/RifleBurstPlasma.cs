using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBurstPlasma : RifleBurst
{
    bool gunIsFired;
    // Update is called once per frame
    public override void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextShotFired && ammoCount != 0 && Time.timeScale > 0) //if the first mouse button is down
        {
            nextShotFired = Time.time + 1f / fireRate / fireRateMultiplier; //delay for the next bullet fired
            Shoot(); //shoot method
        }
    }
    public override void Shoot()
    {
        base.Shoot();
    }
}
