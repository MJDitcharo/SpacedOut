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

    public void SetHealth(float health)
    {
            image.fillAmount = health;
    }

}
