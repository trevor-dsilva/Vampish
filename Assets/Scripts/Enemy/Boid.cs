using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float maxForce = 0.5f;
    public float neighborRadius = 3f;
    public float separationRadius = 1.5f;
    public float alignmentWeight = 1f;
    public float cohesionWeight = 1f;
    public float separationWeight = 1.5f;

    private Vector3 velocity;
    private Vector3 acceleration;

    private void Start()
    {
        velocity = new Vector3(Random.insideUnitSphere.x, 0, Random.insideUnitSphere.z) * maxSpeed;
    }

    private void Update()
    {
        List<Boid> neighbors = GetNeighbors();

        Vector3 alignment = Align(neighbors) * alignmentWeight;
        Vector3 cohesion = Cohere(neighbors) * cohesionWeight;
        Vector3 separation = Separate(neighbors) * separationWeight;

        Vector3 playerFollow = FollowPlayer() * 3f; // Follow player with higher weight

        acceleration = alignment + cohesion + separation + playerFollow;

        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // Limit movement to the X and Z plane
        velocity.y = 0;
        transform.position += velocity * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        // Ensure the boid is facing the direction it is moving
        if (velocity != Vector3.zero)
        {
            transform.forward = velocity.normalized;
        }
    }

    private List<Boid> GetNeighbors()
    {
        List<Boid> neighbors = new List<Boid>();
        Boid[] allBoids = GameObject.FindObjectsByType<Boid>(FindObjectsSortMode.None);

        foreach (Boid boid in allBoids)
        {
            if (boid != this && Vector3.Distance(transform.position, boid.transform.position) < neighborRadius)
            {
                neighbors.Add(boid);
            }
        }

        return neighbors;
    }

    private Vector3 Align(List<Boid> neighbors)
    {
        Vector3 sum = Vector3.zero;
        foreach (Boid neighbor in neighbors)
        {
            sum += neighbor.velocity;
        }
        if (neighbors.Count > 0)
        {
            sum /= neighbors.Count;
            sum = Vector3.ClampMagnitude(sum, maxForce);
        }
        return sum;
    }

    private Vector3 Cohere(List<Boid> neighbors)
    {
        Vector3 sum = Vector3.zero;
        foreach (Boid neighbor in neighbors)
        {
            sum += neighbor.transform.position;
        }
        if (neighbors.Count > 0)
        {
            sum /= neighbors.Count;
            Vector3 desired = sum - transform.position;
            desired = Vector3.ClampMagnitude(desired, maxForce);
            return desired;
        }
        return sum;
    }

    private Vector3 Separate(List<Boid> neighbors)
    {
        Vector3 sum = Vector3.zero;
        foreach (Boid neighbor in neighbors)
        {
            Vector3 diff = transform.position - neighbor.transform.position;
            diff /= diff.magnitude;
            sum += diff;
        }
        if (neighbors.Count > 0)
        {
            sum /= neighbors.Count;
            sum = Vector3.ClampMagnitude(sum, maxForce);
        }
        return sum;
    }

    private Vector3 FollowPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector3 desired = player.transform.position - transform.position;
            desired = Vector3.ClampMagnitude(desired, maxForce);
            desired.y = 0; // Limit to X and Z plane
            return desired;
        }
        return Vector3.zero;
    }
}