using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] platformPrefabs; // Array of platform prefabs
    public Transform spawnPoint;         // The top point where platforms start
    public Transform endPoint;           // The bottom point where platforms are destroyed
    public float minSpeed = 1f;          // Minimum speed of the platforms
    public float maxSpeed = 5f;          // Maximum speed of the platforms
    public float minSpawnDelay = 1f;     // Minimum delay between spawns
    public float maxSpawnDelay = 3f;     // Maximum delay between spawns

    private float spawnTimer;            // Timer to keep track of spawning

    void Update()
    {
        // Countdown to next spawn
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnPlatform();
            spawnTimer = Random.Range(minSpawnDelay, maxSpawnDelay); // Reset spawn timer
        }
    }

    void SpawnPlatform()
    {
        if (platformPrefabs.Length == 0) return; // Check if there are any prefabs assigned

        // Select a random platform prefab
        int index = Random.Range(0, platformPrefabs.Length);
        GameObject selectedPrefab = platformPrefabs[index];

        // Instantiate the selected platform prefab at the spawn point
        GameObject platform = Instantiate(selectedPrefab, spawnPoint.position, Quaternion.identity);

        // Calculate speed and set it
        float speed = Random.Range(minSpeed, maxSpeed);
        Rigidbody2D rb = platform.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(0, -(speed)); // Move down
        }

        // Calculate lifetime based on distance to travel and speed
        float lifeTime = (endPoint.position.y - spawnPoint.position.y) / speed;
        Destroy(platform, Mathf.Abs(lifeTime)); // Ensure the time is positive
    }
}
