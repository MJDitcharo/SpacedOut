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
                MollyAttack();
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


        }
        
        attacking = false;
    }
    void BlastAttack()
    {

    }
}
