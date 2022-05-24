using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideAttackState : State
{
    [SerializeField] float timeToDeath = 1;
    float time;

    [SerializeField] float explodingDistance = 3f;
    [SerializeField] float pushbackMultiplier = 1;
    [SerializeField] float scaleChange = 1;

    [SerializeField] Color flashColor;
    [SerializeField] Renderer rend;
    [SerializeField] float flashInterval = .1f;
    [SerializeField] GameObject hitEffect;

    Color[] initialColor;
    Coroutine flashing;

    Vector3 startingScale;

    private void Start()
    {
        startingScale = transform.localScale;
        
        initialColor = new Color[rend.materials.Length];
        for (int i = 0; i < rend.materials.Length; i++)
        {
            initialColor[i] = rend.materials[i].color;
        }
    }

    public override State RunCurrentState()
    {
        if(flashing == null)
        {
            flashing = StartCoroutine(Flash(flashInterval));
        }
        if(timeToDeath <= time)
        {
            Explode();
            return this;
        }
        

        time += Time.deltaTime;
        transform.localScale = new Vector3(startingScale.x + time * scaleChange, startingScale.y + time * scaleChange, startingScale.z + time * scaleChange);

        movement.MoveToLocation(GameManager.instance.player.transform.position);

        return this;
    }

    void Explode()
    {
        if(Vector3.Distance(transform.position, GameManager.instance.player.transform.position) <= explodingDistance)
        {
            GameManager.instance.player.GetComponent<playerHealth>().DoDamage(10);
            GameManager.instance.movement.pushback += (-pushbackMultiplier * (transform.position - GameManager.instance.player.transform.position).normalized);
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
        GameManager.instance.enemyCount--;
        //StopCoroutine(flashing);
        Destroy(gameObject);
    }

    public IEnumerator Flash(float interval)
    {
        while(true)
        {
            for (int i = 0; i < initialColor.Length; i++)
            {
                rend.materials[i].color = flashColor;
            }
            yield return new WaitForSeconds(interval);
            for (int i = 0; i < initialColor.Length; i++)
            {
                rend.materials[i].color = initialColor[i];
            }
            yield return new WaitForSeconds(interval);
        }
        
    }
}
