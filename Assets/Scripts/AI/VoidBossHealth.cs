using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoidBossHealth : EnemyHealth
{
    [SerializeField] VoidBossAttacks bossAttacks;

    Image bossHealthBar;

    private void Start()
    {
        GameManager.instance.bossHealthBar.SetActive(true);
        bossHealthBar = GameManager.instance.bossHealthBar.transform.GetChild(1).GetComponent<Image>();
    }

    public override void DoDamage(int _dmg)
    {
        bossHealthBar.fillAmount = (float)currHealth / maxHealth;

        if (currHealth <= maxHealth / 3)
        {
            bossAttacks.bossIntensityMultiplier = 3;
        }

        base.DoDamage(_dmg);
    }

    protected override void Death()
    {
        GameManager.instance.bossHealthBar.SetActive(false);
        base.Death();
    }
}