using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : health
{
    public GameObject drops;
    public int dropQuantity;
    // Start is called before the first frame update
    protected void Start()
    {
        deathSound = new Sound();
        deathSound.audio = deathAudio;
        deathSound.audioType = AudioStyle.sfx;
    }

    protected override void Death()
    {
        GameManager.instance.enemyCount--;
        if (GameManager.instance.enemyCount <= 0 && GameManager.instance.playerHealth.currHealth > 0)
        {
            GameManager.instance.checkpointIndex++;
            GameManager.instance.checkpoints[GameManager.instance.checkpointIndex].GetComponent<RoomManager>().EndLockDown();
            GameManager.instance.SaveGame();
            PlayerPrefs.SetInt("Chest Opened", 0);
        }
        GameObject drop = Instantiate(drops, gameObject.transform.position, Quaternion.identity);
        drop.GetComponent<SkrapPickup>().quantity = dropQuantity;
        if (deathParticle != null)
            Instantiate(deathParticle, transform.position, Quaternion.identity);
        AudioManager.Instance.PlaySFX(deathSound);
        Destroy(gameObject);

    }
}
