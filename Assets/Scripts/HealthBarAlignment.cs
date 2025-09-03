using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarAlignment : MonoBehaviour
{
    private Transform player;
    private Vector3 playerP;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerP = new Vector3(player.position.x, player.position.y - 0.7f, 10f);

        this.transform.position = playerP;

    }
}
