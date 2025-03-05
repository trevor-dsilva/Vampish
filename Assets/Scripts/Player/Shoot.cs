using UnityEngine;
using Unity.Cinemachine;

public class NewShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab of the bullet
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
        if ((Input.GetMouseButtonDown(0)|| Input.GetMouseButton(0)) && Time.time - lastShootTime >= shootCooldown) // Check for left mouse button click and cooldown
        {
            FireBullet();
            lastShootTime = Time.time; // Update the last shoot time
        }
    }

    private void FireBullet()
    {

        // Calculate the spawn position in front of the player, in line with the camera
        Vector3 spawnPosition = transform.position + playerCamera.transform.forward * spawnDistance;

        // Get the y position of the spawn point
        float lockedYPosition = transform.position.y;

        // Lock the y position to the spawn point's y value
        spawnPosition.y = lockedYPosition;

        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        // Get the direction the camera is looking, but lock the y component
        Vector3 direction = playerCamera.transform.forward;
        direction.y = 0; // Lock the y component to 0
        direction.Normalize(); // Normalize to maintain the correct direction

        bullet.GetComponent<Rigidbody>().linearVelocity = direction * bulletSpeed;
    }
}