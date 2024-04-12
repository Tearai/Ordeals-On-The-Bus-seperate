using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HoleSpawner : MonoBehaviour
{
    public GameObject holePrefab; // Prefab for the hole
    public Vector2 spawnIntervalRange = new Vector2(1f, 3f); // Range for spawn intervals
    public Vector2 spawnPositionRange = new Vector2(-5f, 5f); // Range for spawn positions

    private float nextSpawnTime; // Time for next hole spawn

    void Start()
    {
        // Schedule the first hole spawn
        nextSpawnTime = Time.time + Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
    }

    void Update()
    {
        // Check if it's time to spawn a hole
        if (Time.time >= nextSpawnTime)
        {
            SpawnHole();
            // Schedule the next hole spawn
            nextSpawnTime = Time.time + Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
        }
    }

    void SpawnHole()
    {
        // Generate random position for the hole
        float spawnX = Random.Range(spawnPositionRange.x, spawnPositionRange.y);
        float spawnY = transform.position.y; // Y position of the wall
        float spawnZ = transform.position.z; // Z position of the wall

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

        // Spawn the hole at the generated position
        Instantiate(holePrefab, spawnPosition, Quaternion.identity);
    }
}