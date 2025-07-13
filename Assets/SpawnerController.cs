using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{

    private float timer = 0f;
    [SerializeField] private  float gap = 5f;
    [SerializeField] EnemySpawner[] enemySpawners; 


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= gap)
        {
            //Debug.Log("Passaram 5 segundos!");
            SpawnEnemies();
            timer = 0f;
        }
    }

    void SpawnEnemies() {
        foreach (EnemySpawner enemySpawner in enemySpawners) {
            enemySpawner.SpawnEnemy(Random.Range(1, 4));
        }
    }
}
