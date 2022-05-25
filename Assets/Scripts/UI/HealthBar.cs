using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image image;
    int maxHealth;


    private void Awake()
    {
        image.fillAmount = 1;
        maxHealth = (int)(image.fillAmount * 100);
    }

    /// <summary>
    /// Pass in as a number health where: 0 < health < 1
    /// </summary>
    /// <param name="health"></param>
    public void SetHealth(float health)
    {
        image.fillAmount = health;
    }

    /// <summary>
    /// Pass in as a number health where: 0 < health < 1
    /// </summary>
    /// <param name="health"></param>
    public void AddHealth(float health)
    {
        if (health + image.fillAmount > GameManager.instance.playerHealth.maxHealth)
            image.fillAmount = (float)(GameManager.instance.playerHealth.maxHealth * .01f);
        else
            image.fillAmount += health;
    }

    public float GetHealthFloat()
    {
        return image.fillAmount;
    }

    public int GetHealthInt()
    {
        return (int)(image.fillAmount * 100);
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

}
