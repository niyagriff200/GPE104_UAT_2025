using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton instance for global access
    public static GameManager instance;

    [Header("Game States")]
    public GameObject gameplayState;
    public GameObject mainMenuState;
    public GameObject gameOverState;
    public GameObject creditsState;
    public GameObject controlsState;
    public GameObject splashScreenState;
    public GameObject settingsScreenState;

    [Header("Audio Clips")]
    public AudioClip backgroundMenuMusic;
    public AudioClip backgroundGameplayMusic;
    public AudioClip shootSound;
    public AudioClip ufoSound;
    public AudioClip deathSound;
    public AudioClip playerFinalDeathSound;
    public AudioClip damageRockSound;
    public AudioClip damageMetalSound;
    public AudioClip healthPickupSound;


    [Header("Player Setup")]
    public List<PlayerController> players;
    public GameObject playerPawnPrefab;
    public GameObject playerControllerPrefab;
    public int startingLives;
    //Needed to render lifeIcon
    public GameplayUI gameplayUI;

    [Header("Player Movement Settings")]
    public float playerMoveSpeed;
    public float playerTurboSpeed;
    public float playerRotateSpeed;
    public float playerTeleportDistance;
    public float playerRandomTeleportDistance;

    [Header("Projectile Settings")]
    public GameObject projectile;
    public float projectileSpeed;
    public float projectileLifetime;
    public float projectileDamage;

    [Header("Enemy Prefabs")]
    public GameObject bigMeteorPrefab;
    public GameObject mediumMeteorPrefab;
    public GameObject smallMeteorPrefab;
    public GameObject ufoPrefab;

    [Header("Enemy Spawning Settings")]
    public List<Transform> enemySpawnPoints;
    public List<GameObject> activeEnemies = new List<GameObject>();
    public int enemyCount;
    [Range(0f, 1f)] public float ufoChance;
    public float enemySpawnInterval;
    private float enemySpawnTimer;
    [HideInInspector] public int initialEnemiesSpawned = 0;

    [Header("Enemy Movement Settings")]
    public float ufoMoveSpeed;
    public float meteorMoveSpeed;

    [Header("Damage Settings")]
    public float baseMeteorDamage;
    [Range(0f, 1f)] public float mediumMeteorMultiplier;
    [Range(0f, 1f)] public float smallMeteorMultiplier;
    public float ufoDamage;

    [Header("Health Settings")]
    public float playerMaxHealth;
    public float ufoMaxHealth;
    public float meteorMaxHealth;

    [Header("Heal Pickup Settings")]
    public GameObject healPickupPrefab;
    public float healAmount;
    public float healSpawnInterval;
    private float healSpawnTimer;
    private List<GameObject> activeHealPickups = new List<GameObject>();

    [Header("Meteor Split Settings")]
    public int bigMeteorSplitCount;
    public int mediumMeteorSplitCount;

    [Header("Screen Wrap Settings")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    [Header("Score Values")]
    public float ufoScore;
    public float bigMeteorScore;
    public float mediumMeteorScore;
    public float smallMeteorScore;

    [Header("Score Tracking")]
    public float score = 0f;
    public float topScore = 0f;

    [Header("Camera Shake Settings")]
    public float shakeDuration;
    public float shakeIntensity;
    public CameraShake cameraShake;

    [Header("Damage Flash Settings")]
    public Color flashColor;
    public Color normalColor;
    public float flashDuration = 0.5f;

    [Header("Knock Back Settings")]
    public float knockbackDamage;





    // Enforce singleton pattern
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        ShowSplashScreen();
    }

    private void Start()
    {

        topScore = PlayerPrefs.GetFloat("TopScore", 0f);
        PlayBackgroundMusic musicManager = Object.FindFirstObjectByType<PlayBackgroundMusic>();
        musicManager.PlayMenuMusic();
    }

    private void Update()
    {
        if (gameplayState.activeInHierarchy)
        {
            StartGameplay();
        }

        if (players.Count > 0 && players[0].pawn == null)
        {
            ShowGameOver();
        }
    }

    // Randomly choose between UFO and big meteor
    public GameObject GetRandomEnemy()
    {
        if (Random.Range(0f, 1f) < ufoChance)
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
        Vector3 spawnPoint = GetRandomSpawnPoint();

        if (enemyToSpawn != null)
        {
            GameObject newEnemy = Instantiate(enemyToSpawn, spawnPoint, Quaternion.identity);
            activeEnemies.Add(newEnemy);
            initialEnemiesSpawned++;
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
            int splitCount = size == 3 ? bigMeteorSplitCount : mediumMeteorSplitCount;

            for (int i = 0; i < splitCount; i++)
            {
                Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);
                GameObject newMeteor = Instantiate(meteorPrefab, position + offset, Quaternion.identity);

                MeteorHealth meteorHealth = newMeteor.GetComponent<MeteorHealth>();
                if (meteorHealth != null) meteorHealth.SetSize(newSize);

                activeEnemies.Add(newMeteor);
            }
        }
    }

    // Remove enemy from tracking list
    public void RemoveEnemy(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy)) activeEnemies.Remove(enemy);

        // Check for victory condition
        if (activeEnemies.Count == 0 && initialEnemiesSpawned >= enemyCount)
        {
            ShowGameOver();
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
            controller.pawn = pawn;
        }
    }

    public void ApplyScreenWrap(GameObject obj)
    {
        Vector3 pos = obj.transform.position;

        if (pos.x > maxX) pos.x = minX;
        else if (pos.x < minX) pos.x = maxX;

        if (pos.y > maxY) pos.y = minY;
        else if (pos.y < minY) pos.y = maxY;

        obj.transform.position = pos;
    }

    public void AddScore(float amount)
    {
        score += amount;

        if (score > topScore)
        {
            topScore = score;
            PlayerPrefs.SetFloat("TopScore", topScore);
            PlayerPrefs.Save();
        }
    }

    public void ShowSplashScreen()
    {
        splashScreenState.SetActive(true);
        mainMenuState.SetActive(false);
        gameplayState.SetActive(false);
        gameOverState.SetActive(false);
        creditsState.SetActive(false);
        controlsState.SetActive(false);
        settingsScreenState.SetActive(false);
    }

    public void ShowMainMenu()
    {
        splashScreenState.SetActive(false);
        mainMenuState.SetActive(true);
        gameplayState.SetActive(false);
        gameOverState.SetActive(false);
        creditsState.SetActive(false);
        controlsState.SetActive(false);
        settingsScreenState.SetActive(false);

    }

    public void ShowGameplay()
    {
        splashScreenState.SetActive(false);
        mainMenuState.SetActive(false);
        gameplayState.SetActive(true);
        gameOverState.SetActive(false);
        creditsState.SetActive(false);
        controlsState.SetActive(false);
        settingsScreenState.SetActive(false);

        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            AudioListener listener = mainCam.GetComponent<AudioListener>();
            if (listener != null)
            {
                listener.enabled = false;
            }
        }


        gameplayUI.InitializeLives(startingLives);
        gameplayUI.UpdateLives(startingLives);

        players = new List<PlayerController>();
        enemySpawnTimer = 0f;
        healSpawnTimer = 0f;
        score = 0f;
        initialEnemiesSpawned = 0;

        AudioListener playerListener = playerPawnPrefab.GetComponent<AudioListener>();
        if (playerListener != null)
        {
            playerListener.enabled = true;
        }

        PlayBackgroundMusic musicManager = Object.FindFirstObjectByType<PlayBackgroundMusic>();
        musicManager.PlayGameplayMusic();



        activeEnemies.Clear();
        SpawnPlayer();


    }

    public void ShowGameOver()
    {
        splashScreenState.SetActive(false);
        mainMenuState.SetActive(false);
        gameplayState.SetActive(false);
        gameOverState.SetActive(true);
        creditsState.SetActive(false);
        controlsState.SetActive(false);
        settingsScreenState.SetActive(false);

        // Destroy enemies
        foreach (GameObject enemy in activeEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        activeEnemies.Clear();

        // Destroy players (controller + pawn)
        foreach (PlayerController controller in players)
        {
            if (controller != null)
            {
                // Destroy the pawn GameObject if present
                if (controller.pawn != null)
                {
                    Destroy(controller.pawn.gameObject);
                }

                // Destroy the controller object
                Destroy(controller.gameObject);
            }
        }
        players.Clear();

        // Destroy heal pickups that were tracked
        foreach (GameObject pickup in activeHealPickups)
        {
            if (pickup != null)
            {
                Destroy(pickup);
            }
        }
        activeHealPickups.Clear();

        GameOverUI gameOverUI = gameOverState.GetComponentInChildren<GameOverUI>();
        if (gameOverUI != null)
        {
            gameOverUI.ShowResults();
        }

        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            AudioListener listener = mainCam.GetComponent<AudioListener>();
            if (listener != null)
            {
                listener.enabled = true;
            }
        }

        PlayBackgroundMusic musicManager = Object.FindFirstObjectByType<PlayBackgroundMusic>();
        musicManager.PlayMenuMusic();
    }


    public void ShowCreditsScreen()
    {
        splashScreenState.SetActive(false);
        mainMenuState.SetActive(false);
        gameplayState.SetActive(false);
        gameOverState.SetActive(false);
        creditsState.SetActive(true);
        controlsState.SetActive(false);
        settingsScreenState.SetActive(false);
    }

    public void ShowControlsScreen()
    {
        splashScreenState.SetActive(false);
        mainMenuState.SetActive(false);
        gameplayState.SetActive(false);
        gameOverState.SetActive(false);
        creditsState.SetActive(false);
        controlsState.SetActive(true);
        settingsScreenState.SetActive(false);
    }

    public void ShowSettingsScreen()
    {
        splashScreenState.SetActive(false);
        mainMenuState.SetActive(false);
        gameplayState.SetActive(false);
        gameOverState.SetActive(false);
        creditsState.SetActive(false);
        controlsState.SetActive(false);
        settingsScreenState.SetActive(true);
    }

    public void StartGameplay()
    {
        if (!IsPlayerAlive())
        {
            return;
        }
        // Meteor spawning
        enemySpawnTimer += Time.deltaTime;
        if (enemySpawnTimer >= enemySpawnInterval && initialEnemiesSpawned < enemyCount)
        {
            SpawnEnemy();
            enemySpawnTimer = 0f;
        }

        // Heal pickup spawning
        healSpawnTimer += Time.deltaTime;
        if (healSpawnTimer >= healSpawnInterval)
        {
            SpawnHealPickup();
            healSpawnTimer = 0f;
        }
    }

    public bool IsPlayerAlive()
    {
        return players.Count > 0 && players[0].pawn != null;
    }


    public void SpawnHealPickup()
    {
        if (players.Count == 0 || players[0].pawn == null)
        {
            return;
        }

        Vector3 playerPos = players[0].pawn.transform.position;
        Vector3 offset = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);
        Vector3 spawnPos = playerPos + offset;

        GameObject pickup = Instantiate(healPickupPrefab, spawnPos, Quaternion.identity);
        activeHealPickups.Add(pickup);
    }



    public void QuitGame()
    {
        Application.Quit();
    }

}
