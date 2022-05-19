using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public GameObject doorEnter;
    [SerializeField] GameObject doorExit;
    public Collider collider;

    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] float spawnTime = 1;

    public void LockDownRoom()
    {
        collider.enabled = false;
        GameManager.instance.Lockdown = true;

        StartCoroutine(enemySpawner.SpawnEnemies(spawnTime));
    }

    public void EndLockDown()
    {
        GameManager.instance.Lockdown = false;
        GameManager.instance.SaveGame();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        LockDownRoom();
    }
}
