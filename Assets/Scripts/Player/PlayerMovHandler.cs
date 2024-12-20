using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpPower = 5f;

    private float moveInputX;
    private float moveInputY;

    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
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

        // Atualiza a velocidade
        rb.velocity = new Vector2(moveInputX * moveSpeed, moveInputY * moveSpeed);

        // Inverte o jogador dependendo da direção
    }

}