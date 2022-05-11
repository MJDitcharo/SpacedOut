using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerHealth : health
{

    public bool isDamageable = true;

    [SerializeField] float delayRate = 1f;
    private float delayDamge = 0f;




    public override void DoDamage(int dmg)
    {
        if (isDamageable)
        {

            delayDamge = Time.time + 1f / delayRate;
            currHealth -= dmg;
            GameManager.instance.healthBar.SetHealth((float)currHealth / maxHealth);
            if (currHealth <= 0)
            {
                GameManager.instance.Respawn();
            }
        }
    }
}
