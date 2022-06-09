using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : State
{

    [SerializeField] int damage = 10;
    [SerializeField] float attackDistance = 4;
    [SerializeField] EngageState engageState;


    [SerializeField] EnemyMovement enemyMovement;
    [SerializeField] float dashSpeed = 5f;
    [SerializeField] float dashTime = 1;
    [SerializeField] float dashDistance = 5;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float pushbackMultiplier = 1;

    Coroutine lookCoroutine;
    Coroutine attackCoroutine;

    bool damageDealt = false;


    public override State RunCurrentState()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, GameManager.instance.player.transform.position + new Vector3(0, 1, 0) - transform.position, out hit, Mathf.Infinity);
        if (Vector3.Distance(transform.position, GameManager.instance.player.transform.position) >= attackDistance + 2 || hit.collider == null || hit.collider.gameObject != GameManager.instance.player)
        {
            return engageState;
        }

        if (lookCoroutine == null)
            lookCoroutine = StartCoroutine(TurnToPlayer());


        if (attackCoroutine == null)
        {
            animator.SetBool("Running", false);
            Debug.Log("Attacking now");
            attackCoroutine = StartCoroutine(DashAttack()); 
        }

        Collider[] colArr = Physics.OverlapSphere(transform.position, 1);
        for (int i = 0; i < colArr.Length; i++)
        {
            if (colArr[i].tag == "Player")
            {
                playerHealth playerHP = colArr[i].GetComponent<playerHealth>();
                if (playerHP != null && playerHP.isDamageable && !damageDealt)
                {
                    GameManager.instance.movement.pushback += (-pushbackMultiplier * (-transform.forward - GameManager.instance.player.transform.position).normalized);
                    playerHP.DoDamage(damage);
                    damageDealt = true;
                    Debug.Log("Damage Done");
                }
            }
        }

        return this;
    }
   

    

    IEnumerator DashAttack()
    {
        enemyMovement.GetAgent().isStopped = true;
        damageDealt = false;

        yield return new WaitForSeconds(dashTime);

        movement.pushback += (-dashDistance * (transform.position - GameManager.instance.player.transform.position).normalized);
       
        attackCoroutine = null;
        enemyMovement.GetAgent().isStopped = false;
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
}
