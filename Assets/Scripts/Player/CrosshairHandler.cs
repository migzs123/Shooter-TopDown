using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairHandler : MonoBehaviour
{
    public RectTransform crosshair; // Use um RectTransform para a mira no Canvas
    public Transform player;

    public float angle;

    // Start é chamado antes do primeiro frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update é chamado a cada frame
    void Update()
    {
        // Obtém a posição do mouse na tela
        Vector3 mousePosTela = Input.mousePosition;

        // Garante que o mouse não saia da tela
        mousePosTela.x = Mathf.Clamp(mousePosTela.x, 0, Screen.width);
        mousePosTela.y = Mathf.Clamp(mousePosTela.y, 0, Screen.height);

        // Ajusta a posição do crosshair no espaço da UI
        crosshair.position = mousePosTela;

        // Calcula o ângulo da mira em relação ao jogador no mundo (opcional, para manter funcionalidade de rotação)
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(player.position);
        Vector3 direcaoMira = mousePosTela - playerScreenPos;
        angle = Mathf.Atan2(direcaoMira.y, direcaoMira.x) * Mathf.Rad2Deg - 90f;
    }
}
