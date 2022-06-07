using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBurstVoid : RifleBurst
{
    public float seconds;
    int fireSpread = 6;
    // Start is called before the first frame update
    public override void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            seconds += Time.deltaTime;
            fireRate = 0.001f;

        }
        if (Input.GetButtonUp("Fire1"))
        {
           nextShotFired = Time.time + 1f / fireRate / fireRateMultiplier;
           Shoot();
        }
    }
    public override void Shoot()
    {
        //spawn the bullet and reference the bullet to modify 
        for (float i = 0; i < seconds; )
        {
            if(GameManager.instance.ammoCount.GetQuantity() == 0)
                return;

            GameObject _bullet = Instantiate(bulletPrefab, firePoint[firePointIndex].position, firePoint[firePointIndex].rotation);
            Rigidbody rb = _bullet.GetComponent<Rigidbody>();
            bullet bulletScript = _bullet.GetComponent<bullet>();
            bulletScript.damage = (int)(damage * damageMultiplier);
            firePoint[firePointIndex].transform.localRotation = Quaternion.Euler(0, firePoint[firePointIndex].transform.localRotation.y + Random.Range(-fireSpread, fireSpread), 0);
            rb.AddForce(firePoint[firePointIndex].forward * bulletForce, ForceMode.Impulse); //add a force in the up vector
            i += .1f; 

            //deplete ammo
            ammoCount = GameManager.instance.ammoCount.GetQuantity();
            ammoCount--;
            GameManager.instance.ammoCount.Subtract();
        }
        seconds = 0;
    }

    
        
}
