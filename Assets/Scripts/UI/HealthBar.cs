using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image image;
    

    private void Awake()
    {
        image.fillAmount = 1;
    }

    /// <summary>
    /// Pass in as a number health where: 0 < health < 1
    /// </summary>
    /// <param name="health"></param>
    public void SetHealth(float health)
    {
            image.fillAmount = health;
    }

    public void AddHealth(float health)
    {
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

}
