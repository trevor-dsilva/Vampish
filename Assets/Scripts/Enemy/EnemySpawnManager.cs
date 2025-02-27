using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemySpawnerPrefab; // Prefab of the enemy spawner
    public int numberOfSpawners = 5; // Number of spawners
    public float radius = 10f; // Radius of the circle around the player
    public float rotationSpeed = 30f; // Speed of rotation

    private GameObject[] spawners;

    void Start()
    {
        // Initialize the spawners array
        spawners = new GameObject[numberOfSpawners];

        // Create spawners in a circle around the player
        for (int i = 0; i < numberOfSpawners; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfSpawners;
            Vector3 position = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            spawners[i] = Instantiate(enemySpawnerPrefab, transform.position + position, Quaternion.identity);
            spawners[i].transform.parent = transform; // Attach spawner to player
        }
    }

    void Update()
    {
        // Rotate spawners around the player
        for (int i = 0; i < numberOfSpawners; i++)
        {
            spawners[i].transform.RotateAround(transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}