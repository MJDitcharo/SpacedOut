using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class health : MonoBehaviour
{
    // Core Health Fields
    public int maxHealth = 100; 
    public int currHealth;


    // Vulnerable Debuff Fields
    private Coroutine vulnCoroutine;
    public Coroutine stun;
    public bool vulnerable = false;
    public bool isStunned = false;

    public float burnTimer = 0;
    [SerializeField] float fireTickTime = 1;
    float fireTick = 0;
 
    [SerializeField] float vulnAmount = 1.5f;
    [SerializeField] int vulnTime = 5;
   
    [SerializeField] GameObject fireParticleEffect;
    [SerializeField] GameObject stunParticleEffect;
    [SerializeField] protected GameObject deathParticle;
    
    // Start is called before the first frame update
    void Start()
    {
        burnTimer = 0;
        currHealth = maxHealth;
    }

    protected void Update()
    {
        fireTick += Time.deltaTime;
        burnTimer -= Time.deltaTime;
        if (burnTimer > 0)
        {
            if (fireTick >= fireTickTime)
            {
                Debug.Log("Burning");
                DoDamage(10);

                Instantiate(fireParticleEffect, transform);
                fireTick = 0;
                //Debug.Break();
            }
        }
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
        DamagePopUpManager.Instance.StartCoroutine(DamagePopUpManager.Instance.DamageIndicator(_dmg, gameObject.transform.position));
        if (currHealth <= 0)
        {
            GetComponent<Collider>().enabled = false;
            Death();
        }
    }

    protected virtual void Death()
    {
        if (deathParticle != null)
            Instantiate(deathParticle, transform.position, Quaternion.identity);
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
        if(other.tag == "Fire")
        {
            Debug.Log("Touching Fire");
            if (burnTimer <= 0)
            {
                Instantiate(fireParticleEffect, transform);
                DoDamage(10);
            }
            burnTimer = 1.5f;
        }
    }

    protected IEnumerator WeakenedState()
    {
        // After a set time, the debuff wears off
        yield return new WaitForSeconds(vulnTime);
        vulnerable = false;
    }

    public void StunMethod()
    {
        stun = StartCoroutine(Stun());
    }
    public IEnumerator Stun()
    {
        GameObject particles = Instantiate(stunParticleEffect, transform);
        particles.transform.localScale = new Vector3(particles.transform.localScale.x * particles.transform.parent.localScale.x, particles.transform.localScale.y * particles.transform.parent.localScale.y, particles.transform.localScale.z * particles.transform.parent.localScale.z);

        Debug.Log("stunning");
        AttackState AS = gameObject.GetComponent<AttackState>();
        EnemyMovement EM = gameObject.GetComponent<EnemyMovement>();
        if(AS != null)
            AS.firerate *= 2f;
        if(EM != null)
            EM.GetAgent().speed *= .5f;

        yield return new WaitForSeconds(4);

        Debug.Log("stunning done");
        if (AS != null)
            AS.firerate *= .5f;
        if (EM != null)
            EM.GetAgent().speed *= 2f;
        
        Destroy(particles);
        stun = null;
    }

   
}
