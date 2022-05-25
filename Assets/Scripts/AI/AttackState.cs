using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    enum AttackType { normal, shotgun }
    [SerializeField] AttackType attackType = AttackType.normal;
    [SerializeField] float attackDistance;
    [SerializeField] EngageState engageState;

    [SerializeField] float rotationSpeed = 10f;
    Coroutine lookCoroutine;

    public Transform firePoint;
    public GameObject bulletPrefab; //instance of bullet prefab

    [SerializeField] float firerate = 1;
    float nextShot;
    public float bulletForce = 20f;
    public float Damage; //damage

    public override State RunCurrentState()
    {
        if(Vector3.Distance(transform.position, GameManager.instance.player.transform.position) >= attackDistance + 2)
        {
            return engageState;
        }

        if (lookCoroutine == null)
            lookCoroutine = StartCoroutine(TurnToPlayer());
        
        Attack();

        return this;
    }

    void Attack()
    {
        if(Time.time > nextShot)
        {
            nextShot = Time.time + firerate;
            Shoot();
        }
    }
    public void Shoot()
    {
        switch(attackType)
        {
            case AttackType.normal:
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); //spawn the bullet and reference the bullet to modify 
                Rigidbody rb = bullet.GetComponent<Rigidbody>(); //acess the rigidbody of the game object
                rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse); //add a force in the up vector
                GameManager.instance.bullets.Add(bullet);
                break;
            case AttackType.shotgun:
                for (float angle = -30; angle <= 30; angle += 5)
                {
                    GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); //spawn the bullet and reference the bullet to modify 
                    Rigidbody rigidbody = bulletInstance.GetComponent<Rigidbody>(); //acess the rigidbody of the game object
                    firePoint.localEulerAngles = new Vector3(0, angle, 0);
                    rigidbody.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse); //add a force in the up vector
                    GameManager.instance.bullets.Add(bulletInstance);
                }
                break;
        }
        
    }

    IEnumerator TurnToPlayer()
    {
        Debug.Log("running coroutine");
        Quaternion lookRoation = Quaternion.LookRotation(GameManager.instance.player.transform.position - new Vector3(transform.position.x, GameManager.instance.player.transform.position.y, transform.position.z));

        float time = 0;

        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, time);

            time += Time.deltaTime * rotationSpeed;

            yield return null;
        }
        lookCoroutine = null;

        
    }

}
