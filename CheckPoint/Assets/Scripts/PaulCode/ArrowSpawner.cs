using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform spawnPoint;   // Where arrows start
    public Transform endPoint;     // Where arrows are destroyed
    public float minSpeed = 5f;
    public float maxSpeed = 15f;
    public float minSpawnDelay = 0.5f;
    public float maxSpawnDelay = 2f;

    private float spawnTimer;

    private void Update()
    {
        // Countdown to next spawn
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnArrow();
            spawnTimer = Random.Range(minSpawnDelay, maxSpawnDelay); // Reset spawn timer
        }
    }

    private void SpawnArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, spawnPoint.position, Quaternion.identity);
        float speed = Random.Range(minSpeed, maxSpeed);
        arrow.GetComponent<Rigidbody2D>().velocity = (endPoint.position - spawnPoint.position).normalized * speed;
        Destroy(arrow, (endPoint.position - spawnPoint.position).magnitude / speed); // Destroy arrow when it reaches the endpoint
    }
}
