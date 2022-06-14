using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnPoints = new();
    [SerializeField] GameObject[] enemies;
    [SerializeField] bool isRandom;
    private void Start()
    {
        if (isRandom)
        {
            for (int i = 0; i < transform.childCount; i++)
                spawnPoints.Add(transform.GetChild(i).gameObject);
        }
    }

    public IEnumerator SpawnEnemies(float time)
    {
        List<GameObject> temp = new List<GameObject>(spawnPoints);
        yield return new WaitForSeconds(time);
        for (int i = 0; i < enemies.Length; i++)
        {
            Transform spawnPoint;
            if (isRandom)
                spawnPoint = temp[(int)Random.Range(0, temp.Count - 1)].transform;
            else
                spawnPoint = spawnPoints[i].transform;
            Instantiate(enemies[i], spawnPoint.position, Quaternion.identity);
            temp.Remove(spawnPoint.gameObject);
            GameManager.instance.enemyCount++;
        }
    }
}
