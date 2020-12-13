using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject groundPlane;
    public float minSecondsBetweenSpawns = 0.5f, maxSecondsBetweenSpawns = 3;
    public float minSpawnDistance = 40, maxSpawnDistance = 80;
    public float minObstacleWidth = 10, maxObstacleWidth = 20;
    public float minObstacleHeight = 40, maxObstacleHeight = 90;
    public float epsilon = 200;
    public float playerViewRadiusH = 400;
    public float playerViewRadiusV = 1000;

    float spawnZ;
    /// spawnXMin is right of the player
    float spawnXMin, spawnXMax;

    float timeOfLastWave;

    bool shouldSpawn = true;
    Player player;

    void Start()
    {
        // Set limits for where obstacles can be spawned
        float worldWidth = groundPlane.transform.localScale.x;
        float worldHeight = groundPlane.transform.localScale.z;
        Vector3 worldLocation = groundPlane.transform.position;

        spawnZ = worldLocation.z - worldHeight * 10 / 2f;
        spawnXMin = worldLocation.x - worldWidth * 10 / 2f;
        spawnXMax = worldLocation.x + worldWidth * 10 / 2f;

        // Get player reference
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.OnPlayerDeath += StopSpawning;

        // At the start of the game, spawn some obstacles first
        StartCoroutine(SpawnInitialBlocks());
    }

    void Update()
    {
        if (!shouldSpawn) return;

        float currentSecondsBetweenSpawns = Mathf.Lerp(maxSecondsBetweenSpawns, minSecondsBetweenSpawns, Difficulty.GetDifficultyPercent());

        if (Time.timeSinceLevelLoad >= timeOfLastWave + currentSecondsBetweenSpawns)
        {
            timeOfLastWave = Time.timeSinceLevelLoad;

            SpawnWave(Camera.main.transform.position.z - playerViewRadiusV);
        }
    }

    void SpawnWave(float zLocation)
    {
        for (
            float currentX = Mathf.Max(spawnXMin, Camera.main.transform.position.x - playerViewRadiusH);
            currentX < Mathf.Min(spawnXMax, Camera.main.transform.position.x + playerViewRadiusH);
            currentX += Random.Range(minSpawnDistance, maxSpawnDistance))
        {
            GameObject newObstacle = Instantiate(
                obstaclePrefab, new Vector3(
                    currentX,
                    0,
                    zLocation + Random.Range(-minSpawnDistance, minSpawnDistance)
                ),
                Quaternion.Euler(0, Random.Range(0f, 360f), 0)
            );
            newObstacle.transform.localScale = new Vector3(
                Random.Range(minObstacleWidth, maxObstacleWidth),
                Random.Range(minObstacleHeight, maxObstacleHeight),
                Random.Range(minObstacleWidth, maxObstacleWidth)
            );
        }
    }

    IEnumerator SpawnInitialBlocks()
    {
        for (
                float currentZ = Camera.main.transform.position.y - epsilon;
                currentZ >= Mathf.Max(spawnZ, Camera.main.transform.position.z - playerViewRadiusV);
                currentZ -= Random.Range(minSpawnDistance, maxSpawnDistance))
        {
            SpawnWave(currentZ);
            yield return null;
        }
    }

    void StopSpawning()
    {
        shouldSpawn = false;
    }
}
