using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject hitEffect;
    public int damage = 10;

    private void Start()
    {
        Destroy(gameObject, 2);
    }

    private void OnCollisionEnter(Collision collision)
    {
        health HP = collision.gameObject.GetComponent<health>();
        if (HP != null)
        {
            HP.DoDamage(damage);
            Debug.Log("Damage Dealt");
        }

            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity); //create a bullet with no rotation at the postion 
            Destroy(effect, 0.2f);     //destroy game object and effect upon collison
            Destroy(gameObject);     //destroy game object and effect upon collisons
    }

   

}