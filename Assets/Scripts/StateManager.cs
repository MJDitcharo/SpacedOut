using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    State currentState;

    [SerializeField] IdleState idleState;
    [SerializeField] WanderState wanderState;

    public void Update()
    {
        ChangeState();
    }

    public void ChangeState()
    {
        currentState = currentState.RunCurrentState();
    }
}
