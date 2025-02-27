using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]public float followSpeed = 2.0f; // Speed at which the object follows the player
    public Vector3 offset; // Offset position from the player

    private Transform player; // Reference to the player's transform

    void Awake()
    {
        // Find the player GameObject by tag and get its transform
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found. Please ensure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        // Check if the player reference is assigned
        if (player != null)
        {
            // Calculate the new position with the offset
            Vector3 targetPosition = player.position + offset;

            // Smoothly move towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}