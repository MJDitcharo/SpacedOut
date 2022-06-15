using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeVoid : Grenade
{

    [SerializeField] GameObject childGrenade;
    [SerializeField] int clusterAmount = 3;
    [SerializeField] int force = 20;
    [SerializeField] public Sound audioS;

    public override IEnumerator Detonate()
    {
        yield return new WaitForSeconds(detonateTime);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosiveRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            health healthScript = colliders[i].GetComponent<health>();
            //RaycastHit hit;
            if (healthScript != null)
            {
                if (colliders[i].GetComponent<EnemyMovement>() != null)
                {
                    colliders[i].GetComponent<EnemyMovement>().pushback += (-pushbackMultiplier * (transform.position - colliders[i].transform.position).normalized);
                }
                healthScript.DoDamage(damage);
            }
        }
        Instantiate(hitEffect, transform.position, Quaternion.identity); //create a bullet with no rotation at the postion 
        AudioManager.Instance.PlaySFX(audioS);
        Destroy(gameObject);     //destroy game object and effect upon detonation
        for (int i = 0; i < clusterAmount; i++)
        {
            GameObject item = Instantiate(childGrenade, transform.position, Quaternion.identity);
            Rigidbody body = item.GetComponent<Rigidbody>(); //acess the rigidbody of the game object
            item.transform.rotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);
            body.AddForce(item.transform.forward * force, ForceMode.Impulse);
        }
    }
}
