using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlashRed : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    Color collideColor = Color.red;
    Color normalColor = Color.yellow;

    private void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.tag == "Bullet")
        {
            StartCoroutine(FlashRed(enemy));
        }
    }

    IEnumerator FlashRed(GameObject target)
    {
        target.GetComponent<Renderer>().material.color = collideColor;
        yield return new WaitForSeconds(0.2f);
        target.GetComponent<Renderer>().material.color = normalColor;
    }

}
