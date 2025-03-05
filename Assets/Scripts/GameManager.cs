using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int maxEnemies = 10; // Maximum number of enemies allowed
    private int currentEnemyCount = 0;

    public int playerHealth = 100;
    public int playerXP = 0;


    private void Awake()
    {
        // Ensure that only one instance of the GameManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CanSpawnEnemy()
    {
        return currentEnemyCount < maxEnemies;
    }

    public void EnemySpawned()
    {
        currentEnemyCount++;
    }

    public void EnemyDestroyed()
    {
        currentEnemyCount--;
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            // Handle player death
            playerHealth = 0;
            Debug.Log("Player is dead");
        }
    }
}