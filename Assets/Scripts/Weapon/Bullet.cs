using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private float defaultDamage = 1f;
    [SerializeField] private float explosionDamage = 2f;

    public bool explosionBullet;

    private float explosionRadius = 2f;

    public LayerMask explosionLayer;

    private void Awake()
    {
        explosionBullet = false;
    }

    private void Update()
    {
        if (explosionBullet)
        {
            StartCoroutine(Explode());
        }
            
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); // Destrói o objeto quando ele sair da tela
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (explosionBullet) {
                StartCoroutine(Explode());
            }
            else
            {
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(defaultDamage);
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("Wall"))
        {
            if (explosionBullet)
            {
                StartCoroutine(Explode());
            }
            else
            { 
                Destroy(gameObject);
            }
        }
    }
 
    private IEnumerator Explode()
    {
       Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, explosionLayer);

        foreach (Collider2D collider in colliders)
        {
           collider.gameObject.GetComponent<EnemyController>().TakeDamage(explosionDamage);
        }

        yield return new WaitForSeconds(0.1f); // Pequeno atraso para garantir a detecção

        Destroy(gameObject);
    }


    private void OnDrawGizmos()
    {
        if (explosionBullet) // Só desenha se for uma bala explosiva
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f); // Vermelho semi-transparente
            Gizmos.DrawSphere(transform.position, explosionRadius);
        }
    }
}
