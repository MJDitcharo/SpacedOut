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
        deathSound = new Sound();
        deathSound.audio = deathAudio;
        deathSound.audioType = AudioStyle.sfx;

        GameManager.instance.bossHealthBar.transform.parent.parent.gameObject.SetActive(true);
        bossHealthBar = GameManager.instance.bossHealthBar.GetComponent<Image>();
    }

    public override void DoDamage(int _dmg)
    {
        bossHealthBar.fillAmount = (float)currHealth / maxHealth;

        if (currHealth <= maxHealth / 3)
        {
            bossAttacks.bossIntensityMultiplier = 3;
        }
        else if(currHealth <= 2 * maxHealth / 3)
        {
            bossAttacks.bossIntensityMultiplier = 2;
        }

        base.DoDamage(_dmg);
    }

    protected override void Death()
    {
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Enemy");

        for(int i = 0; i < clones.Length; i++)
        {
            if(clones[i] != gameObject)
            {
                Destroy(clones[i]);
            }
        }

        GameManager.instance.bossHealthBar.transform.parent.parent.gameObject.SetActive(false);
        base.Death();
    }
}
