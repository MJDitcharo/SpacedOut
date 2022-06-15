using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Grenade : MonoBehaviour
{
    public GameObject hitEffect;
    public int damage = 50;
    public float detonateTime = 1f;
    public float explosiveRadius = 5;
    public float pushbackMultiplier = 5;
    [SerializeField] public Sound audioS;

    //when the grenade is instantiated
    private void Awake()
    { 
       StartCoroutine(Detonate());
    }
    public virtual IEnumerator Detonate()
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
        AudioManager.Instance.PlaySFX(audioS);
        Destroy(gameObject);     //destroy game object and effect upon detonation
    }
}
