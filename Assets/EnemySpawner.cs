using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnChance;

    public void SpawnEnemy(int amount)
    {

        float range = Random.Range(0, 100);


        Instantiate(enemyPrefab);
    }
    
}
