using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State
{
    [SerializeField] EngageState engageState;

    [SerializeField] float noticeDistance;

    // Start is called before the first frame update
    void Start()
    {

    }

    public override State RunCurrentState()
    {
        if(Vector2.Distance(transform.position, GameManager.instance.player.transform.position) <= noticeDistance)
        {
            return engageState;
        }

        return this;
    }
}
