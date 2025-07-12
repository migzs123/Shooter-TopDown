using UnityEngine;

public class CrosshairHandler : MonoBehaviour
{
    [SerializeField] private RectTransform crosshair;

    void Start()
    {
        Cursor.visible = false;
        // Confina o cursor � janela do jogo
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        // Mant�m a mira seguindo o mouse
        crosshair.position = Input.mousePosition;

        // Garante que o mouse n�o escape (backup)
        if (Cursor.lockState != CursorLockMode.Confined)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}