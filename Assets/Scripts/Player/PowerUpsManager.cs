using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{

    public int item;

    /* Cada item tem um codigo:
     * 0 -> Item de cura
     * 1 -> Aumento de firerate
     * 2 -> Balas de fogo (mais dano)
     * 3 -> Balas explosivas (dano em área)
    */

    [SerializeField] private int[] dropChances = new int[4];

    [SerializeField] private float[] durationTime = new float[4];

    private void Start()
    {
        item = -1;

        dropChances[0] = 45;
        dropChances[1] = 25; 
        dropChances[2] = 15; 
        dropChances[3] = 15;

        durationTime[0] = 0.5f;
        durationTime[1] = 15f;
        durationTime[2] = 8f;
        durationTime[3] = 8f;
    }

    private void Update()
    {
        
        if (item >= 0)
        {
            //Debug.Log(item);
            StartCoroutine(PowerDuration(durationTime[item]));
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
        /*
        float randomNumber = Random.Range(0, 100);
        


        if (randomNumber < dropChances[0]) {
            item = 0;
        }
        else if (randomNumber < dropChances[1])
        {
            item = 1;
        }
        else if(randomNumber < dropChances[2])
        {
            item = 2;
        }
        else if(randomNumber < dropChances[3])
        {
            item = 3;
        }
       */

        item = 1;

    }

    private IEnumerator PowerDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        item = -1;
    }
}
