using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlashRed : MonoBehaviour
{
    [SerializeField] Color collideColor;
    [SerializeField] Color normalColor;
    [SerializeField] Renderer rend;

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
        rend.material.color = collideColor;
        yield return new WaitForSeconds(flashTime);
        rend.material.color = normalColor;
    }

}
