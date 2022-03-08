using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject timeExtenderCheckpointPrefab;
    [SerializeField] float spawnTimeRate = 6f;
    [SerializeField] float spawnRadiusRange = 7f;
    
    [SerializeField] GameObject[] powerUpPrefabs;
    [SerializeField] float powerUpSpawnTimeRate = 10f;
    private void Start()
    {
        StartCoroutine(SpawnTimeExtender());
        StartCoroutine(SpawnPowerUp());
    }


    IEnumerator SpawnTimeExtender()
    {
        yield return new WaitForSeconds(spawnTimeRate);
        Instantiate(timeExtenderCheckpointPrefab, GenerateRandomSpawnPoint(), Quaternion.identity);
        StartCoroutine("SpawnTimeExtender");

    }

    private IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(powerUpSpawnTimeRate);
        int randomPowerUpIndex = Random.Range(0, powerUpPrefabs.Length);
        Debug.Log(powerUpPrefabs[randomPowerUpIndex].name);
        Instantiate(powerUpPrefabs[randomPowerUpIndex], GenerateRandomSpawnPoint(), Quaternion.identity);
        StartCoroutine("SpawnPowerUp");
    }

    private Vector2 GenerateRandomSpawnPoint()
    {
        return Random.insideUnitCircle * spawnRadiusRange;
    }



    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadiusRange);
    }
}
