using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State
{
    //[SerializeField] EngageState engageState;
    [SerializeField] State attackState;

    [SerializeField] float noticeDistance;
    [SerializeField] float movementSpeed = 3;
    [SerializeField] float walkTime = 5;
    float walkTimer = 0;
    [SerializeField] float obsticleAvoidanceDistance = 6;
    [SerializeField] GameObject fire;
    [SerializeField] float turnSpeed = 90;
    Coroutine lookCoroutine;
    [SerializeField] LayerMask layerMask;

    public override State RunCurrentState()
    {
        if(walkTimer >= walkTime)
        {
            walkTimer = 0;
            return attackState;
        }

        movement.GetAgent().Move(transform.forward * movementSpeed * Time.deltaTime);
        walkTimer += Time.deltaTime;

        if (lookCoroutine == null && Physics.Raycast(transform.position, transform.forward, obsticleAvoidanceDistance, layerMask))
            lookCoroutine = StartCoroutine(Turn());

        return this;
    }

    IEnumerator Turn()
    {
        Quaternion lookRoation = Quaternion.Euler(0, transform.rotation.y + 15, 0);

        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, time);

            time += Time.deltaTime * turnSpeed;

            yield return null;
        }
        lookCoroutine = null;
    }
}
