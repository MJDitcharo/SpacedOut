using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlashRed : MonoBehaviour
{
    [SerializeField] Color collideColor;
    [SerializeField] Color[] normalColor;
    [SerializeField] Renderer rend;

    [SerializeField] float flashTime = .1f;

    private void Start()
    {
        normalColor = new Color[rend.materials.Length];
        for(int i = 0; i < rend.materials.Length; i++)
        {
            normalColor[i] = rend.materials[i].color;
        }
    }

    private void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.tag == "Bullet")
        {
            StartCoroutine(FlashRed());
        }
    }

    IEnumerator FlashRed()
    {
        for(int i = 0; i < normalColor.Length; i++)
        {
            rend.materials[i].color = collideColor;
        }
        yield return new WaitForSeconds(flashTime);
        for (int i = 0; i < normalColor.Length; i++)
        {
            rend.materials[i].color = normalColor[i];
        }
    }

}
