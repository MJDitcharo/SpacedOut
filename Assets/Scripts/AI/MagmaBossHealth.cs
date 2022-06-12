using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagmaBossHealth : EnemyHealth
{
    [SerializeField] MagmaBossAttacks bossAttacks;
    [SerializeField] WanderState wanderState;

    Image bossHealthBar;

    private void Start()
    {
        GameManager.instance.bossHealthBar.transform.parent.parent.gameObject.SetActive(true);
        bossHealthBar = GameManager.instance.bossHealthBar.GetComponent<Image>();
    }

    public override void DoDamage(int _dmg)
    {
        bossHealthBar.fillAmount = (float)currHealth / maxHealth;

        if (currHealth <= maxHealth / 2)
        {
            bossAttacks.bossIntensityMultiplier = 2;
            wanderState.walkTimeMultiplier = 2;
        }

        base.DoDamage(_dmg);
    }

    protected override void Death()
    {
        GameManager.instance.bossHealthBar.transform.parent.parent.gameObject.SetActive(false);
        base.Death();
    }
}
