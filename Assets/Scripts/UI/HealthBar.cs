using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image image;
    int maxHealth;
    [SerializeField]
    RectTransform rectTransform;
    int moreHealth = 20;

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

    public void IncreaseMaxHealth()
    {
        int increaseSize = moreHealth * 2;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x + increaseSize, rectTransform.sizeDelta.y); //increase the width of the bar
        rectTransform.position = new Vector3(rectTransform.position.x + increaseSize / 2, rectTransform.position.y, rectTransform.position.z); //reposition the bar

        GameManager.instance.playerHealth.AddMaxHealth(moreHealth);
    }

}
