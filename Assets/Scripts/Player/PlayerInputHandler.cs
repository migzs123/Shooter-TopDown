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

    private void Start()
    {
        controller = GetComponent<PlayerController>();
    }


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            weapon.Fire();
        }
       
    }


    void FixedUpdate()
    {
        getMovementInput();

        if (Input.GetKey(KeyCode.E))
        {
            controller.usePotion();
        }

        // Atualiza a velocidade
        rb.velocity = new Vector2(moveInputX * moveSpeed, moveInputY * moveSpeed);

        // Atualiza o angulo
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