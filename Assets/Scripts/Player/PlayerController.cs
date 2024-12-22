using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10f;
    private float health;

    private SpriteRenderer rend;
    private Color hitColor = Color.red; // Cor de feedback
    private Color originalColor; // Cor original do sprite

    [Header("Invincibility Frames")]
    [SerializeField] private float invincibilityDuration = 1f; // Duração dos iframes
    private bool isInvincible = false; // Controla se o jogador está invulnerável


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;


        rend = GetComponent<SpriteRenderer>();
        if (rend != null)
        {
            originalColor = rend.color; // Salva a cor original
        }
        else
        {
            Debug.LogError("SpriteRenderer não encontrado!");
        }
    }

    // Update is called once per frame
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameOver();
        }
        else
        {
            StartCoroutine(InvincibilityFrames());
        }
    }

    private IEnumerator InvincibilityFrames()
    {
        if (isInvincible) yield break; // Sai imediatamente se já está invencível
        if (rend == null) yield break;

        isInvincible = true;
        rend.color = (rend.color == originalColor) ? hitColor : originalColor;
        yield return new WaitForSeconds(invincibilityDuration);
        rend.color = originalColor;
        isInvincible = false;
    }


    private void GameOver()
    {
        Debug.Log("Morreu");
    }
}
