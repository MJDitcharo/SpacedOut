using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int damage, delaySeconds;
    [SerializeField]
    bool keepDamaging;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("found: " + other.gameObject.name);
        health hp = other.gameObject.GetComponent<health>();
        if (hp != null)
            hp.burnTimer = 3;
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    keepDamaging = false;
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    StartCoroutine(DamageEntity(collision.collider));
    //}
    //IEnumerator DamageEntity(Collider other)
    //{
    //    health enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
    //    while (true)
    //    {
    //        Debug.Log("enter player");
    //        if (other.gameObject == GameManager.instance.player && keepDamaging)//damage player
    //            GameManager.instance.playerHealth.DoDamage(damage);
    //        else
    //        {
    //            Debug.Log("exitt");
    //            break;
    //        }
    //        yield return new WaitForSeconds(delaySeconds);

    //    }
    //}
}
