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
    [SerializeField] private float invincibilityDuration = 0.3f; // Duração dos iframes
    private bool isInvincible = false; // Controla se o jogador está invulnerável

    private SpriteRenderer rend;
    private Color hitColor = Color.red; // Cor de feedback
    private Color originalColor; // Cor original do sprite

    [SerializeField] private GameObject weapon;
    private SpriteRenderer WPrend;
    private Color WPoriginalColor; // Cor original do sprite

    private PowerUpsManager powerUpsManager;
    [SerializeField] private Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        powerUpsManager = GetComponent<PowerUpsManager>();

        currentHealth = maxHealth;


        rend = GetComponent<SpriteRenderer>();
        WPrend = weapon.GetComponent<SpriteRenderer>();
        if (rend != null && WPrend != null)
        {
            originalColor = rend.color; // Salva a cor original
            WPoriginalColor = WPrend.color;
        }
        else
        {
            Debug.LogError("SpriteRenderer não encontrado!");
        }
    }

    public void Heal()
    {

        if (currentHealth + cureAmmount > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += cureAmmount;
        }
        Debug.Log("Vida atual: " + currentHealth);
    }

    // Update is called once per frame
    public void TakeDamage(float damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        this.UpdateHealthBar();
        if (currentHealth <= 0)
        {
            GameOver();
        }
        else
        {
            StartCoroutine(InvincibilityFrames());
        }
        Debug.Log("Vida atual: " + currentHealth);
    }

    private IEnumerator InvincibilityFrames()
    {
        if (isInvincible) yield break; // Sai imediatamente se já está invencível
        if (rend == null) yield break;

        isInvincible = true;
        rend.color = (rend.color == originalColor) ? hitColor : originalColor;
        WPrend.color = (WPrend.color == WPoriginalColor) ? hitColor : WPoriginalColor;
        yield return new WaitForSeconds(invincibilityDuration);
        rend.color = originalColor;
        WPrend.color = WPoriginalColor;
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

    public void getPotion()
    {
        this.hasPotion = true;
    }

    public void usePotion()
    {
        if (hasPotion)
        {
            this.Heal();
            this.hasPotion = false;
            powerUpsManager.applyPowerUp(-1);
        }
    }
}