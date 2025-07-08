using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Health")]
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float cureAmmount = 3f;
    private float currentHealth;
    private bool hasPotion = false;

    [Header("Invincibility Frames")]
    [SerializeField] private float invincibilityDuration = 1f; // Dura��o dos iframes
    private bool isInvincible = false; // Controla se o jogador est� invulner�vel

    private SpriteRenderer rend;
    private Color hitColor = Color.red; // Cor de feedback
    private Color originalColor; // Cor original do sprite

    private PowerUpsManager powerUpsManager;


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
            Debug.LogError("SpriteRenderer n�o encontrado!");
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
        Debug.Log("Vida atual: " + currentHealth);
    }

    // Update is called once per frame
    public void TakeDamage(float damage)
    {
        
        currentHealth -= damage;
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
        if (isInvincible) yield break; // Sai imediatamente se j� est� invenc�vel
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
