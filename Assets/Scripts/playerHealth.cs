using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public float health;
    float damage = 2f;
    public bool isDamageable = true;

    [SerializeField] float currentHealth = 100;
    [SerializeField] float delayRate = 1f;
    private float delayDamge = 0f;
    // Start is called before the first frame update
    void Start()
    {
        health = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = currentHealth;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("floor") && Time.time >= delayDamge)
        {
            DoDamge();
        }
    }

    void DoDamge()
    {
        if (isDamageable)
        {
            delayDamge = Time.time + 1f / delayRate;
            currentHealth -= damage;
            if (health == 0)
            {
                Debug.Log("Dead");
            }
        }
    }
}
