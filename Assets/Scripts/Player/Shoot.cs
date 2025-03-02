using UnityEngine;
using Unity.Cinemachine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab of the bullet
    public Transform bulletSpawnPoint; // Where the bullet will be spawned
    public float bulletSpeed = 20f; // Speed of the bullet
    public CinemachineCamera playerCamera; // Reference to the player's Cinemachine camera
    public float shootCooldown = 1f; // Minimum time between shots
    private float lastShootTime;
    public float spawnDistance = 1f; // Distance in front of the player to spawn the bullet

    private void Start()
    {
        lastShootTime = -shootCooldown; // Initialize to allow immediate shooting
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time - lastShootTime >= shootCooldown) // Check for left mouse button click and cooldown
        {
            ShootClick();
            lastShootTime = Time.time; // Update the last shoot time
        }
    }

    private void ShootClick()
    {  
        // Calculate the spawn position in front of the player, in line with the camera
        Vector3 spawnPosition = transform.position + playerCamera.transform.forward * spawnDistance;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Vector3 direction = playerCamera.transform.forward; // Get the direction the camera is looking
        bullet.GetComponent<Rigidbody>().linearVelocity = direction * bulletSpeed;
    }
}