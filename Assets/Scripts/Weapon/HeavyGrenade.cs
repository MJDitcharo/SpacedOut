using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyGrenade : Heavy
{
    public override void Shoot()
    {
        GameObject item = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody body = item.GetComponent<Rigidbody>(); //acess the rigidbody of the game object
        body.AddForce(transform.forward * bulletForce, ForceMode.Impulse);

        //deplete ammo
        ammoCount--;
        UpdateVisual();
        AudioManager.Instance.PlaySFX(gunshotSound);
    }
}
