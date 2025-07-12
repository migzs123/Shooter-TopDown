using UnityEngine;

public class CrosshairHandler : MonoBehaviour
{
    [SerializeField] private RectTransform crosshair;

    void Start()
    {
        Cursor.visible = false;
        // Confina o cursor à janela do jogo
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        // Mantém a mira seguindo o mouse
        crosshair.position = Input.mousePosition;

        // Garante que o mouse não escape (backup)
        if (Cursor.lockState != CursorLockMode.Confined)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}