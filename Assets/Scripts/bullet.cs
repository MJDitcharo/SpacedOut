using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public GameObject hiteffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            GameObject effect = Instantiate(hiteffect, transform.position, Quaternion.identity); //create a bullet with no rotation at the postion 
            Destroy(effect, 2f);     //destroy game object and effect upon collison
            Destroy(gameObject);     //destroy game object and effect upon collisons
        }
    }
}