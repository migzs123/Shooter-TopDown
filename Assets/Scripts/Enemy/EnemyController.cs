using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private float maxHealth=4f;
    [SerializeField] private float health=4f;

    [SerializeField] private PowerUpsManager powerUpsManager;

    [Header("Feedback visual")]
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashDuration = 0.1f;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float damage)
    {
        if (powerUpsManager.getItem() == 2)
        {
            damage *= 2;
        }

        health -= damage;

        // Piscar visual
        StartCoroutine(Flash());

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Flash()
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.color = originalColor;
    }
}

