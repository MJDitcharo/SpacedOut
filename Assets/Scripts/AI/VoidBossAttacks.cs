using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VoidBossAttacks : State
{
    public float bossIntensityMultiplier = 1;
    [SerializeField] float attackDistance = 15;
    [SerializeField] State engageState;
    [SerializeField] float rotationSpeed = 45;
    [SerializeField] TeleportingState teleportingState;
    enum Attacks { dash, wave, clone, pull }
    [SerializeField] Attacks currentAttack = Attacks.dash;
    Coroutine attackCoroutine;
    Coroutine turnCoroutine;
    bool attacking = true;
    bool damageDealt;

    [SerializeField] int damage = 20;
    [SerializeField] float pushbackMultiplier = 4;
    [SerializeField] float dashAttackTime = 2;
    [SerializeField] float dashSpeed = 15;
    [SerializeField] float dashWaitTime = 1;
    [SerializeField] float dashDistance = 15;

    [SerializeField] int waveNumber = 2;
    [SerializeField] float waveInterval = 1;
    [SerializeField] Transform[] firePoints;
    int firepointIndex = 0;
    [SerializeField] GameObject wavePrefab;
    [SerializeField] float waveForce = 20;

    [SerializeField] GameObject shadowClone;
    [SerializeField] int cloneCount = 3;
    [SerializeField] float cloneAttackChaseSpeed = 8;
    [SerializeField] float pullStrength = 10;

    public override State RunCurrentState()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, GameManager.instance.player.transform.position + new Vector3(0, 1, 0) - transform.position, out hit, Mathf.Infinity);
        if (!attacking)
        {
            attacking = true;

            int tp = Random.Range(0, 2);
            if (tp == 0)
                return engageState;
            else
                return teleportingState;
        }

        Collider[] colArr = Physics.OverlapSphere(transform.position, 2);
        for (int i = 0; i < colArr.Length; i++)
        {
            if (colArr[i].tag == "Player")
            {
                playerHealth playerHP = colArr[i].GetComponent<playerHealth>();
                if (playerHP != null && playerHP.isDamageable && !damageDealt)
                {
                    GameManager.instance.movement.pushback += (-pushbackMultiplier * (transform.right - GameManager.instance.player.transform.position).normalized);
                    playerHP.DoDamage(damage);
                    damageDealt = true;
                    //Debug.Log("Damage Done");
                }
            }
        }

        Attack();

        return this;
    }

    void Attack()
    {
        switch(currentAttack)
        {
            case Attacks.dash:
                if (attackCoroutine == null)
                    attackCoroutine = StartCoroutine(DashAttack());
                if(turnCoroutine == null)
                    turnCoroutine = StartCoroutine(TurnToPlayer());
                break;
            case Attacks.wave:
                if (attackCoroutine == null)
                    attackCoroutine = StartCoroutine(WaveAttack());
                if (turnCoroutine == null)
                    turnCoroutine = StartCoroutine(TurnToPlayer());
                break;
            case Attacks.clone:
                if (attackCoroutine == null)
                    attackCoroutine = StartCoroutine(CloneAttack());
                break;
            case Attacks.pull:
                PullAttack();
                break;
        }
    }

    IEnumerator DashAttack()
    {
        animator.SetBool("Running", false);
        animator.SetBool("Crouching", true);
        movement.GetAgent().isStopped = true;
        damageDealt = false;


        yield return new WaitForSeconds(dashAttackTime);
        animator.SetBool("Dashing", true);
        animator.SetBool("Crouching", false);

        movement.pushback += (-dashDistance * (transform.position - GameManager.instance.player.transform.position).normalized);
        
        yield return new WaitForSeconds(.5f);

        animator.SetBool("Dashing", false);

        movement.GetAgent().isStopped = false;

        currentAttack = (Attacks)Random.Range(0, 1 + bossIntensityMultiplier);
        attackCoroutine = null;
        attacking = false;
    }
    IEnumerator WaveAttack()
    {
        animator.SetBool("Running", false);
        animator.SetBool("Casting", true);

        movement.GetAgent().isStopped = true;
        for(int i = 0; i < waveNumber; i++)
        {
            yield return new WaitForSeconds(waveInterval);

            for (float angle = -45; angle <= 45; angle += 5)
            {
                GameObject bulletInstance = Instantiate(wavePrefab, firePoints[firepointIndex].position, firePoints[firepointIndex].rotation); //spawn the bullet and reference the bullet to modify 
                Rigidbody rigidbody = bulletInstance.GetComponent<Rigidbody>(); //acess the rigidbody of the game object
                firePoints[firepointIndex].localEulerAngles = new Vector3(0, angle, 0);
                rigidbody.AddForce(firePoints[firepointIndex].forward * waveForce, ForceMode.Impulse); //add a force in the up vector
                GameManager.instance.bullets.Add(bulletInstance);
            }
            if (firepointIndex >= firePoints.Length)
                firepointIndex = 0;
        }

        animator.SetBool("Casting", false);

        currentAttack = (Attacks)Random.Range(0, 1 + bossIntensityMultiplier);
        attackCoroutine = null;
        attacking = false;
        movement.GetAgent().isStopped = false;
    }

    IEnumerator CloneAttack()
    {
        animator.SetBool("Running", true);
        damageDealt = false;
        bool done = false;
        while (!done) 
        {
            int spot = Random.Range(0, teleportingState.tpLocations.Length);
            if(Vector3.Distance(teleportingState.tpLocations[spot].transform.position, GameManager.instance.player.transform.position) >= 15)
            {
                movement.GetAgent().Warp(teleportingState.tpLocations[spot].transform.position);
                done = true;
            }
            
        }

        List<NavMeshAgent> clones = new List<NavMeshAgent>();

        for(int i = 0; i < cloneCount; i++)
        {
            done = false;
            Vector3 spawnPosition = Vector3.zero;
            while(!done)
            {
                int spot = Random.Range(0, teleportingState.tpLocations.Length);
                if(Vector3.Distance(teleportingState.tpLocations[spot].transform.position, GameManager.instance.player.transform.position) >= 15)
                {
                    spawnPosition = teleportingState.tpLocations[spot].transform.position;
                    done = true;
                }
            }
            clones.Add(Instantiate(shadowClone, spawnPosition, Quaternion.identity).GetComponent<NavMeshAgent>());
        }

        int startingHP = GetComponent<health>().currHealth;
        bool gotHit = false;
        while(!gotHit)
        {
            movement.MoveToLocation(GameManager.instance.player.transform.position);
            Collider[] colArr = Physics.OverlapSphere(transform.position, 2);
            for (int i = 0; i < colArr.Length; i++)
            {
                if (colArr[i].tag == "Player")
                {
                    Debug.Log("touching player");
                    playerHealth playerHP = colArr[i].GetComponent<playerHealth>();
                    if (playerHP != null && playerHP.isDamageable && !damageDealt)
                    {
                        GameManager.instance.movement.pushback += (-pushbackMultiplier * (transform.right - GameManager.instance.player.transform.position).normalized);
                        playerHP.DoDamage(damage);
                        damageDealt = true;
                        gotHit = true;
                        //Debug.Log("Damage Done");
                    }
                }
            }
            
            for(int i = 0; i < clones.Count; i++)
            {
                clones[i].SetDestination(GameManager.instance.player.transform.position);
                Collider[] colliders = Physics.OverlapSphere(clones[i].transform.position, 2);
                for (int j = 0; j < colliders.Length; j++)
                {
                    if (colliders[j].tag == "Player")
                    {
                        Debug.Log("touching player");
                        playerHealth playerHP = colliders[j].GetComponent<playerHealth>();
                        if (playerHP != null && playerHP.isDamageable && !damageDealt)
                        {
                            GameManager.instance.movement.pushback += (-pushbackMultiplier * (transform.right - GameManager.instance.player.transform.position).normalized);
                            playerHP.DoDamage(damage);
                            damageDealt = true;
                            gotHit = true;
                            //Debug.Log("Damage Done");
                        }
                    }
                }
            }

            if(GetComponent<health>().currHealth < startingHP)
            {
                animator.SetBool("Stunned", true);
                animator.SetBool("Running", false);
                for (int i = 0; i < clones.Count; i++)
                {
                    Destroy(clones[i].gameObject);
                }

                yield return new WaitForSeconds(.5f);
                gotHit = true;

            }

            yield return new WaitForEndOfFrame();
        }

        for (int i = 0; i < clones.Count; i++)
        {
            if (clones[i] == null)
                break;
            Destroy(clones[i].gameObject);
        }

        while (!done)
        {
            int spot = Random.Range(0, teleportingState.tpLocations.Length);
            if (Vector3.Distance(teleportingState.tpLocations[spot].transform.position, GameManager.instance.player.transform.position) >= 15)
            {
                movement.GetAgent().Warp(teleportingState.tpLocations[spot].transform.position);
                done = true;
            }
        }
        animator.SetBool("Stunned", false);

        currentAttack = (Attacks)Random.Range(0, 1 + bossIntensityMultiplier);
        attackCoroutine = null;
        attacking = false;
    }

    void PullAttack()
    {
        animator.SetBool("Running", false);
        animator.SetBool("Pulling", true);
        damageDealt = false;

        Vector3 direction = (pullStrength * (transform.position - GameManager.instance.player.transform.position).normalized);
        GameManager.instance.movement.pushback += direction;

        currentAttack = (Attacks)Random.Range(0, 2);
        animator.SetBool("Pulling", false);

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
        turnCoroutine = null;
    }
}
