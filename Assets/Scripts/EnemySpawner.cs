using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [Range(1,10)][SerializeField] float spawnRate = 1;
    [SerializeField] private GameObject[] spawnPoints;

    private void Start()
    {
        StartCoroutine("SpawnNewEnemy");
    }

    IEnumerator SpawnNewEnemy()
    {
        yield return new WaitForSeconds(1/spawnRate);
        EnemyToSpawn();
        StartCoroutine("SpawnNewEnemy");
    }

    private void EnemyToSpawn()
    {
        int enemy = Random.Range(0, enemyPrefabs.Length);
       
        Instantiate(enemyPrefabs[enemy], RandomSpawnPoint(), Quaternion.identity);
    }

    private Vector2 RandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length - 1)].transform.position;
    }
    
}
