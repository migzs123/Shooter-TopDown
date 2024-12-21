using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bulletPrefab;
    public Transform firePoint;

    public float fireForce = 20f;

    public float cooldownTime = 0.5f; // Tempo de cooldown em segundos
    private bool canFire = true; // Controle para saber se o jogador pode atirar

    public void Fire()
    {
        if (canFire)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);

            StartCoroutine(FireCooldown()); // Inicia o cooldown
        }
    }

    private IEnumerator FireCooldown()
    {
        canFire = false; // Impede que o jogador atire
        yield return new WaitForSeconds(cooldownTime); // Espera o tempo do cooldown
        canFire = true; // Permite que o jogador atire novamente
    }
}

