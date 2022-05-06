using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlashRed : MonoBehaviour
{
    public SpriteRenderer sprite;

    private void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.tag == "Bullet")
        {
            StartCoroutine(FlashRed());
        }
    }

    public IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
