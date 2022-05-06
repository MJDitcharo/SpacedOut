using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private int currentHealth;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        currentHealth = (int)slider.value;
    }

    public void SetHealth(int health)
    {
        if (health <= slider.maxValue && health >= slider.minValue)
            currentHealth = health;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
    }
    public void SetMinHealth(int health)
    {
        slider.minValue = health;
    }

}
