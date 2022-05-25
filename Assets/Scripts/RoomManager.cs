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
        Debug.Log("Locking down room!");

        collider.enabled = false;
        doorEnter.SetActive(true);
        doorExit.SetActive(true);

        StartCoroutine(enemySpawner.SpawnEnemies(spawnTime));
    }

    public void EndLockDown()
    {
        Debug.Log("Unlocking room!");

        doorEnter.SetActive(true);
        doorExit.SetActive(false);
        GameManager.instance.SaveGame();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        LockDownRoom();
    }
}
