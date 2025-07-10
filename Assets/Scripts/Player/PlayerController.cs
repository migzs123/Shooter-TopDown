using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Header("Health")]
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float cureAmmount = 3f;
    private float currentHealth;
    private bool hasPotion = false;

    [Header("Invincibility Frames")]
    [SerializeField] private float invincibilityDuration = 1f; // Duração dos iframes
    private bool isInvincible = false; // Controla se o jogador está invulnerável

    private SpriteRenderer rend;
    private Color hitColor = Color.red; // Cor de feedback
    private Color originalColor; // Cor original do sprite

    private PowerUpsManager powerUpsManager;
    [SerializeField] private Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        powerUpsManager = GetComponent<PowerUpsManager>();

        currentHealth = maxHealth;


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

    public void Heal()
    {

        if(currentHealth+ cureAmmount > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += cureAmmount;
        }
        UpdateHealthBar();
        Debug.Log("Vida atual: " + currentHealth);
    }

    // Update is called once per frame
    public void TakeDamage(float damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            GameOver();
        }
        else
        {
            StartCoroutine(DamageFlashThenInvincibility());
        }

        Debug.Log("Vida atual: " + currentHealth);
    }

    private IEnumerator DamageFlashThenInvincibility()
    {
        if (rend == null) yield break;

        // Feedback imediato de dano (flash vermelho)
        rend.color = hitColor;
        yield return new WaitForSeconds(0.1f);

        // Volta à cor original antes dos i-frames
        rend.color = originalColor;

        // Inicia os i-frames
        StartCoroutine(InvincibilityFrames());
    }

    private IEnumerator InvincibilityFrames()
    {
        if (isInvincible || rend == null) yield break;

        isInvincible = true;

        float elapsed = 0f;
        float blinkInterval = 0.3f; 

        while (elapsed < invincibilityDuration)
        {
            // Deixa o sprite quase transparente
            Color transparentColor = rend.color;
            transparentColor.a = 0.3f;
            rend.color = transparentColor;

            yield return new WaitForSeconds(blinkInterval);

            // Volta a ser opaco
            Color opaqueColor = rend.color;
            opaqueColor.a = 1f;
            rend.color = opaqueColor;

            yield return new WaitForSeconds(blinkInterval);

            elapsed += blinkInterval * 2;
        }

        // Restaura a cor original e opacidade total
        rend.color = originalColor;
        isInvincible = false;
    }

    private void UpdateHealthBar()
    {
        slider.value = currentHealth / maxHealth;
    }

    private void GameOver()
    {
        Debug.Log("Morreu");
    }

    public bool HasPotion()
    {
        return hasPotion;
    }

    public void getPotion() { 
        this.hasPotion = true;
    }

    public void usePotion() {
        if (hasPotion)
        {
            this.Heal();
            this.hasPotion = false;
            powerUpsManager.applyPowerUp(-1);
        }
    }
}
