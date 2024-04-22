using System;
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

    private void Awake()
    {
        waitTime = new WaitForSeconds(spawnTime);
    }

    private void Start()
    {
        enemyCount = 0;
        spawner = GameController.instance.character.GetComponent<Spawner>();
        //enemyCount += spawner.SpawnLineTop(bull);
        //enemyCount += spawner.SpawnLineBottom(bull);
        //enemyCount += spawner.SpawnLineLeft(bull);
        //enemyCount += spawner.SpawnLineRight(bull);

        //enemyCount += spawner.SpawnLineSwarm(bull, true);
        //enemyCount += spawner.SpawnLineSwarm(bull, false);

        //enemyCount += spawner.SpawnRandomEnemiesTop(bull, 10, false);
        //enemyCount += spawner.SpawnRandomEnemiesRight(bull, 10, false);.

        //enemyCount += spawner.SpawnRandomEnemiesTop(bull, 2, true);

        //enemyCount += spawner.SpawnRandomEnemiesTop(kite, 2, true);
        //enemyCount += spawner.SpawnRandomEnemiesTop(kite, 2, false);

        //enemyCount += spawner.SpawnRandomEnemiesRight(bull, 20, true);
        StartCoroutine(SpawnBulls());
    }

    private IEnumerator SpawnBulls()
    {
        while(true) 
        {
            if(enemyCount < 100)
            {
                enemyCount += spawner.SpawnRandomEnemiesRight(slime, 10, true);
                enemyCount += spawner.SpawnRandomEnemiesRight(bull, 10, false);
                enemyCount += spawner.SpawnRandomEnemiesTop(kite, 3, true);
            }
            yield return waitTime;
        }
    }
}
