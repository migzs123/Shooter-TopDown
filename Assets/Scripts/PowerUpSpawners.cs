using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawners : MonoBehaviour
{
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private float spawnChance = 100f; // 0 a 100
    [SerializeField] private Transform powerUpsParent;

    private float timer = 0f;
    [SerializeField] private float gap = 5f;

    [Header("Debug")]
    [SerializeField] private Color gizmoColor = Color.yellow;
    [SerializeField] private float gizmoRadius = 0.3f;


    private List<Transform> spawnPoints = new List<Transform>();

    private void Start()
    {
        if (powerUpPrefab == null) return;
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
            SpawnPowerUps();
            timer = 0f;
        }
    }

    void SpawnPowerUps()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            float chance = Random.Range(0f, 100f);
            if (chance < spawnChance)
                if(!HasObjectAtPosition(spawnPoint.position, "Power-Up"))
                    Instantiate(powerUpPrefab, spawnPoint.position, Quaternion.identity, powerUpsParent);
        }
    }

    bool HasObjectAtPosition(Vector2 position, string tag, float maxDistance = 0.1f)
    {
        GameObject[] objetos = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objetos)
        {
            if (Vector2.Distance(obj.transform.position, position) < maxDistance)
            {
                return true;
            }
        }
        return false;
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
