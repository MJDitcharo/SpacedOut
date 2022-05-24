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
            Death();
        }
    }

    virtual public void Death()
    {
        GameManager.instance.enemyCount--;
        if (GameManager.instance.enemyCount <= 0)
        {
            if(GameManager.instance.checkpoints.Length != 0)
            {
                GameManager.instance.checkpointIndex++;
                GameManager.instance.checkpoints[GameManager.instance.checkpointIndex].GetComponent<RoomManager>().EndLockDown();
            }
        }
        Destroy(gameObject);
    }
}
