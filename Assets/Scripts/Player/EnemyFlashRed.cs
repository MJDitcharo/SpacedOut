using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlashRed : MonoBehaviour
{
    Color collideColor = Color.red;
    Color normalColor = Color.yellow;

    [SerializeField] float flashTime = .1f;

    private void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.tag == "Bullet")
        {
            StartCoroutine(FlashRed());
        }
    }

    IEnumerator FlashRed()
    {
        GetComponent<Renderer>().material.color = collideColor;
        yield return new WaitForSeconds(flashTime);
        GetComponent<Renderer>().material.color = normalColor;
    }

}
