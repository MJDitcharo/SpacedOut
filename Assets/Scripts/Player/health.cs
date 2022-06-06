using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    // Core Health Fields
    public int maxHealth = 100; 
    public int currHealth;


    // Vulnerable Debuff Fields
    private Coroutine vulnCoroutine;
    public bool vulnerable = false;
    public bool isStunned = false;
    [SerializeField] float fireTickTime = 1;
    float fireTick = 0;
    [SerializeField] float vulnAmount = 1.5f;
    [SerializeField] int vulnTime = 5;


    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
    }

    protected void Update()
    {
        fireTick += Time.deltaTime;
    }

    // Health takes damage
    public virtual void DoDamage(int _dmg)
    {
        StartCoroutine(GetComponent<EnemyFlashRed>().FlashRed());

        // Do more damage to people with vulnerable debuff
        if (vulnerable)
            currHealth = (int)(_dmg * 1.5f);
        else
            currHealth -= (int)_dmg;

        if (currHealth <= 0)
        {
            GetComponent<Collider>().enabled = false;
            Death();
        }
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }


    public void WeakenedDebuff()
    {

        // This is to prevent starting multiple coroutines at once
        if (!vulnerable)
        {
            vulnerable = true;
            vulnCoroutine = StartCoroutine(WeakenedState());
        }
        else
        {
            StopCoroutine(WeakenedState());
            vulnCoroutine = null;
            vulnCoroutine = StartCoroutine(WeakenedState());
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name);
        if(other.tag == "Fire")
        {
            Debug.Log("Touching Fire");
            if(fireTick >= fireTickTime)
            {
                Debug.Log("Burning");
                DoDamage(10);
                fireTick = 0;
            }
        }
    }

    protected IEnumerator WeakenedState()
    {
        // After a set time, the debuff wears off
        yield return new WaitForSeconds(vulnTime);
        vulnerable = false;
    }
}
