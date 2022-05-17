using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideAttackState : State
{
    public override State RunCurrentState()
    {
        return this;
    }
}
