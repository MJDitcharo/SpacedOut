using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float explosiveRadius = 0;
    public float pushbackMultiplier = 1;
    public GameObject hitEffect;
    public int damage = 10;

    private void Start()
    {
        Destroy(gameObject, 10);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Store")
            return;
        playerHealth playerHP = other.gameObject.GetComponent<playerHealth>();
        if (playerHP != null && !playerHP.isDamageable)
            return;
        
        if(explosiveRadius > 0)
        {
            float distance = Vector3.Distance(transform.position, GameManager.instance.player.transform.position);
            float shakeMagnitude = 10 / (distance);
            ScreenShake.instance.StartCoroutine(ScreenShake.instance.ShakeScreen(1, shakeMagnitude));

            Collider[] colliders = Physics.OverlapSphere(transform.position, explosiveRadius);
            for(int i = 0; i < colliders.Length; i++)
            {
                health healthScript = colliders[i].GetComponent<health>();
                //RaycastHit hit;
                if(healthScript != null/* Physics.Raycast(transform.position, colliders[i].transform.position - transform.position, out hit, Mathf.Infinity) && hit.collider.gameObject == colliders[i].gameObject*/)
                {
                    if(colliders[i].GetComponent<EnemyMovement>() != null)
                        colliders[i].GetComponent<EnemyMovement>().pushback += (-pushbackMultiplier* (transform.position - colliders[i].transform.position).normalized);
                    healthScript.DoDamage(damage);
                }
            }
            Instantiate(hitEffect, transform.position, Quaternion.identity); //create a bullet with no rotation at the postion 
            Destroy(gameObject);
            
            //destroy game object and effect upon collisons
            return;
        }

        health HP = other.gameObject.GetComponent<health>();
        if (HP != null)
        {
            HP.DoDamage(damage);
            Debug.Log("Damage Dealt");
            AttackState AS = other.gameObject.GetComponent<AttackState>();
            EnemyMovement EM = other.gameObject.GetComponent<EnemyMovement>();
            if (other.tag != "Player")
            {
                EM.pushback = pushbackMultiplier * GetComponent<Rigidbody>().velocity.normalized; 
            }
            if(HP.isStunned == true)
            {
                AS.firerate *=  0.75f;
                EM.movementSpeed *= 0.75f;
            }
        }

        
        Instantiate(hitEffect, transform.position, Quaternion.identity); //create a bullet with no rotation at the postion 
        Destroy(gameObject);     //destroy game object and effect upon collisons
    }

   

}