using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidBossAttacks : State
{
    public float bossIntensityMultiplier = 1;
    [SerializeField] float attackDistance = 15;
    [SerializeField] State engageState;
    [SerializeField] float rotationSpeed = 45;
    [SerializeField] State teleportingState;
    enum Attacks { dash, wave, clone }
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
        }
    }

    IEnumerator DashAttack()
    {
        movement.GetAgent().isStopped = true;
        damageDealt = false;


        yield return new WaitForSeconds(dashAttackTime);

        movement.pushback += (-dashDistance * (transform.position - GameManager.instance.player.transform.position).normalized);
        
        yield return new WaitForSeconds(.5f);

        movement.GetAgent().isStopped = false;

        currentAttack = (Attacks)Random.Range(0, 1 + bossIntensityMultiplier);
        attackCoroutine = null;
        attacking = false;
    }
    IEnumerator WaveAttack()
    {
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
        
        currentAttack = (Attacks)Random.Range(0, 1 + bossIntensityMultiplier);
        attackCoroutine = null;
        attacking = false;
        movement.GetAgent().isStopped = false;
    }

    IEnumerator CloneAttack()
    {
        yield return new WaitForSeconds(.5f);
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
