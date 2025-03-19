using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int maxEnemies = 10; // Maximum number of enemies allowed
    private int currentEnemyCount = 0;
    //stats level
    public int playerXP = 0;
    public int experienceToNextLevel = 10;
    public int playerLevel = 1;
    
    //stats per level
    public int playerHealth = 100;
    public int playerPower = 5;
    public float shotCooldown = 1f;
    public float detectionInterval= 2f;
    public float detectionRadius = 10f;



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

    public void CheckLevelUp()
    {
        while (playerXP >= experienceToNextLevel)
        {
            playerXP -= experienceToNextLevel; // Deduct the experience required for the current level
            playerLevel++;
            experienceToNextLevel = CalculateExperienceForNextLevel(playerLevel); // Calculate the experience required for the next level
            OnLevelUp(); // Handle level up actions
        }
    }

    private int CalculateExperienceForNextLevel(int level)
    {
        // Example formula: Experience required increases by 50% each level
        return Mathf.FloorToInt(experienceToNextLevel * 1.5f);
    }

    private void OnLevelUp()
    {
        playerHealth += 10;
        playerPower += 1;
        maxEnemies++;
        shotCooldown -= .01f;
        detectionRadius += .5f;
        detectionInterval -= .02f; 
        
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