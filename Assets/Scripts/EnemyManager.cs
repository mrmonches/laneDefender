using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private float gameTimer, spawnTimer;

    private int gameRound, laneChoice, enemyChoice;

    private bool trackTime;

    [SerializeField] private GameObject[] Enemies = new GameObject[3];
    [SerializeField] private Transform[] Lanes = new Transform[5];

    private void Awake()
    {
        trackTime = true;

        gameRound = 1;
    }

    /// <summary>
    /// Allows the game to advance to the next "round"
    /// Resets the round timer
    /// </summary>
    private void AdvanceGameRound()
    {
        gameRound++;

        gameTimer = 0;
    }

    /// <summary>
    /// All of the logic behind round control
    /// </summary>
    private void RoundControl()
    {
        switch (gameRound)
        {
            case 1:
                if (spawnTimer >= 5)
                {
                    SpawnEnemy();
                }
                break;
            case 2:
                if (spawnTimer >= 4)
                {
                    SpawnEnemy();
                }
                break;
            case 3:
                if (spawnTimer >= 3.5)
                {
                    SpawnEnemy();
                }
                break;
            case 4:
                if (spawnTimer >= 3)
                {
                    SpawnEnemy();
                }
                break;
            case 5:
                if (spawnTimer >= 2.5)
                {
                    SpawnEnemy();
                }
                break;
            default:
                print("You're not supposed to get here.");
                break;
        }
    }

    /// <summary>
    /// Handles all of the random calculations to spawn an enemy
    /// </summary>
    private void SpawnEnemy()
    {
        spawnTimer = 0;

        RandomLane();

        RandomEnemy();

        Instantiate(Enemies[enemyChoice], Lanes[laneChoice].position, Quaternion.identity);
    }

    /// <summary>
    /// Generates a random lane to spawn on
    /// </summary>
    private void RandomLane()
    {
        // Really strange bug when Random.Range was set from 0-4
        // Literally would never spawn at 4, so expand to 5 and compensate

        laneChoice = Random.Range(0, 5);

        if (laneChoice == 5)
        {
            laneChoice = 4;
        }
    }

    /// <summary>
    /// Generates a random enemy to spawn
    /// </summary>
    private void RandomEnemy()
    {
        enemyChoice = Random.Range(0, 3);

        if (enemyChoice == 3)
        {
            enemyChoice = 2;
        }
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        RoundControl();

        if (trackTime)
        {
            gameTimer += Time.deltaTime;

            if (gameTimer >= 15)
            {
                AdvanceGameRound();
            }
        }

        if (trackTime && gameRound > 4)
        {
            trackTime = false;
        }
    }
}
