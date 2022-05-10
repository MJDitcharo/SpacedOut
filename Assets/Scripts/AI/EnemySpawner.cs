using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    List<GameObject> spawnPoints;
    GameObject[] enemies;

    private void OnEnable()
    {
        spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy Spawn"));
    }

    public void SpawnEnemies()
    {
        for(int i = 0; i < enemies.Length; i++)
        {
            Transform spawnPoint = spawnPoints[(int)Random.Range(0, spawnPoints.Count - 1)].transform;
            Instantiate(enemies[i], spawnPoint.position, Quaternion.identity);
            spawnPoints.Remove(spawnPoint.gameObject);
        }
    }
}
