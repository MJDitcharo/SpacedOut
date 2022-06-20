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
        deathSound = new Sound();
        deathSound.audio = deathAudio;
        deathSound.audioType = AudioStyle.sfx;

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
        GameObject[] fire = GameObject.FindGameObjectsWithTag("BossFire");
        for (int i = 0; i < fire.Length; i++)
        {
            Destroy(fire[i]);
        }
        base.Death();
    }
}
