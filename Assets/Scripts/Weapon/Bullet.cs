using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage = 1f;

    private void OnBecameInvisible()
    {
        Destroy(gameObject); // Destrói o objeto quando ele sair da tela
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {  
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
 
}
