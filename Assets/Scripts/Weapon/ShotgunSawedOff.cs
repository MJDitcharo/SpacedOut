using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunSawedOff : Shotgun
{

    [SerializeField] float shotgunSpread = 45f;
    [SerializeField] float shotgunAngleChange = 5f;

    public override void Shoot()
    {
        for (float angle = -shotgunSpread; angle <= shotgunSpread; angle += shotgunAngleChange)
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); //spawn the bullet and reference the bullet to modify 
            Rigidbody rigidbody = bulletInstance.GetComponent<Rigidbody>(); //access the rigidbody of the game object
            bullet bulletScript = bulletInstance.GetComponent<bullet>();
            bulletScript.damage = (int)(damage * damageMultiplier);
            firePoint.localEulerAngles = new Vector3(0, angle, 0);
            rigidbody.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse); //add a force in the up vector
            GameManager.instance.bullets.Add(bulletInstance);
        }

        //deplete ammo
        ammoCount--;
        GameManager.instance.ammoCount.Subtract();
    }
}
