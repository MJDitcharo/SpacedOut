using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerHealth : health
{

    public bool isDamageable = true;

    [SerializeField] float delayRate = 1f;
    private float delayDamge = 0f;


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
        delayDamge = Time.time + 1f / delayRate;
        currHealth -= dmg;
        StartCoroutine(GetComponent<EnemyFlashRed>().FlashRed());
        GameManager.instance.healthBar.SetHealth((float)currHealth / maxHealth);
        if (currHealth <= 0)
        {
            GameManager.instance.Respawn();
        }
    }

    public void AddMaxHealth(int value)
    {
        maxHealth += value;
        //change the ui max value
    }
}
