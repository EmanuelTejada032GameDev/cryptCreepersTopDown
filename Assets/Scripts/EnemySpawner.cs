using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [Range(1,10)][SerializeField] float spawnRate = 1;

    private void Start()
    {
        StartCoroutine("SpawnNewEnemy");
    }
    private Vector2 RandomSpawnPoint()
    {
        int x = Random.Range(-14, 19);
        int y = Random.Range(-6, 7);
        return new Vector2(x,y);
    }

    private void EnemyToSpawn()
    {
        int enemy = 0;
        if(Random.Range(0,10) < GameManager.Instance.dificulty * 0.1f)
        {
            enemy = 1;
        }
        Instantiate(enemyPrefabs[enemy], RandomSpawnPoint(), Quaternion.identity);
    }

    
    IEnumerator SpawnNewEnemy()
    {
        yield return new WaitForSeconds(1/spawnRate);
        EnemyToSpawn();
        StartCoroutine("SpawnNewEnemy");
    }
}
