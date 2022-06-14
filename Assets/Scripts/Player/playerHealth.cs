using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerHealth : health
{

    public bool isDamageable = true;

    [SerializeField] float delayRate = 1f;
    private float delayDamge = 0f;
    [SerializeField] AudioClip playerHitAudio;
    Sound playerHitSound;

    private void Start()
    {
        playerHitSound = new Sound();
        playerHitSound.audio = playerHitAudio;
        playerHitSound.audioType = AudioStyle.sfx;

        currHealth = PlayerPrefs.GetInt("Player Health");
    }

    public void AddHealth(float percentage)
    {
        currHealth += (int)(maxHealth * percentage);
        if (currHealth > maxHealth)
            currHealth = maxHealth;

        GameManager.instance.healthBar.SetHealth((float)currHealth / maxHealth);
    }

    public override void DoDamage(int dmg)
    {
        if (!isDamageable)
            return;

        Debug.Log("Took damage");
        //AudioManager.Instance.PlaySFX(playerHitSound);
        delayDamge = Time.time + 1f / delayRate;
        currHealth -= dmg;
        DamagePopUpManager.Instance.StartCoroutine(DamagePopUpManager.Instance.DamageIndicator(dmg, gameObject.transform.position));
        ScreenShake.instance.StartCoroutine(ScreenShake.instance.ShakeScreen(.3f, 1));
        StartCoroutine(GetComponent<EnemyFlashRed>().FlashRed());
        GameManager.instance.healthBar.SetHealth((float)currHealth / maxHealth); 
        if (currHealth <= 0)
        {
            Death();
        }
    }

    public void AddMaxHealth(int value)
    {
        maxHealth += value;
        //change the ui max value
    }

    protected override void Death()
    {
        if(deathParticle != null)
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
        }
        GameManager.instance.gameOverScreen.SetActive(true);
        GameManager.instance.SetNormalCursor();
        gameObject.SetActive(false);
    }
}
