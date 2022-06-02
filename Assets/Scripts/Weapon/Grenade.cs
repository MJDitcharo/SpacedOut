using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Grenade : MonoBehaviour
{
    private float detonateTime = 3f;
    private float explosiveRadius = 10;
    private float pushbackMultiplier = 1;
    private int damage = 10;

    //when the grenade is instantiated
    private void Awake()
    {
        Detonate();
    }
    public IEnumerator Detonate()
    {
        yield return new WaitForSeconds(detonateTime);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosiveRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            health healthScript = colliders[i].GetComponent<health>();
            if (healthScript != null)
            {
                colliders[i].GetComponent<EnemyMovement>().pushback += (-pushbackMultiplier * (transform.position - colliders[i].transform.position).normalized);
                healthScript.DoDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
