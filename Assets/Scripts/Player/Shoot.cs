using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab of the bullet
    public Transform bulletSpawnPoint; // Where the bullet will be spawned
    public float bulletSpeed = 20f; // Speed of the bullet
    public float shootCooldown = 1f; // Minimum time between shots
    private float lastShootTime;

    public Transform cameraTransform; // Reference to the main camera transform
    private float lastShotTime;


    void Start()
    {
        // Ensure the camera is assigned
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

      

    }

    void Update()
    {
      AutoShoot(); 
    }

    private void AutoShoot()
    {
        if (Time.time - lastShootTime >= shootCooldown)
        {
            GameObject closestEnemy = FindClosestEnemy();
            if (closestEnemy != null)
            {
                ShootAt(closestEnemy.transform);
                lastShootTime = Time.time;
            }
        }
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    private void ShootAt(Transform target)
    {
        Vector3 futurePosition = PredictFuturePosition(target.gameObject);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Vector3 direction = (futurePosition - bulletSpawnPoint.position).normalized;
        Vector3 velocity = direction * bulletSpeed;
        velocity.y = 0f; //doesnt shoot up 
        bullet.GetComponent<Rigidbody>().linearVelocity = velocity;
    }


    private Vector3 PredictFuturePosition(GameObject target)
    {
        Rigidbody targetRigidbody = target.GetComponent<Rigidbody>();
        Vector3 targetPosition = target.transform.position;
        Vector3 targetVelocity = targetRigidbody.linearVelocity;

        // Calculate the time to reach the target
        float distance = Vector3.Distance(bulletSpawnPoint.position, targetPosition);
        float timeToReachTarget = distance / bulletSpeed;

        // First step of Euler integration
        Vector3 firstStepPosition = targetPosition + targetVelocity * (timeToReachTarget / 2);

        // Second step of Euler integration
        Vector3 futurePosition = firstStepPosition + targetVelocity * (timeToReachTarget / 2);

        return futurePosition;
    }

}
