using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isHitEnemy : MonoBehaviour
{
    [SerializeField] int isHit = 0;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            isHit++;
            if (isHit == 2 && gameObject.CompareTag("Enemy2"))
            {
                Destroy(gameObject);
            }
        }
    }
}