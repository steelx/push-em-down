using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private PlayerInput playerInput;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction escAction;
    private bool cursorVisible = false;

    // Awake happens before OnEnable
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        escAction = playerInput.actions["Menu"];
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = buildFollowCameraMovement(input);

        controller.Move(move * Time.deltaTime * playerSpeed);

        // rotation
        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.deltaTime);
            gameObject.transform.rotation = targetRotation;
        }

        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        UpdateCursorState();
    }

    Vector3 buildFollowCameraMovement(Vector2 input)
    {
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        return move;
    }

    private void OnEnable()
    {
        // subscribe to event
        escAction.performed += _ => ToggleCursor();
    }

    private void OnDisable()
    {
        // unsubscribe
        escAction.performed -= _ => ToggleCursor();
    }

    void ToggleCursor()
    {
        cursorVisible = !cursorVisible;
    }

    void UpdateCursorState()
    {
        Cursor.lockState = !cursorVisible ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = cursorVisible;
    }
}
