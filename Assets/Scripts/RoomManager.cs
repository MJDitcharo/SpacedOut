using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject doorEnter;
    [SerializeField] GameObject doorExit;
    public Collider collider;

    [SerializeField] EnemySpawner enemySpawner;

    public void LockDownRoom()
    {
        collider.enabled = false;
        doorEnter.SetActive(true);
        doorExit.SetActive(true);

        StartCoroutine(enemySpawner.SpawnEnemies(3));
    }

    public void EndLockDown()
    {
        doorExit.SetActive(false);
        doorEnter.SetActive(true);

        GameManager.instance.SaveGame();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        LockDownRoom();
    }
}
