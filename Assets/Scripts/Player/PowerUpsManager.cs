using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    [System.Serializable]
    public class PowerUP
    {
        [SerializeField, Tooltip("Duração do power up (em segundos)")]
        public float durationTime { get; private set; }

        [SerializeField, Tooltip("Chance de ativação (%)")]
        public int dropChance { get; private set; }

        public PowerUP(int dropChance, float durationTime)
        {
            this.dropChance = dropChance;
            this.durationTime = durationTime;
        }
    }

    private int item = -1;

    /* Códigos dos Power-ups:
     * 0 -> Item de cura
     * 1 -> Aumento de firerate
     * 2 -> Balas de fogo (mais dano)
     * 3 -> Balas explosivas (dano em área)
    */

    [SerializeField] private List<PowerUP> powerUPs = new List<PowerUP>();

    [SerializeField] private PowerUpsUI powerUpsUI;
    private PlayerController playerController;
    private Coroutine powerCoroutine;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();

        // Criar a lista de power-ups dentro do Start()
        powerUPs = new List<PowerUP>
        {
            new PowerUP(35, 0.1f),
            new PowerUP(25, 15f),
            new PowerUP(20, 8f),
            new PowerUP(20, 8f)
        };
    }

    private void Update()
    {
        powerUpsUI.changePowerUP(item);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Power-Up") && item < 0)
        {
            Destroy(collision.gameObject);
            randomizeItem();
        }
    }

    private void randomizeItem()
    {
        float randomNumber = Random.Range(0, 100);
        int cumulativeChance = 0;

        for (int i = 0; i < powerUPs.Count; i++)
        {
            cumulativeChance += powerUPs[i].dropChance;

            if (randomNumber < cumulativeChance)
            {
                applyPowerUp(i);
                powerUpsUI.changePowerUP(i);
                Debug.Log("Alterou o power-Up para: " + i);
                return;
            }
        }
    }

    public void applyPowerUp(int powerUpIndex)
    {
        item = powerUpIndex;

        if(item <0)
        {
            powerUpsUI.changePowerUP(-1);
            return;
        }

        if (item == 0)
        {
            playerController.getPotion();
        }
        else
        {
            // Garante que a corrotina anterior seja interrompida antes de iniciar uma nova
            if (powerCoroutine != null)
            {
                StopCoroutine(powerCoroutine);
            }

            powerCoroutine = StartCoroutine(powerDuration(powerUPs[item].durationTime));
        }
    }

    private IEnumerator powerDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        item = -1;
    }

    public int getItem()
    {
        return item;
    }
}
