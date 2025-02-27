using JetBrains.Annotations;
using UnityEngine;
using System.Collections;


public class EnemySpawn : MonoBehaviour
{

    public GameObject enemyPrefab; // Prefab of the enemy to spawn
    public float minSpawnInterval = 1f; // Minimum time interval between spawns
    public float maxSpawnInterval = 5f; // Maximum time interval between spawns

    private void Start()
    {
        // Start the coroutine to spawn enemies at random intervals
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Wait for a random time interval between minSpawnInterval and maxSpawnInterval
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);

            // Instantiate the enemy prefab at the spawner's position
            if (GameManager.Instance.CanSpawnEnemy())
            {
                GameManager.Instance.EnemySpawned();
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}