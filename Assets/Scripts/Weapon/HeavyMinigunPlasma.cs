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
        StartCoroutine(BulletExplosion(other));

    }

    IEnumerator BulletExplosion(Collider other)
    {
        health HP = other.gameObject.GetComponent<health>();
        EnemyMovement MV = other.gameObject.GetComponent<EnemyMovement>();
        yield return new WaitForSeconds(2);

        if (HP != null)
        {
            HP.DoDamage(damage * 2);
            MV.pushback = -pushbackMultiplier * (other.transform.position - new Vector3(Random.Range(0, 100), 0 , Random.Range(0,100))).normalized;
        }


        Instantiate(hitEffect, other.transform.position, Quaternion.identity); //create a bullet with no rotation at the postion 
        Destroy(gameObject);
    }
}
