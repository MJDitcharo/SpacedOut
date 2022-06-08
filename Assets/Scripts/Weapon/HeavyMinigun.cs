using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyMinigun : Heavy
{
    [SerializeField] int fireSpread = 2;

    public override void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextShotFired && ammoCount != 0 && Time.timeScale > 0) //if the first mouse button is down
        {
            nextShotFired = Time.time + 1f / fireRate / fireRateMultiplier; //delay for the next bullet fired
            Shoot(); //shoot method
        }
    }
    public override void Shoot()
    {
        firePoint[firePointIndex].transform.localRotation = Quaternion.Euler(0, firePoint[firePointIndex].transform.localRotation.y + Random.Range(-fireSpread, fireSpread), 0);
        GameObject bullet = Instantiate(bulletPrefab, firePoint[firePointIndex].position, firePoint[firePointIndex].rotation); //spawn the bullet and reference the bullet to modify 
        Rigidbody rb = bullet.GetComponent<Rigidbody>(); //acess the rigidbody of the game object
        rb.AddForce(firePoint[firePointIndex].forward * bulletForce, ForceMode.Impulse); //add a force in the up vector
        GameManager.instance.bullets.Add(bullet);
        firePointIndex++;
        if (firePointIndex >= firePoint.Length)
            firePointIndex = 0;

        //deplete ammo
        ammoCount = GameManager.instance.ammoCount.GetQuantity();
        ammoCount--;
        GameManager.instance.ammoCount.Subtract();
        AudioManager.Instance.PlaySFX("baseGun");
    }
}
