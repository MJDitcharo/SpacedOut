using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawedOffPlasma : bullet
{

    public float fireDelay = 1f;


    public override void OnTriggerEnter(Collider other)
    {
        health HP = other.gameObject.GetComponent<health>();
        if (HP != null)
        {
            HP.DoDamage(damage);

            for (int i = 0; i < 10; i++)
            {
                StartCoroutine(Delay());
                HP.DoDamage(damage);
            }
        }


        Instantiate(hitEffect, transform.position, Quaternion.identity); //create a bullet with no rotation at the postion 
        Destroy(gameObject);     //destroy game object and effect upon collisons
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(fireDelay);
    }

}
