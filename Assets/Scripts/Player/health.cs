using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currHealth;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
    }

    

    // Health takes damage
    public virtual void DoDamage(int _dmg)
    {
        StartCoroutine(GetComponent<EnemyFlashRed>().FlashRed());

        currHealth -= _dmg;
        if(currHealth <= 0)
        {
            GetComponent<Collider>().enabled = false;
            Death();
        }
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }
}
