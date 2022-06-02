using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heavy : WeaponBase
{
    [SerializeField] float explosiveRadius = 2;
    [SerializeField] float knockback = 1;

    private void Awake()
    {
        weaponID = WeaponID.Heavy;
    }

    public override void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint[firePointIndex].position, firePoint[firePointIndex].rotation); //spawn the bullet and reference the bullet to modify 
        Rigidbody rb = bullet.GetComponent<Rigidbody>(); //acess the rigidbody of the game object
        bullet bulletScript = bullet.GetComponent<bullet>();
        bulletScript.damage = (int)(damage * damageMultiplier);
        bulletScript.explosiveRadius = explosiveRadius;
        rb.AddForce(firePoint[firePointIndex].forward * bulletForce, ForceMode.Impulse); //add a force in the up vector

        //deplete ammo
        ammoCount--;
        GameManager.instance.ammoCount.Subtract();
    }
}
