using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int burnTimer;
    [SerializeField]
    bool keepDamaging; //resets the timer 

    [SerializeField]
    GameObject fire;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Fire damage");
        DealFireDamage(other);
    }

    private void OnTriggerStay(Collider other)
    {
        //if (keepDamaging)
        //    KeepDamaging(other);
    }
    private void OnCollisionEnter(Collision collision)
    {
        DealFireDamage(collision.collider);
    }

    private void OnCollisionStay(Collision collision)
    {
        //if (keepDamaging)
        //    KeepDamaging(collision.collider);
    }
    private void OnCollisionExit(Collision other)
    {
        health hp = other.gameObject.GetComponent<health>();
        if (hp != null)
            StartCoroutine(EndEffect(other.collider));
    }

    private void OnTriggerExit(Collider other)
    {
        health hp = other.gameObject.GetComponent<health>();
        if (hp != null)
            StartCoroutine(EndEffect(other));
    }

    private void DealFireDamage(Collider other)
    {
        Debug.Log("found: " + other.gameObject.name);
        health hp = other.gameObject.GetComponent<health>();
        if (hp != null)
        {
            hp.burnTimer = burnTimer;
            hp.fireParticleEffect = Instantiate(fire, other.transform.position, Quaternion.Euler(270, other.transform.rotation.y, other.transform.rotation.x), other.transform);
        }
    }

    private void KeepDamaging(Collider other)
    {
        health hp = other.gameObject.GetComponent<health>();
        if (hp != null)
            hp.burnTimer = burnTimer;
    }

    private void StopFireDamage(Collider other)
    {
        Debug.Log("stopping: " + other.gameObject.name);
        health hp = other.gameObject.GetComponent<health>();
        if (hp != null)
        {
            hp.burnTimer = 0;
            if (hp.fireParticleEffect != null)
                Destroy(hp.fireParticleEffect);
        }
    }

    private IEnumerator EndEffect(Collider other)
    {
        health hp = other.gameObject.GetComponent<health>();
        if (hp != null)
        {
            yield return new WaitForSeconds(hp.burnTimer);
            StopFireDamage(other);
        }
    }
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

