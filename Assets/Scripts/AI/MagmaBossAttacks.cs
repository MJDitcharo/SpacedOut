using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaBossAttacks : State
{
    [SerializeField] WanderState wanderState;

    [SerializeField] GameObject mollyPrefab;

    enum Attacks { mollies, blast }

    [SerializeField] float mollyTossDelay = 1;
    [SerializeField] int numberOfMollies = 5;

    Coroutine coroutine;

    Attacks currentAttack = Attacks.mollies;
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
                if(coroutine == null)
                    coroutine = StartCoroutine(MollyAttack());
                break;
            case Attacks.blast:
                BlastAttack();
                break;
        }
    }

    IEnumerator MollyAttack()
    {
        for(int mollyCount = 0; mollyCount < mollyTossDelay; mollyCount++)
        {
            yield return new WaitForSeconds(mollyTossDelay);

            Debug.Log("Throwing Molly");
        }

        coroutine = null;
        attacking = false;
    }
    void BlastAttack()
    {

    }
}
