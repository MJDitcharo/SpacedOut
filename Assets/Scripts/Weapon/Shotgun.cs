using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : WeaponBase
{
    [SerializeField] float shotgunSpread = 30f;
    [SerializeField] float shotgunAngleChange = 5f;

    private void Awake()
    {
        weaponID = WeaponID.Shotgun;
    }

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
        for (float angle = -shotgunSpread; angle <= shotgunSpread; angle += shotgunAngleChange)
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, firePoint[firePointIndex].position, firePoint[firePointIndex].rotation); //spawn the bullet and reference the bullet to modify 
            Rigidbody rigidbody = bulletInstance.GetComponent<Rigidbody>(); //acess the rigidbody of the game object
            bullet bulletScript = bulletInstance.GetComponent<bullet>();
            bulletScript.damage = (int)(damage * damageMultiplier);
            firePoint[firePointIndex].localEulerAngles = new Vector3(0, angle, 0);
            rigidbody.AddForce(firePoint[firePointIndex].forward * bulletForce, ForceMode.Impulse); //add a force in the up vector
            GameManager.instance.bullets.Add(bulletInstance);
        }

        //deplete ammo
        ammoCount--;
        GameManager.instance.ammoCount.Subtract();
        AudioManager.Instance.PlaySFX("baseGun");
    }
}
