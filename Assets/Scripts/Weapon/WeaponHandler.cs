using System.Collections;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float fireForce = 20f;

    private float baseCooldownTime = 0.5f; // Cooldown base em segundos
    private float cooldownTime; // Cooldown atual
    private bool canFire = true; // Controle para saber se o jogador pode atirar

    public PowerUpsManager powerUpsManager;

    private void Start()
    {

        if (powerUpsManager == null)
        {
            Debug.LogError("PowerUpsManager não encontrado no GameObject.");
        }

        cooldownTime = baseCooldownTime; // Inicializa com o valor base
    }

    private void Update()
    {
        // Atualiza o cooldown baseado no Power-Up ativo (se houver)
        if (powerUpsManager != null)
        {

            if (powerUpsManager.getItem() == 1)
            {
                cooldownTime = baseCooldownTime/5;
            }
            else if (powerUpsManager.getItem() == 3)
            {
                cooldownTime = baseCooldownTime*2;
            }
            else {
                cooldownTime = baseCooldownTime;
            }

        }
    }

    public void Fire()
    {
        if (canFire)
        {
            // Instancia a bala
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            if(powerUpsManager.getItem() == 3)
            {
                bullet.GetComponent<Bullet>().explosionBullet = true;
            }


            // Aplica força à bala
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
            }
            else
            {
                Debug.LogError("Rigidbody2D não encontrado no bulletPrefab.");
            }

            // Inicia o cooldown
            StartCoroutine(FireCooldown());
        }
    }

    private IEnumerator FireCooldown()
    {
        canFire = false; // Impede que o jogador atire
        yield return new WaitForSeconds(cooldownTime); // Espera o tempo do cooldown
        canFire = true; // Permite que o jogador atire novamente
    }
}
