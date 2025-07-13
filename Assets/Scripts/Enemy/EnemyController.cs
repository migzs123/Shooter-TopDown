using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private float maxHealth=4f;
    [SerializeField] private float health=4f;

    private PowerUpsManager powerUpsManager;

    [Header("Feedback visual")]
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private Sprite feedbackSprite;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (playerObject != null)
        {
            powerUpsManager = playerObject.GetComponent<PowerUpsManager>();
        }
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
        Sprite normalSprite = spriteRenderer.sprite;
        spriteRenderer.sprite = feedbackSprite;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.sprite = normalSprite;
    }
}

