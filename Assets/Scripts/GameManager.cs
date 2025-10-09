using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance for global access
    public static GameManager instance;

    [Header("Game States")]
    public GameObject gameplayState;     // Active during gameplay
    public GameObject mainMenuState;     // Main menu UI
    public GameObject gameOverState;     // Game over screen
    public GameObject creditsState;      // Credits screen
    public GameObject controlsState;     // Controls/help screen

    [Header("Players")]
    public List<PlayerController> players;      // List of active player controllers
    public GameObject playerPawnPrefab;         // Player visual/pawn prefab
    public GameObject playerControllerPrefab;   // Player logic/controller prefab

    [Header("Player Movement Settings")]
    public float playerMoveSpeed;               // Normal movement speed
    public float playerTurboSpeed;              // Boosted movement speed
    public float playerRotateSpeed;             // Rotation speed
    public float playerTeleportDistance;        // Fixed teleport distance
    public float playerRandomTeleportDistance;  // Randomized teleport offset

    [Header("Projectile Settings")]
    public GameObject projectile;               //Projectile prefab
    public float projectileSpeed;               // Speed of player projectiles
    public float projectileLifetime;            // How long projectiles exist
    public float projectileDamage;              // Damage dealt by projectiles

    [Header("Enemies")]
    public GameObject bigMeteorPrefab;          // Large meteor prefab
    public GameObject mediumMeteorPrefab;       // Medium meteor prefab
    public GameObject smallMeteorPrefab;        // Small meteor prefab
    public GameObject ufoPrefab;                // UFO prefab

    [Header("Enemy Movement Settings")]
    public float ufoMoveSpeed;                  // UFO movement speed
    public float meteorMoveSpeed;               // Meteor movement speed

    [Header("Enemy Spawner")]
    public List<Transform> enemySpawnPoints;    // Spawn locations for enemies
    public List<GameObject> activeEnemies;      // Currently active enemies
    public int enemyCount;                      // Max number of enemies allowed
    [Range(0f, 1.0f)] public float ufoChance;   // Chance to spawn a UFO
    public float enemySpawnInterval;            // Time between spawns
    private float enemySpawnTimer;              // Tracks time since last spawn

    [Header("Meteor Split Settings")]
    public int bigMeteorSplitCount;             // How many mediums spawn from big
    public int mediumMeteorSplitCount;          // How many smalls spawn from medium

    [Header("Health Settings")]
    public float playerMaxHealth;               // Player health value
    public float ufoMaxHealth;                  // UFO health value
    public float meteorMaxHealth;               // Meteor health value

    private void Awake()
    {
        // Enforce singleton pattern
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        SpawnPlayer();   // Spawn player at game start
        SpawnEnemy();    // Spawn initial enemy
    }

    private void Update()
    {
        // Only spawn enemies during gameplay
        if (gameplayState.activeInHierarchy)
        {
            enemySpawnTimer += Time.deltaTime;

            // Spawn enemy if timer exceeds interval and count is below limit
            if (enemySpawnTimer >= enemySpawnInterval && activeEnemies.Count < enemyCount)
            {
                SpawnEnemy();
                enemySpawnTimer = 0f;
            }
        }
    }

    // Randomly choose between UFO and big meteor
    public GameObject GetRandomEnemy()
    {
        if (Random.Range(0f, 1.0f) < ufoChance)
        {
            return ufoPrefab;
        }
        else
        {
            return bigMeteorPrefab;
        }
    }

    // Pick a random spawn point from the list
    public Vector3 GetRandomSpawnPoint()
    {
        return enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)].position;
    }

    // Spawn a new enemy at a random location
    public void SpawnEnemy()
    {
        GameObject enemyToSpawn = GetRandomEnemy();
        Vector3 enemySpawnPoint = GetRandomSpawnPoint();

        if (enemyToSpawn != null)
        {
            GameObject newEnemy = Instantiate(enemyToSpawn, enemySpawnPoint, Quaternion.identity);
            activeEnemies.Add(newEnemy); // Track spawned enemy
        }
    }

    // Return meteor prefab based on size
    public GameObject GetMeteorSize(int size)
    {
        switch (size)
        {
            case 3: return bigMeteorPrefab;
            case 2: return mediumMeteorPrefab;
            case 1: return smallMeteorPrefab;
            default: return null;
        }
    }

    // Split meteor into smaller ones based on size
    public void SplitMeteors(Vector3 position, int size)
    {
        int newSize = size - 1;
        GameObject meteorPrefab = GetMeteorSize(newSize);

        if (meteorPrefab != null)
        {
            int splitCount = 0;

            if (size == 3)
            {
                splitCount = bigMeteorSplitCount;
            }
            else if (size == 2)
            {
                splitCount = mediumMeteorSplitCount;
            }

            for (int i = 0; i < splitCount; i++)
            {
                // Slight random offset for each split
                float xOffset = Random.Range(-0.5f, 0.5f);
                float yOffset = Random.Range(-0.5f, 0.5f);
                Vector3 offset = new Vector3(xOffset, yOffset, 0f);

                GameObject newMeteor = Instantiate(meteorPrefab, position + offset, Quaternion.identity);
                activeEnemies.Add(newMeteor); // Track new meteor
            }
        }
    }

    // Remove enemy from tracking list
    public void RemoveEnemy(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
        }
    }

    // Spawn player controller and pawn, then link them
    public void SpawnPlayer()
    {
        GameObject controllerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        PlayerController controller = controllerObj.GetComponent<PlayerController>();
        players.Add(controller);

        GameObject pawnObj = Instantiate(playerPawnPrefab, Vector3.zero, Quaternion.identity);
        Pawn pawn = pawnObj.GetComponent<Pawn>();

        if (pawn != null)
        {
            controller.pawn = pawn; // Link pawn to controller
        }
    }
}
