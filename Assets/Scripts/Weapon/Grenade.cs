using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Grenade : MonoBehaviour
{
    public GameObject hitEffect;
    private float detonateTime = 1f;
    private float explosiveRadius = 5;
    private float pushbackMultiplier = 5;
    private int damage = 50;

    //when the grenade is instantiated
    private void Awake()
    { 
       StartCoroutine(Detonate());
    }
    public IEnumerator Detonate()
    {
        yield return new WaitForSeconds(detonateTime);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosiveRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            health healthScript = colliders[i].GetComponent<health>();
            //RaycastHit hit;
            if (healthScript != null /*&& Physics.Raycast(transform.position, colliders[i].transform.position - transform.position, out hit, Mathf.Infinity) && hit.collider.gameObject == colliders[i].gameObject*/)
            {
                if(colliders[i].GetComponent<EnemyMovement>() != null)
                {
                    colliders[i].GetComponent<EnemyMovement>().pushback += (-pushbackMultiplier * (transform.position - colliders[i].transform.position).normalized);
                }
                healthScript.DoDamage(damage);
            }
        }
        Instantiate(hitEffect, transform.position, Quaternion.identity); //create a bullet with no rotation at the postion 
        Destroy(gameObject);     //destroy game object and effect upon detonation
    }
}
