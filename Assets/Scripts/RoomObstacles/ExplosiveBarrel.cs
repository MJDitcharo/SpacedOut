using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField] int timesHit = 0;
    [SerializeField] LayerMask layers;
    public bool hasExploded = false;
    private void OnTriggerEnter(Collider other)
    {
        timesHit++;
        StartCoroutine(Explode());
    }
    IEnumerator Explode()
    {
        while (true)
        {
            if (timesHit == 1)
            {
                yield return new WaitForSeconds(3f);
            }
            if (timesHit == 2)
            {
                yield return new WaitForSeconds(1.5f);
            }
            else if(timesHit >= 3)
            {
                yield return  new WaitForSeconds(0.1f);
            } 
            hasExploded = true;
            if(hasExploded == true)
            {
                ExplosiveDamage();
            }
            Destroy(gameObject);
        }
    }

    void ExplosiveDamage()
    {
        int damage = 200;
        int radius = 20;
        Collider[] colliderCount = new Collider[15];
        int colliders = Physics.OverlapSphereNonAlloc(transform.position, radius, colliderCount,layers);
        for (int i = 0; i < colliders; i++)
        {
            if(colliderCount[i].tag == "Player")
            {
                float distance = Vector3.Distance(colliderCount[i].transform.position, transform.position);
                damage /= (int)distance;
                colliderCount[i].GetComponent<playerHealth>().DoDamage(damage);
               
            }
            else if (colliderCount[i].tag == "Enemy")
            {
                float distance = Vector3.Distance(colliderCount[i].transform.position, transform.position);
                damage /= (int)distance;
                colliderCount[i].GetComponent<EnemyHealth>().DoDamage((damage));
            }
        }

    }
}
