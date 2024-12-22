using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float cooldownTime;
    [SerializeField] private float damage;

    private bool ableToHit;

    private Coroutine attackCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ableToHit = true;

            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(Attack(collision.gameObject.GetComponent<PlayerController>()));
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ableToHit = false;
            if (attackCoroutine != null) // Para a corrotina
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }



    IEnumerator Attack(PlayerController player)
    {
        while (ableToHit)
        {
            player.TakeDamage(damage);
            //Debug.Log("Entrou na Corroutine");
            yield return new WaitForSeconds(cooldownTime);
        }
    }
}
