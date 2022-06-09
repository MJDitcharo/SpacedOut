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

    [SerializeField] GameObject damagePopUP;
    TextMeshProUGUI tmp;
    Color color;

    
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
        if(burnTimer > 0)
        {
            if (fireTick >= fireTickTime)
            {
                Debug.Log("Burning");
                DoDamage(10);
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
        StartCoroutine(DamageIndicator(_dmg));
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

    public void StunMethod()
    {
        stun = StartCoroutine(Stun());
    }
    public IEnumerator Stun()
    {
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

        stun = null;
    }

    public IEnumerator DamageIndicator(int damage)
    {
        int seconds = 15;
        float distance = 1;
        float fadeSpeed = 1f;
        GameObject go = Instantiate(damagePopUP, gameObject.transform.position + new Vector3(0,5,0), Quaternion.Euler(90,0,0));
        tmp = go.GetComponent<TextMeshProUGUI>();
        tmp.text = "-" + damage.ToString();
        color = tmp.color;

        while (seconds > 0)
        {
            go.transform.position = Vector3.Lerp(go.transform.position, go.transform.position + new Vector3(distance, 0, 0), 1);
            yield return new WaitForEndOfFrame();
            distance *= 0.5f;
            seconds--;

        }
        while(seconds <= 0)
        {
       
            color.a -= fadeSpeed * Time.deltaTime;
            tmp.color = color;
            yield return null;
            if(tmp.color.a <= 0f)
            {
                Destroy(go);
            }
        }

    }
}
