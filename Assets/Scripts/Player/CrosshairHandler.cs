using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairHandler : MonoBehaviour
{
    [SerializeField] private RectTransform crosshair; // Use um RectTransform para a mira no Canvas
    [SerializeField] private Transform player;

    [HideInInspector] public float angle;

    // Start � chamado antes do primeiro frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update � chamado a cada frame
    void Update()
    {
        // Obt�m a posi��o do mouse na tela
        Vector3 mousePosTela = Input.mousePosition;

        // Garante que o mouse n�o saia da tela
        mousePosTela.x = Mathf.Clamp(mousePosTela.x, 0, Screen.width);
        mousePosTela.y = Mathf.Clamp(mousePosTela.y, 0, Screen.height);

        // Ajusta a posi��o do crosshair no espa�o da UI
        crosshair.position = mousePosTela;

        // Calcula o �ngulo da mira em rela��o ao jogador no mundo (opcional, para manter funcionalidade de rota��o)
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(player.position);
        Vector3 direcaoMira = mousePosTela - playerScreenPos;
        angle = Mathf.Atan2(direcaoMira.y, direcaoMira.x) * Mathf.Rad2Deg - 90f;
    }
}
