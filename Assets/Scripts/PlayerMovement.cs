using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement Instance { get; private set; }

    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] InputHandler inputHandler;
    Rigidbody rb;

    bool isGrounded;

    void Awake()
    {
        Instance = this;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 inputVector = inputHandler.GetMovementVectorNormalized();

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * inputVector.y + right * inputVector.x).normalized;

        transform.position += speed * Time.deltaTime * moveDirection;

    }

    public void Jump()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, transform.localScale.y / 2f + 0.1f,LayerMask.GetMask("Ground"));

        if(isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
 
    }

}
