using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab; //instance of bullet prefab

    public float fireRate = 7f; //firerate
    public float bulletForce = 20f;
    public float ammoCount; //ammo count 
    public float Damage; //damage

    private float nextShotFired = 0f; //counter for next bullet that is fired
    // Update is called once per frame
    public virtual void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextShotFired) //if the first mouse button is down
        {
            nextShotFired = Time.time + 1f / fireRate; //delay for the next bullet fired
            Shoot(); //shoot method
        }
    }

    public virtual void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); //spawn the bullet and reference the bullet to modify 
        Rigidbody rb = bullet.GetComponent<Rigidbody>(); //acess the rigidbody of the game object
        rb.AddForce(firePoint.up * bulletForce, ForceMode.Impulse); //add a force in the up vector

    }

    public virtual void SetActive(bool isActive) //set current weapon to sctive
    {
        gameObject.SetActive(isActive);
    }
}
