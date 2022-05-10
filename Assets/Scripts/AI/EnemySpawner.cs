using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnPoints;
    [SerializeField] GameObject[] enemies;
    private void Start()
    {
        //spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy Spawn"));
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(3);
        for(int i = 0; i < enemies.Length; i++)
        {
            Transform spawnPoint = spawnPoints[(int)Random.Range(0, spawnPoints.Count - 1)].transform;
            Instantiate(enemies[i], spawnPoint.position, Quaternion.identity);
            spawnPoints.Remove(spawnPoint.gameObject);
        }
    }
}
