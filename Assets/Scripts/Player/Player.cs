using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player movement
    public float jumpForce = 5f; // Force applied when the player jumps
    public Camera mainCamera; // Reference to the main camera
    public LayerMask groundMask; // Layer mask to identify the ground
    public float groundCheckDistance = 0.1f; // Distance to check for the ground

    private Rigidbody rb; // Reference to the player's Rigidbody
    private bool isGrounded; // Check if the player is on the ground

    //damage
    public int damage = 10; // Damage taken per collision
    public float damageCooldown = 1f; // Minimum time between applying damage
    private float lastDamageTime;



    void Start()
    {
        // Initialize the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Ensure the main camera is assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        lastDamageTime = -damageCooldown; // Initialize to allow immediate damage

    }

    void Update()
    {
        // Check if the player is on the ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        // Get input from WASD keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the movement direction relative to the camera
        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 movement = forward * moveVertical + right * moveHorizontal;

     

        // Apply movement
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);

        // Jump if the player is on the ground and the jump key is pressed
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ApplyDamage();
        }

        if(collision.gameObject.CompareTag("XP"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.playerXP += 10;
            GameManager.Instance.CheckLevelUp();

        }
    }
    private void ApplyDamage()
    {
        if (Time.time - lastDamageTime >= damageCooldown)
        {
            GameManager.Instance.TakeDamage(damage);
            lastDamageTime = Time.time;
        }
    }
}