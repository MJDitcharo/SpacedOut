using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngageState : State
{
    [SerializeField] AttackState attackState;

    [SerializeField] float attackDistance;

    // Start is called before the first frame update
    void Start()
    {

    }

    public override State RunCurrentState()
    {
        if (Vector2.Distance(transform.position, GameManager.instance.player.transform.position) <= attackDistance)
        {
            return attackState;
        }

        return this;
    }
}
