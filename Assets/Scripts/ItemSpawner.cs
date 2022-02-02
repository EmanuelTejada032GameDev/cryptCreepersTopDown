using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject timeExtenderCheckpointPrefab;
    [SerializeField] float spawnTimeRate = 6f;
    [SerializeField] float spawnRadiusRange = 7f;
    

    private void Start()
    {
        StartCoroutine(SpawnTimeExtender());
    }


    IEnumerator SpawnTimeExtender()
    {
        yield return new WaitForSeconds(spawnTimeRate);
        Instantiate(timeExtenderCheckpointPrefab, GenerateRandomSpawnPoint(), Quaternion.identity);
        StartCoroutine("SpawnTimeExtender");

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
