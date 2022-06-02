using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolDualWield : Pistol
{

    [SerializeField] Transform[] firePoints;
    int firepointIndex = 0;

    public override void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoints[firepointIndex].position, firePoints[firepointIndex].rotation); //spawn the bullet and reference the bullet to modify 
        Rigidbody rb = bullet.GetComponent<Rigidbody>(); //acess the rigidbody of the game object
        rb.AddForce(firePoints[firepointIndex].forward * bulletForce, ForceMode.Impulse); //add a force in the up vector
        GameManager.instance.bullets.Add(bullet);
        firepointIndex++;
        if (firepointIndex >= firePoints.Length)
            firepointIndex = 0;
    }
}
