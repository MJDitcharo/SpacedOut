using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBurst : Rifle
{
    [SerializeField] float burstFireRate = 1;
    [SerializeField] int burstSize = 5;

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
        StartCoroutine(FireBurst());
    }

    public IEnumerator FireBurst()
    {
       
        for (int i = 0; i < burstSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint[firePointIndex].position, firePoint[firePointIndex].rotation); //spawn the bullet and reference the bullet to modify 
            Rigidbody rb = bullet.GetComponent<Rigidbody>(); //acess the rigidbody of the game object
            bullet bulletScript = bullet.GetComponent<bullet>();
            bulletScript.damage = (int)(damage * damageMultiplier);
            rb.AddForce(firePoint[firePointIndex].forward * bulletForce, ForceMode.Impulse); //add a force in the up vector

            //deplete ammo
            ammoCount = GameManager.instance.ammoCount.GetQuantity();
            ammoCount--;
            GameManager.instance.ammoCount.Subtract();
           AudioManager.Instance.PlaySFX(gunshotSound);

            yield return new WaitForSeconds(burstFireRate); // wait till the next round
        }
    }

}
