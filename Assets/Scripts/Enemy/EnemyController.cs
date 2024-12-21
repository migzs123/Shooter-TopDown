using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private float maxHealth=4f;
    [SerializeField] private float health=4f;

    private void UpdateHealthBar()
    {
        slider.value = health / maxHealth;
    }

    public void Hit(float damage){
        health-= damage;
        UpdateHealthBar();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
