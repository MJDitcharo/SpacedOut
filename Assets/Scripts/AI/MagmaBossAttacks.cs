using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaBossAttacks : State
{
    [SerializeField] WanderState wanderState;
    [SerializeField] Transform firePointMolly;
    [SerializeField] Vector2 mollyForceRange;
    [SerializeField] Vector2 mollyAngleRange;

    [SerializeField] GameObject mollyPrefab;
    [SerializeField] GameObject fireBall;
    [SerializeField] GameObject blastAttackBullet;

    enum Attacks { mollies, blast }

    [SerializeField] float mollyTossDelay = 1;
    [SerializeField] int numberOfMollies = 5;
    [SerializeField] float turnSpeed = 45;
    [SerializeField] float blastSpeed = 20;

    Coroutine attackCoroutine;
    Coroutine lookCoroutine;

    Attacks currentAttack = Attacks.blast;
    bool attacking = false;

    public override State RunCurrentState()
    {
        if(!attacking)
        {
            attacking = true;
            return wanderState;
        }

        Attack();

        return this;
    }

    void Attack()
    {
        switch(currentAttack)
        {
            case Attacks.mollies:
                Debug.Log("Molly attack");
                if(attackCoroutine == null)
                    attackCoroutine = StartCoroutine(MollyAttack());
                break;
            case Attacks.blast:
                if(lookCoroutine == null)
                    lookCoroutine = StartCoroutine(TurnToPlayer());
                if (attackCoroutine == null)
                    attackCoroutine = StartCoroutine(BlastAttack());
                break;
        }
    }

    IEnumerator MollyAttack()
    {
        //Transform player = GameManager.instance.player.transform;

        Quaternion lookRoation = Quaternion.LookRotation(GameManager.instance.player.transform.position - new Vector3(transform.position.x, GameManager.instance.player.transform.position.y, transform.position.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, 1);

        for (int mollyCount = 0; mollyCount < numberOfMollies; mollyCount++)
        {
            yield return new WaitForSeconds(mollyTossDelay);

            lookRoation = Quaternion.LookRotation(GameManager.instance.player.transform.position - new Vector3(transform.position.x, GameManager.instance.player.transform.position.y, transform.position.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, 1);

            GameObject bullet = Instantiate(fireBall, firePointMolly.position, Quaternion.identity);
            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();

            firePointMolly.localRotation = Quaternion.Euler(-45, Random.Range(mollyAngleRange.x, mollyAngleRange.y), 0);
            bulletRB.AddForce(firePointMolly.forward * Random.Range(mollyForceRange.x, mollyForceRange.y) * Vector3.Distance(transform.position, GameManager.instance.player.transform.position));
        }

        attackCoroutine = null;
        attacking = false;
    }
    IEnumerator BlastAttack()
    {
        yield return new WaitForSeconds(1);

        GameObject blast = Instantiate(blastAttackBullet, transform.position, Quaternion.identity);
        blast.transform.rotation = transform.rotation;
        Rigidbody blastRB = blast.GetComponent<Rigidbody>();
        blastRB.velocity = transform.forward * blastSpeed;

        yield return new WaitForSeconds(1);

        attackCoroutine = null;
        attacking = false;
    }

    IEnumerator TurnToPlayer()
    {
        //Debug.Log("running coroutine");
        Quaternion lookRoation = Quaternion.LookRotation(GameManager.instance.player.transform.position - new Vector3(transform.position.x, GameManager.instance.player.transform.position.y, transform.position.z));

        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, time);

            time += Time.deltaTime * turnSpeed;

            yield return null;
        }
        lookCoroutine = null;
    }
}
