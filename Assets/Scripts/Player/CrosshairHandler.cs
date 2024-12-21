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

        // Garante que o mouse não saia da tela
        mousePosTela.x = Mathf.Clamp(mousePosTela.x, 0, Screen.width);
        mousePosTela.y = Mathf.Clamp(mousePosTela.y, 0, Screen.height);

        // Converte a posição do mouse para o mundo
        Vector3 mousePosMundo = Camera.main.ScreenToWorldPoint(mousePosTela);
        mousePosMundo.z = 0; // Z = 0 para 2D (evita que a mira suba ou desça no eixo Z)

        // Define a posição da mira
        crosshair.position = mousePosMundo;

        // Calcula o ângulo da mira em relação ao jogador
        Vector3 direcaoMira = mousePosMundo - player.position;
        angle = Mathf.Atan2(direcaoMira.y, direcaoMira.x) * Mathf.Rad2Deg - 90f;
    }
}
