using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairHandler : MonoBehaviour

{
    public Transform player;
    public Transform crosshair;

    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Pega a posição do mouse na tela
        Vector3 mousePosTela = Input.mousePosition;

        // Converte a posição do mouse para o mundo
        Vector3 mousePosMundo = Camera.main.ScreenToWorldPoint(mousePosTela);
        mousePosMundo.z = 0; // Z = 0 para 2D (evita que a mira suba ou desça no eixo Z)

        Vector3 direcaoMira = mousePosMundo - player.position;

        crosshair.position = mousePosMundo;

        angle = Mathf.Atan2(direcaoMira.y, direcaoMira.x) * Mathf.Rad2Deg  - 90f;
    }

}
