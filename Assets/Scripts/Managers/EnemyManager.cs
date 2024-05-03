using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Spawner spawner;
    [SerializeField] private GameObject bull;
    [SerializeField] private GameObject kite;
    [SerializeField] private GameObject slime;
    [SerializeField] private float spawnTime;
    private WaitForSeconds waitTime;

    public int enemyCount;

    private int maxEnemies = 2;
    private int spawnersCount = 0;

    public bool startedSpawningBull = false;
    public bool startedSpawningSlime = false;
    public bool startedSpawningKite = false;

    public bool started5Seconds = false;


    private void Awake()
    {
        waitTime = new WaitForSeconds(spawnTime);
    }

    private void Start()
    {
        enemyCount = 0;
        spawner = GameController.instance.character.GetComponent<Spawner>();
    }

    private void Update()
    {
        if(GameController.instance.timeManager.beginPlay)
        {
            if (!started5Seconds)
            {
                started5Seconds = true;
                StartCoroutine(Every4Seconds());
            }
            if (!startedSpawningBull)
            {
                startedSpawningBull = true;
                StartCoroutine(SpawnBulls());
                spawnersCount++;
            }
            if (GameController.instance.timeManager.timeLeft7 && !startedSpawningSlime)
            {
                maxEnemies += 5;
                startedSpawningSlime = true;
                StartCoroutine(SpawnSlimes());
                spawnersCount++;
            }

            if (GameController.instance.timeManager.timeLeft3 && !startedSpawningKite)
            {
                maxEnemies += 5;
                startedSpawningKite = true;
                StartCoroutine(SpawnKites());
                spawnersCount++;
            }
        }
        
    }

    private IEnumerator Every4Seconds()
    {
        while (true)
        {
            maxEnemies++;
            yield return new WaitForSeconds(4f);
        }
    }

    private IEnumerator SpawnBulls()
    {
        while(true) 
        {
            if(spawnersCount == 1)
            {
                if (enemyCount < maxEnemies)
                {
                    SpawnBullsRandomly(maxEnemies - enemyCount);
                }
            }

            if(spawnersCount == 2)
            {
                if (enemyCount < maxEnemies)
                {
                    SpawnBullsRandomly(Mathf.RoundToInt((maxEnemies - enemyCount) * 0.7f));
                }
            }

            if (spawnersCount == 3)
            {
                if (enemyCount < maxEnemies)
                {
                    SpawnBullsRandomly(Mathf.RoundToInt((maxEnemies - enemyCount) * 0.7f));
                }
            }
            yield return waitTime;
        }
    }

    private void SpawnBullsRandomly(int amount)
    {
        if (Random.value <= 0.95f)
        {
            int random = Random.Range(0, 4);
            if (random == 0)
            {
                enemyCount += spawner.SpawnRandomEnemiesRight(bull, amount, false);
            }
            else if (random == 1)
            {
                enemyCount += spawner.SpawnRandomEnemiesRight(bull, amount, true);
            }
            else if (random == 2)
            {
                enemyCount += spawner.SpawnRandomEnemiesTop(bull, amount, false);
            }
            else
            {
                enemyCount += spawner.SpawnRandomEnemiesTop(bull, amount, true);
            }
        }
        else
        {
            int random = Random.Range(0, 4);
            if (random == 0)
            {
                enemyCount += spawner.SpawnLineBottom(bull);
            }
            else if (random == 1)
            {
                enemyCount += spawner.SpawnLineTop(bull);
            }
            else if (random == 2)
            {
                enemyCount += spawner.SpawnLineLeft(bull);
            }
            else
            {
                enemyCount += spawner.SpawnLineRight(bull);
            }

        }
        if (Random.value <= 0.05f)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                enemyCount += spawner.SpawnLineSwarm(bull, true);
            }
            else
            {
                enemyCount += spawner.SpawnLineSwarm(bull, false);
            }

        }
    }

    private IEnumerator SpawnKites()
    {
        while (true)
        {
            if (spawnersCount == 3)
            {
                if (enemyCount < maxEnemies)
                {
                    SpawnKitesRandomly(Mathf.RoundToInt((maxEnemies - enemyCount) * 0.08f));
                }
            }
            
            yield return waitTime;
        }
    }

    private void SpawnKitesRandomly(int amount)
    {
        int random = Random.Range(0, 4);
        if (random == 0)
        {
            enemyCount += spawner.SpawnRandomEnemiesRight(kite, amount, false);
        }
        else if (random == 1)
        {
            enemyCount += spawner.SpawnRandomEnemiesRight(kite, amount, true);
        }
        else if (random == 2)
        {
            enemyCount += spawner.SpawnRandomEnemiesTop(kite, amount, false);
        }
        else
        {
            enemyCount += spawner.SpawnRandomEnemiesTop(kite, amount, true);
        }
    }

    private IEnumerator SpawnSlimes()
    {
        while (true)
        {
            if (spawnersCount == 2)
            {
                if (enemyCount < maxEnemies)
                {
                    SpawnSlimesRandomly(Mathf.RoundToInt((maxEnemies - enemyCount) * 0.2f));
                }
            }
            if (spawnersCount == 3)
            {
                if (enemyCount < maxEnemies)
                {
                    SpawnSlimesRandomly(Mathf.RoundToInt((maxEnemies - enemyCount) * 0.2f));
                }
            }
            yield return waitTime;
        }
    }

    private void SpawnSlimesRandomly(int amount)
    {
        int random = Random.Range(0, 4);
        if (random == 0)
        {
            enemyCount += spawner.SpawnRandomEnemiesRight(slime, amount, false);
        }
        else if (random == 1)
        {
            enemyCount += spawner.SpawnRandomEnemiesRight(slime, amount, true);
        }
        else if (random == 2)
        {
            enemyCount += spawner.SpawnRandomEnemiesTop(slime, amount, false);
        }
        else
        {
            enemyCount += spawner.SpawnRandomEnemiesTop(slime, amount, true);
        }
    }
}
