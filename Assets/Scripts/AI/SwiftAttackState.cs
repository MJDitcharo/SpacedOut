using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SwiftAttackState : State
{
    enum AttackType { normal, shotgun }
    [SerializeField] AttackType attackType = AttackType.normal;
    [SerializeField] float attackDistance;
    [SerializeField] EngageState engageState;

    [SerializeField] float rotationSpeed = 10f;
    Coroutine lookCoroutine;
    Coroutine turnRight;

    public Transform[] firePoints;
    public int firepointIndex = 0;
    public GameObject bulletPrefab; //instance of bullet prefab
    public float bulletForce = 20f;
    public float Damage; //damage

    [SerializeField] EnemyMovement enemyMovement;
    [SerializeField] float dashSpeed = 5f;

    [SerializeField] float dashTime = 1;
    float dashTimer = 0;
    bool dashRight = true;

    [SerializeField] float burstSpacing = .2f;
    float burstTimer = 0;

    [SerializeField] int burstBulletCount = 3;
    int bulletsFired = 0;

    public override State RunCurrentState()
    {
        enemyMovement.GetAgent().isStopped = true;

        if(dashTimer >= dashTime)
        {
            
            burstTimer = 0;
            bulletsFired = 0;
            dashTimer = 0;
            dashRight = !dashRight;
            enemyMovement.GetAgent().isStopped = false;
            
            RaycastHit hit = new RaycastHit();
            if (Vector3.Distance(transform.position, GameManager.instance.player.transform.position) <= attackDistance && Physics.Raycast(transform.position, GameManager.instance.player.transform.position + new Vector3(0, 1, 0) - transform.position, out hit, Mathf.Infinity) && hit.collider.gameObject == GameManager.instance.player)
            {
                animator.SetBool("Dashing", false);
                animator.SetBool("Running", false);
                return this;
            }
            else
            {
                animator.SetBool("Dashing", false);
                animator.SetBool("Running", true);
                return engageState;
            }
        }

        //dash if true
        if(bulletsFired >= burstBulletCount)
        {
            //if(turnRight == null)
                //turnRight = StartCoroutine(TurnForRoll());
            animator.SetBool("Dashing", true);
            animator.SetBool("Running", false);
            
            if (dashRight)
            {
                enemyMovement.GetAgent().Move(transform.right * dashSpeed * Time.deltaTime);
            }
            else
            {
                enemyMovement.GetAgent().Move(transform.right * -dashSpeed * Time.deltaTime);
            }

            //enemyMovement.GetAgent().Move(transform.forward * dashSpeed * Time.deltaTime);

            dashTimer += Time.deltaTime;
            return this;
        }

        if (lookCoroutine == null)
            lookCoroutine = StartCoroutine(TurnToPlayer());

        if (burstTimer >= burstSpacing)
        {
            animator.SetBool("Dashing", false);
            animator.SetBool("Running", false);
            burstTimer = 0;
            bulletsFired++;
            Shoot();
        }

        burstTimer += Time.deltaTime;
        return this;
    }

    public void Shoot()
    {
        switch (attackType)
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

        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, time);

            time += Time.deltaTime * rotationSpeed;

            yield return null;
        }
        lookCoroutine = null;
    }

    IEnumerator TurnForRoll()
    {
        bool rotatedRight = dashRight;
        if (rotatedRight)
            transform.Rotate(0,-90,0);
        else
            transform.Rotate(0, 90, 0);

        yield return new WaitForSeconds(dashTime - .1f);

        if (rotatedRight)
            transform.Rotate(0, 90, 0);
        else
            transform.Rotate(0, -90, 0);

        turnRight = null;
    }
}
