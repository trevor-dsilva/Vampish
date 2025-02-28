using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string playerTag = "Player"; // Tag assigned to the player GameObject
    [SerializeField]public float maxDistance = 50f; // Maximum distance the projectile can travel

    private Vector3 startPosition; // Position where the projectile was instantiated

    void Awake()
    {
        // Store the initial position of the projectile
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the distance traveled
        float distanceTraveled = Vector3.Distance(startPosition, transform.position);

        // Destroy the projectile if it has traveled the maximum distance
        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is not the player
        if (collision.gameObject.tag != "Player")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Destroy(collision.gameObject);
                GameManager.Instance.EnemyDestroyed();            }
            // Destroy the projectile

            
            
        }
        Destroy(gameObject);
    }
}