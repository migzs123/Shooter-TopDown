using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private float maxHealth=4f;
    [SerializeField] private float health=4f;

    [SerializeField] private PowerUpsManager powerUpsManager;

    public void TakeDamage(float damage){
        if (powerUpsManager.getItem() == 2)
        {
            damage *= 2;
        }

        health-= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
