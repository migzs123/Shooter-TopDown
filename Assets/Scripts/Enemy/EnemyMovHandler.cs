using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovHandler : MonoBehaviour
{
    [SerializeField] private Transform player; // Refer�ncia ao Transform do jogador
    [SerializeField] private float speed =2f;

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}
