using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    [SerializeField] EngageState engageState;

    void Start()
    {
        
    }

    public override State RunCurrentState()
    {
        return engageState;
    }
}
