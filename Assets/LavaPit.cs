using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int damage, delaySeconds;
    bool keepDamaging ;

    private void OnTriggerEnter(Collider other)
    {
        keepDamaging = true;
        StartCoroutine(DamageEntity(other));
    }

    private void OnTriggerExit(Collider other)
    {
        keepDamaging = false;
    }
    IEnumerator DamageEntity(Collider other)
    {
        while (true)
        {
            Debug.Log("enter");
            if (other.gameObject == GameManager.instance.player && keepDamaging)
                GameManager.instance.playerHealth.DoDamage(damage);
            else
            {
                Debug.Log("exitt");
                break;
            }
            yield return new WaitForSeconds(delaySeconds);

        }
    }
}
