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
    [SerializeField] GameObject goreEffect;
    [SerializeField] AudioClip explosionAudio;
    [SerializeField] AudioClip goreAudio;
    Sound explosionSound;
    Sound goreSound;

    Color[] initialColor;
    Coroutine flashing;

    Vector3 startingScale;

    private void Start()
    {
        explosionSound = new Sound();
        goreSound = new Sound();
        explosionSound.audio = explosionAudio;
        goreSound.audio = goreAudio;
        explosionSound.audioType = AudioStyle.sfx;
        goreSound.audioType = AudioStyle.sfx;

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
        }
        GameManager.instance.enemyCount--;
        if (GameManager.instance.enemyCount <= 0 && GameManager.instance.playerHealth.currHealth > 0)
        {
            GameManager.instance.checkpointIndex++;
            GameManager.instance.checkpoints[GameManager.instance.checkpointIndex].GetComponent<RoomManager>().EndLockDown();
            GameManager.instance.SaveGame();
            PlayerPrefs.SetInt("Chest Opened", 0);
        }
        //StopCoroutine(flashing);
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        Instantiate(goreEffect, transform.position, Quaternion.identity);
        AudioManager.Instance.PlaySFX(goreSound);
        AudioManager.Instance.PlaySFX(explosionSound);
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
