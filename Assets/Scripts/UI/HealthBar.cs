using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image image;

    private float currentHealth;
    private void Awake()
    {
        currentHealth = image.fillAmount;
    }

    public void SetHealth(float health)
    {
        if (health <= 1 && health >= 0)
            currentHealth = health;
    }

}
