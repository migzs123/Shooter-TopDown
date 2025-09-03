using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovHandler : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float speed = 2f;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}