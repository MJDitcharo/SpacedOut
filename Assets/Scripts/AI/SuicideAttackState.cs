using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideAttackState : State
{
    [SerializeField] float timeToDeath = 1;
    float time;

    [SerializeField] float explodingDistance = 3f;
    [SerializeField] float pushbackMultiplier = 1;

    public override State RunCurrentState()
    {
        if(timeToDeath <= time)
        {
            Explode();
            return this;
        }

        time += Time.deltaTime;

        movement.MoveToLocation(GameManager.instance.player.transform.position);

        return this;
    }

    void Explode()
    {
        if(Vector3.Distance(transform.position, GameManager.instance.player.transform.position) <= explodingDistance)
        {
            GameManager.instance.player.GetComponent<playerHealth>().DoDamage(10);
            GameManager.instance.movement.pushback += -pushbackMultiplier * (transform.position - GameManager.instance.player.transform.position).normalized;
        }

        Destroy(gameObject);
    }
}
