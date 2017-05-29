using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject asteroid;
    public GameObject flyingSaucer;

    private int score;
    private int hiscore;
    private int asteroidsRemaining;
    private int lives;
    private int wave;
    private int increaseEachWave = 4;
    private float enemySpawnRate = 30.0f;

    public Text scoreText;
    public Text livesText;
    public Text waveText;
    public Text hiscoreText;

    private IEnumerator EnemySpawner;

    // Use this for initialization
    void Start()
    {
        EnemySpawner = FlyingSaucerSpawner();

        hiscore = PlayerPrefs.GetInt("hiscore", 0);

        BeginGame();
    }

    // Update is called once per frame
    void Update()
    {
        // Quit if player presses escape
        // Quit game
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("Scene/Menu");
        }
    }

    void BeginGame()
    {

        score = 0;
        lives = 3;
        wave = 1;

        // Prepare the HUD
        scoreText.text = "SCORE " + score;
        hiscoreText.text = "HISCORE: " + hiscore;
        livesText.text = "x" + lives;
        waveText.text = "WAVE " + wave;

        SpawnAsteroids();

        DestoryExistingEnemies();
        StartCoroutine(EnemySpawner);
    }

    void SpawnAsteroids()
    {

        DestroyExistingAsteroids();

        // Decide how many asteroids to spawn
        // If any asteroids left over from previous game, subtract them
        asteroidsRemaining = (wave * increaseEachWave);

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("AsteroidSpawnPoint");
        List<int> usedSpawnPositions = new List<int>();

        for (int i = 0; i < asteroidsRemaining; i++)
        {
            //Find an avaliable spawn point
            int RandomIndex = (int)Random.Range(0.0f, spawnPoints.Length - 0.01f);

            while (usedSpawnPositions.IndexOf(RandomIndex) != -1 && usedSpawnPositions.Count < spawnPoints.Length)
            {
                RandomIndex++;
                if (RandomIndex >= spawnPoints.Length)
                {
                    RandomIndex = 0;
                }
            }

            // Use a spawn point 
            if (usedSpawnPositions.Count < spawnPoints.Length)
            {
                Transform spawnPoint = spawnPoints[RandomIndex].transform;
                // Spawn an asteroid
                Instantiate(asteroid,
                    spawnPoint.position,
                    spawnPoint.rotation);

                usedSpawnPositions.Add(RandomIndex);
            }
            // If there are no more spawn points to use
            else
            {
                Debug.Log("Random Asteroid Added");
                // Spawn an asteroid
                Instantiate(asteroid,
                    new Vector3(Random.Range(-9.0f, 9.0f),
                        Random.Range(-6.0f, 6.0f), 0),
                    Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)));
            }
        }

        waveText.text = "WAVE " + wave;
    }

    IEnumerator FlyingSaucerSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemySpawnRate / (float)wave);
            SpawnFlyingSaucer();
        }
    }

    void SpawnFlyingSaucer()
    {
        // Spawn enemy and determine the postion and velocity the enemy spawns
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
        Transform spawnTransform = spawnPoints[(int)Random.Range(0, spawnPoints.Length - 0.01f)].transform;
        GameObject enemy = Instantiate(flyingSaucer, spawnTransform.position, new Quaternion());

        enemy.GetComponent<Rigidbody2D>().velocity = spawnTransform.up * enemy.GetComponent<EnemyController>().speed;
    }

    public void IncrementScore(int addedScore = 1)
    {
        score += addedScore;

        scoreText.text = "SCORE " + score;

        if (score > hiscore)
        {
            hiscore = score;
            hiscoreText.text = "HISCORE: " + hiscore;

            // Save the new hiscore
            PlayerPrefs.SetInt("hiscore", hiscore);
        }

        // Has player destroyed all asteroids?
        if (asteroidsRemaining < 1)
        {

            // Start next wave
            wave++;
            SpawnAsteroids();

        }
    }

    public void DecrementLives()
    {
        lives--;
        livesText.text = "x" + lives;

        // Has player run out of lives?
        if (lives < 1)
        {
            // Restart the game
            BeginGame();
        }
    }

    public void DecrementAsteroids()
    {
        asteroidsRemaining--;
    }

    public void SplitAsteroid()
    {
        // Two extra asteroids
        // - big one
        // + 3 little ones
        // = 2
        asteroidsRemaining += 2;

    }

    void DestroyExistingAsteroids()
    {
        GameObject[] asteroids =
            GameObject.FindGameObjectsWithTag("Large Asteroid");

        foreach (GameObject current in asteroids)
        {
            GameObject.Destroy(current);
        }

        GameObject[] asteroids2 =
            GameObject.FindGameObjectsWithTag("Small Asteroid");

        foreach (GameObject current in asteroids2)
        {
            GameObject.Destroy(current);
        }
    }

    void DestoryExistingEnemies()
    {
        GameObject[] enemies =
           GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject current in enemies)
        {
            GameObject.Destroy(current);
        }

        //Reset the coroutine
        StopCoroutine(EnemySpawner);
        EnemySpawner = FlyingSaucerSpawner();
    }

    public void PlayerDeath()
    {
        StartCoroutine(PlayerDeathCoroutine());
    }

    IEnumerator PlayerDeathCoroutine()
    {
        player.SetActive(false);

        ShipController playerController = player.GetComponent<ShipController>();
        Debug.Assert(playerController);
        // Wait to respawn
        yield return new WaitForSeconds(playerController.respawnTimer);

        player.SetActive(true);

        if (lives > 1)
        {
            playerController.RespawnPlayer();
        }
        else
        {
            playerController.ResetPlayer();
        }
    }
}