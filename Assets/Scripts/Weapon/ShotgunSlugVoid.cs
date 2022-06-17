using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunSlugVoid : Shotgun
{

    public override void Shoot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint[firePointIndex].position, Quaternion.identity); //spawn the bullet and reference the bullet to modify 
        Rigidbody rb = bulletInstance.GetComponent<Rigidbody>(); //acess the rigidbody of the game object

        rb.velocity = (firePoint[firePointIndex].forward * bulletForce); //add a force in the up vector


        ammoCount--;
        AudioManager.Instance.PlaySFX(gunshotSound);
    }
}
