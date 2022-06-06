using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBullets : bullet
{
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        AttackState AS = other.gameObject.GetComponent<AttackState>();
        EnemyMovement EM = other.gameObject.GetComponent<EnemyMovement>();
        health hp = other.gameObject.GetComponent<health>();
        hp.isStunned = true;
        if (hp.isStunned == true)
        {
            AS.firerate *= 0.75f;
            EM.movementSpeed *= 0.75f;
        }
    }
}
