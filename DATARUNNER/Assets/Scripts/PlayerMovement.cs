using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4f;
    // Sprint speed
    [Header("Sprint")]
    public bool IsSprinting { get; private set; }
    public float sprintSpeed = 15f;     
    // Sprint duration
    public float sprintDuration = 10f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool sprintUsed = false;
    private float sprintTimer = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Shift Pressed");
        }
        // Check if grounded
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
         // Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift) && !sprintUsed)
        {
            IsSprinting = true;
            sprintUsed = true;
            sprintTimer = sprintDuration;
            Debug.Log("Sprint Started");
        }

        // Sprint timer
        if (IsSprinting)
        {
            sprintTimer -= Time.deltaTime;

            if (sprintTimer <= 0)
            {
                IsSprinting = false;
                Debug.Log("Sprint Ended");
            }
        }

        // Movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Sprint logic
        float currentSpeed = IsSprinting ? sprintSpeed : speed;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}