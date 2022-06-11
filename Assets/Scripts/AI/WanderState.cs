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
    public float walkTimeMultiplier = 1;
    float walkTimer = 0;
    [SerializeField] float obsticleAvoidanceDistance = 6;
    [SerializeField] GameObject fire;
    [SerializeField] float turnSpeed = 90;
    [SerializeField] float minimunTurnTime = .5f;
    Coroutine lookCoroutine = null;
    [SerializeField] LayerMask layerMask;

    public override State RunCurrentState()
    {
        

        if (lookCoroutine == null && Physics.Raycast(transform.position, transform.forward, obsticleAvoidanceDistance, layerMask))
            lookCoroutine = StartCoroutine(Turn());
        else if(walkTimer >= walkTime / walkTimeMultiplier)
        {
            walkTimer = 0;
            animator.SetBool("Running", false);

            return attackState;
        }
        else
        {
            movement.GetAgent().Move(transform.forward * movementSpeed * Time.deltaTime);
            animator.SetBool("Running", true);
        }
        walkTimer += Time.deltaTime;

        return this;
    }

    IEnumerator Turn()
    {
        Quaternion lookRoation = Quaternion.Euler(0, transform.rotation.y , 0);

        float time = 0;

        while (time < minimunTurnTime)
        {
            Debug.Log("turning");
            transform.Rotate(0, turnSpeed * Time.deltaTime, 0);

            time += Time.deltaTime;

            yield return null;
        }
        lookCoroutine = null;
    }
}
