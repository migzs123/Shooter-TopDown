using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CrosshairHandler crosshair;
    [SerializeField] private WeaponHandler weapon;

    [SerializeField] private float moveSpeed = 5f;

    private float moveInputX;
    private float moveInputY;

    private PlayerController controller;
    private Vector2 moveInput;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        getMovementInput();

        // Movimento em vetor normalizado
        moveInput = new Vector2(moveInputX, moveInputY).normalized;

        if (Input.GetMouseButton(0))
        {
            weapon.Fire();
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E))
        {
            controller.usePotion();
        }

        // Usa MovePosition para aplicar movimento com colisão suave
        Vector2 targetPos = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPos);

        rb.rotation = crosshair.angle;
    }


    void getMovementInput()
    {
        // Movimento Horizontal
        moveInputX = 0f;

        if (Input.GetKey(KeyCode.D))
        {
            moveInputX = 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveInputX = -1f;
        }

        // Movimento Vertical
        moveInputY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveInputY = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveInputY = -1f;
        }
    }

}