using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] float attackDistance;
    [SerializeField] EngageState engageState;

    public Transform firePoint;
    public GameObject bulletPrefab; //instance of bullet prefab

    public float fireRate = 7f; //firerate
    public float bulletForce = 20f;
    public float Damage; //damage

    private float nextShotFired = 0f; //counter for next bullet that is fired

    public override State RunCurrentState()
    {
        if(Vector3.Distance(transform.position, GameManager.instance.player.transform.position) >= attackDistance)
        {
            return engageState;
        }

        Attack();

        return this;
    }

    void Attack()
    {
        Shoot();
    }
    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); //spawn the bullet and reference the bullet to modify 
        Rigidbody rb = bullet.GetComponent<Rigidbody>(); //acess the rigidbody of the game object
        rb.AddForce(firePoint.up * bulletForce, ForceMode.Impulse); //add a force in the up vector

    }
}
