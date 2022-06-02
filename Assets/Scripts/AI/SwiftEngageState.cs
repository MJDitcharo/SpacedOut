using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiftEngageState : EngageState
{
    [SerializeField] float attackDistance;
    [SerializeField] AttackState attackState;

    [SerializeField] Vector3 dashPushback;
    Vector3 pushback;

    public override State RunCurrentState()
    {


        return this;
    }
}
