using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolDualWield : Pistol
{

    public override void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint[firePointIndex].position, firePoint[firePointIndex].rotation); //spawn the bullet and reference the bullet to modify 
        Rigidbody rb = bullet.GetComponent<Rigidbody>(); //acess the rigidbody of the game object
        rb.AddForce(firePoint[firePointIndex].forward * bulletForce, ForceMode.Impulse); //add a force in the up vector
        GameManager.instance.bullets.Add(bullet);
        bullet.GetComponent<bullet>().damage = (int)(damage * damageMultiplier);
        firePointIndex++;

        ammoCount--;
        UpdateVisual();
        if (firePointIndex >= firePoint.Length)
            firePointIndex = 0;
        AudioManager.Instance.PlaySFX(gunshotSound);
    }
}
