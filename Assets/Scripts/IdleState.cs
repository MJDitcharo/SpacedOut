using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    [SerializeField] WanderState wanderState;

    void Start()
    {
        
    }

    public override State RunCurrentState()
    {
        return wanderState;
    }
}
