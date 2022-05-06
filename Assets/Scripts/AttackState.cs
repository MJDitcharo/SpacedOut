using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] float attackDistance;

    public override State RunCurrentState()
    {
        Debug.Log("attacking");
        return this;
    }
}
