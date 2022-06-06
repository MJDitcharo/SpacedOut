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

    enum Attacks { mollies, blast, spray }

    [SerializeField] float mollyTossDelay = 1;
    [SerializeField] int numberOfMollies = 5;
    [SerializeField] float turnSpeed = 45;
    [SerializeField] float blastSpeed = 20;

    [SerializeField] float sprayWaitTime = .5f;
    [SerializeField] int sprayMollyCount = 40;
    [SerializeField] float delayBetweenSpray = .05f;
    [SerializeField] Vector2 sprayForceRange;
    [SerializeField] Transform sprayFirePoint;

    public float bossIntensityMultiplier = 1f;

    Coroutine attackCoroutine;
    Coroutine lookCoroutine;

    Attacks currentAttack = Attacks.spray;
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
            case Attacks.spray:
                if (attackCoroutine == null)
                    attackCoroutine = StartCoroutine(SprayAttack());
                break;
        }
    }

    IEnumerator MollyAttack()
    {
        //Transform player = GameManager.instance.player.transform;

        Quaternion lookRoation = Quaternion.LookRotation(GameManager.instance.player.transform.position - new Vector3(transform.position.x, GameManager.instance.player.transform.position.y, transform.position.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, 1);

        for (int mollyCount = 0; mollyCount < numberOfMollies * bossIntensityMultiplier; mollyCount++)
        {
            yield return new WaitForSeconds(mollyTossDelay / bossIntensityMultiplier);

            lookRoation = Quaternion.LookRotation(GameManager.instance.player.transform.position - new Vector3(transform.position.x, GameManager.instance.player.transform.position.y, transform.position.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, 1);

            GameObject bullet = Instantiate(fireBall, firePointMolly.position, Quaternion.identity);
            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();

            firePointMolly.localRotation = Quaternion.Euler(-45, Random.Range(mollyAngleRange.x, mollyAngleRange.y), 0);
            bulletRB.AddForce(firePointMolly.forward * Random.Range(mollyForceRange.x, mollyForceRange.y) * Vector3.Distance(transform.position, GameManager.instance.player.transform.position));
        }

        attackCoroutine = null;
        attacking = false;
        if(bossIntensityMultiplier == 1)
            currentAttack = (Attacks)((int)Mathf.Round(Random.Range(0, 2)));
        else
            currentAttack = (Attacks)((int)Mathf.Round(Random.Range(0, 3)));
    }
    IEnumerator BlastAttack()
    {
        yield return new WaitForSeconds(1 / bossIntensityMultiplier);

        GameObject blast = Instantiate(blastAttackBullet, transform.position, Quaternion.identity);
        blast.transform.rotation = transform.rotation;
        Rigidbody blastRB = blast.GetComponent<Rigidbody>();
        blastRB.velocity = transform.forward * blastSpeed;

        yield return new WaitForSeconds(1 / bossIntensityMultiplier);

        attackCoroutine = null;
        attacking = false;
        if (bossIntensityMultiplier == 1)
            currentAttack = (Attacks)((int)Mathf.Round(Random.Range(0, 2)));
        else
            currentAttack = (Attacks)((int)Mathf.Round(Random.Range(0, 3)));
    }
    IEnumerator SprayAttack()
    {
        yield return new WaitForSeconds(sprayWaitTime / bossIntensityMultiplier);

        for(int i = 0; i < sprayMollyCount * bossIntensityMultiplier; i++)
        {
            GameObject bullet = Instantiate(fireBall, sprayFirePoint.position, Quaternion.identity);
            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();

            sprayFirePoint.localRotation = Quaternion.Euler(-45, Random.Range(-179, 180), 0);
            bulletRB.AddForce(sprayFirePoint.forward * Random.Range(sprayForceRange.x, sprayForceRange.y));
                
            yield return new WaitForSeconds(delayBetweenSpray / bossIntensityMultiplier);

        }
        attackCoroutine = null;
        attacking = false;
        if (bossIntensityMultiplier == 1)
            currentAttack = (Attacks)((int)Mathf.Round(Random.Range(0, 2)));
        else
            currentAttack = (Attacks)((int)Mathf.Round(Random.Range(0, 3)));
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