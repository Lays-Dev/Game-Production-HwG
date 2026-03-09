using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4f;
    public float sprintSpeed = 15f;
    public float sprintDuration = 10f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    private PlayerControls3 controls;
    private Vector2 moveInput;

    public bool IsSprinting { get; private set; }
    private bool sprintUsed = false;
    private float sprintTimer = 0f;

    void Awake()
    {
        controls = new PlayerControls3();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Jump.performed += ctx => Jump();

        controls.Player.Sprint.performed += ctx => StartSprint();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        float currentSpeed = IsSprinting ? sprintSpeed : speed;

        controller.Move(move * currentSpeed * Time.deltaTime);

        if (IsSprinting)
        {
            sprintTimer -= Time.deltaTime;

            if (sprintTimer <= 0)
            {
                IsSprinting = false;
                Debug.Log("Sprint Ended");
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void StartSprint()
    {
        if (!sprintUsed)
        {
            IsSprinting = true;
            sprintUsed = true;
            sprintTimer = sprintDuration;
            Debug.Log("Sprint Started");
        }
    }
}