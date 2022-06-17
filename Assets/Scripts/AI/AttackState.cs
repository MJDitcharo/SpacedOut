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

    public Transform[] firePoints;
    public int firepointIndex = 0;
    public GameObject bulletPrefab; //instance of bullet prefab

    public float firerate = 1;
    float nextShot;
    public float bulletForce = 20f;
    public float Damage; //damage
    [SerializeField] LayerMask lm;

    public override State RunCurrentState()
    {
        RaycastHit hit = new RaycastHit();
        //Physics.Raycast(transform.position, GameManager.instance.player.transform.position + new Vector3(0, 1, 0) - transform.position, out hit, Mathf.Infinity, lm);
        if (Vector3.Distance(transform.position, GameManager.instance.player.transform.position) >= attackDistance + 2 || Physics.Raycast(transform.position, GameManager.instance.player.transform.position + new Vector3(0, 1, 0) - transform.position, out hit, Mathf.Infinity, lm) && hit.collider.gameObject != GameManager.instance.player)
        {
            Debug.Log("switching");
            return engageState;
        }
        Debug.DrawRay(transform.position, GameManager.instance.player.transform.position + new Vector3(0, 1, 0) - transform.position, Color.blue, .05f);

        if (lookCoroutine == null)
            lookCoroutine = StartCoroutine(TurnToPlayer());
        if(movement.GetAgent().velocity.magnitude == 0)
        {
            animator.SetBool("Running", false);
           
        }

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
                GameObject bullet = Instantiate(bulletPrefab, firePoints[firepointIndex].position, firePoints[firepointIndex].rotation); //spawn the bullet and reference the bullet to modify 
                Rigidbody rb = bullet.GetComponent<Rigidbody>(); //acess the rigidbody of the game object
                rb.AddForce(firePoints[firepointIndex].forward * bulletForce, ForceMode.Impulse); //add a force in the up vector
                GameManager.instance.bullets.Add(bullet);
                firepointIndex++;
                if (firepointIndex >= firePoints.Length)
                    firepointIndex = 0;
                break;
            case AttackType.shotgun:
                for (float angle = -30; angle <= 30; angle += 5)
                {
                    GameObject bulletInstance = Instantiate(bulletPrefab, firePoints[firepointIndex].position, firePoints[firepointIndex].rotation); //spawn the bullet and reference the bullet to modify 
                    Rigidbody rigidbody = bulletInstance.GetComponent<Rigidbody>(); //acess the rigidbody of the game object
                    firePoints[firepointIndex].localEulerAngles = new Vector3(0, angle, 0);
                    rigidbody.AddForce(firePoints[firepointIndex].forward * bulletForce, ForceMode.Impulse); //add a force in the up vector
                    GameManager.instance.bullets.Add(bulletInstance);
                }
                if (firepointIndex >= firePoints.Length)
                    firepointIndex = 0;
                break;
        }
        
    }

    IEnumerator TurnToPlayer()
    {
        //Debug.Log("running coroutine");
        Quaternion lookRoation = Quaternion.LookRotation(GameManager.instance.player.transform.position - new Vector3(transform.position.x, GameManager.instance.player.transform.position.y, transform.position.z));
        Quaternion originalRotation = transform.rotation;

        float time = 0;

        while(time < 1)
        {
            //Debug.Log(time);
            transform.rotation = Quaternion.Slerp(originalRotation, lookRoation, time);

            time += Time.deltaTime * rotationSpeed;

            yield return new WaitForEndOfFrame();
        }
        lookCoroutine = null;
    }
}
