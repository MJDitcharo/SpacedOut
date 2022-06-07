using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyMinigunPlasma : bullet
{

    public override void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        playerHealth playerHP = other.gameObject.GetComponent<playerHealth>();
        if (playerHP != null && !playerHP.isDamageable)
            return;


        health HP = other.gameObject.GetComponent<health>();
        if (HP != null)
        {
            HP.DoDamage(damage);
        }

        rb.velocity = new Vector3(0, 0, 0);
        GetComponent<Collider>().enabled = false;
        if(explosion == null)
            explosion = StartCoroutine(BulletExplosion(other));

    }

    IEnumerator BulletExplosion(Collider other)
    {
        Debug.Log("Exploding");
        health HP = other.gameObject.GetComponent<health>();
        EnemyMovement MV = other.gameObject.GetComponent<EnemyMovement>();
        yield return new WaitForSeconds(2f);
        Debug.Log("past coroutine");
        Debug.Log("Boom");
        if (HP != null)
        {

            HP.DoDamage(damage * 2);
            MV.pushback = -pushbackMultiplier * (other.transform.position - new Vector3(Random.Range(0, 10), 0 , Random.Range(0, 10))).normalized;
        }


        Instantiate(hitEffect, other.transform.position, Quaternion.identity); //create a bullet with no rotation at the postion 
        Destroy(gameObject);
    }
}
