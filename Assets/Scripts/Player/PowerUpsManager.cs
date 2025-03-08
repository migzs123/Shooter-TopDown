using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{

    public class PowerUP
    {
        public int dropChance { get; private set; }
        public float durationTime { get; private set; }

        public PowerUP(int dropChance, float durationTime)
        {
            this.dropChance = dropChance;
            this.durationTime = durationTime;
        }

    }

    public int item;

    /* Cada item tem um codigo:
     * 0 -> Item de cura
     * 1 -> Aumento de firerate
     * 2 -> Balas de fogo (mais dano)
     * 3 -> Balas explosivas (dano em área)
    */

    [SerializeField] private PowerUP[] powerUPs = new PowerUP[4];

    [SerializeField] private PowerUpsUI powerUpsUI;

    private PlayerController playerController;
    private Coroutine powerCoroutine;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();

        item = -1;

        powerUPs[0] = new PowerUP(35, 0.1f);
        powerUPs[1] = new PowerUP(25, 15f);
        powerUPs[2] = new PowerUP(20,8f);
        powerUPs[3] = new PowerUP(20, 8f);

    }

    private void Update()
    {

        powerUpsUI.ChangePowerUP(item);

        if (item >= 0)
        {
            //Debug.Log(item);
            if (item == 0)
            {
                playerController.Heal();
            }

            StartCoroutine(PowerDuration(powerUPs[item].durationTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Power-Up") && item<0){
            Destroy(collision.gameObject);
            RandomizeItem();
        }
    }

    private void RandomizeItem()
    {
        float randomNumber = Random.Range(0, 100); // Gera um número aleatório entre 0 e 100.
        int cumulativeChance = 0; // Variável para somar as chances cumulativamente.

        for (int i = 0; i < powerUPs.Length; i++) // Percorre todos os power-ups disponíveis.
        {
            cumulativeChance += powerUPs[i].dropChance; //Soma a chance de drop do item atual.

            if (randomNumber < cumulativeChance) // Se o número aleatório for menor que a chance acumulada...
            {
                ApplyPowerUp(i); // Seleciona o item correspondente.
                return; // Sai da função imediatamente, pois já escolheu um item.
            }
        }
    }

    private void ApplyPowerUp(int powerUpIndex)
    {
        item = powerUpIndex;

        if (item == 0)
        {
            playerController.Heal();
        }

        // Se já houver uma corrotina rodando, para antes de iniciar outra
        if (powerCoroutine != null)
        {
            StopCoroutine(powerCoroutine);
        }

        powerCoroutine = StartCoroutine(PowerDuration(powerUPs[item].durationTime));
    }

    private IEnumerator PowerDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        item = -1;
    }
}
