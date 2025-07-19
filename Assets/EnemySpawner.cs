using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnChance = 100f; // 0 a 100
    [SerializeField] private Vector2 spawnOffsetRange = new Vector2(1f, 1f); // deslocamento aleatório opcional
    [SerializeField] private Transform enemiesParent;
    private float timer = 0f;
    [SerializeField] private float gap = 5f;
    [SerializeField] private int maxEnemies = 50;
    private int count = 0;

    [Header("Debug")]
    [SerializeField] private Color gizmoColor = Color.blue;
    [SerializeField] private float gizmoRadius = 0.3f;


    private List<Transform> spawnPoints = new List<Transform>();

    private void Start()
    {
        if (enemyPrefab == null) return;
        spawnPoints.Clear();
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t != transform)
                spawnPoints.Add(t);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= gap)
        {
            //Debug.Log("Passaram 5 segundos!");
            SpawnEnemies();
            timer = 0f;
        }
    }

    public void SpawnEnemy(int amount, Vector2 position)
    {
        for (int i = 0; i < amount; i++)
        {
            float chance = Random.Range(0f, 100f);
            if (chance < spawnChance)
            {
                Vector2 offset = new Vector2(
                    Random.Range(-spawnOffsetRange.x, spawnOffsetRange.x),
                    Random.Range(-spawnOffsetRange.y, spawnOffsetRange.y)
                );

                Vector3 spawnPosition = position + offset;
                if (count <= maxEnemies)
                {
                    Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, enemiesParent);
                    count++;
                }
            }
        }
    }

    void SpawnEnemies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            this.SpawnEnemy(Random.Range(1, 4), new Vector2(spawnPoint.position.x, spawnPoint.position.y));
        }
    }

    public void KillEnemy()
    {
        count--;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        foreach (Transform t in transform)
        {
            Gizmos.DrawSphere(t.position, gizmoRadius);
        }
    }
}
