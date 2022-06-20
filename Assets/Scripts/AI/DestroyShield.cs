using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyShield : MonoBehaviour
{
    public int shieldHealth;
    public int currentShieldHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentShieldHealth = shieldHealth;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        DamageShield(other.GetComponent<bullet>().damage);
        if(currentShieldHealth <= 0)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void DamageShield(int damage)
    {
        currentShieldHealth -=damage;
        DamagePopUpManager.Instance.StartCoroutine(DamagePopUpManager.Instance.DamageIndicator(damage, gameObject.transform.position));

        if (currentShieldHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
