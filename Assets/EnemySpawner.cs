using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnChance = 100f; // 0 a 100
    [SerializeField] private Vector2 spawnOffsetRange = new Vector2(1f, 1f); // deslocamento aleatório opcional

    public void SpawnEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            float chance = Random.Range(0f, 100f);
            if (chance < spawnChance)
            {
                Vector3 offset = new Vector3(
                    Random.Range(-spawnOffsetRange.x, spawnOffsetRange.x),
                    Random.Range(-spawnOffsetRange.y, spawnOffsetRange.y),
                    0f
                );

                Vector3 spawnPosition = transform.position + offset;
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
