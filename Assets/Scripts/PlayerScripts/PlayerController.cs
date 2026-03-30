using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset pInputActions;

    [Header("Input Actions")]
    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction aimAction;
    private InputAction jumpAction;
   


    [Header("Movement Settings")]
    public float walkSpeed = 4f;
    public float sprintSpeed = 8f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    
    

    private void Awake()
    {
        moveAction = pInputActions.FindAction("Player/Move");
        attackAction = pInputActions.FindAction("Player/BasicAttack");
        jumpAction = pInputActions.FindAction("Player/Jump");
    }

    private void OnEnable()
    {
        moveAction.Enable();
        attackAction.Enable();
        jumpAction.Enable();
     

        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;

        jumpAction.performed += OnJump;
        attackAction.performed += OnAttack;

    }

    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;

        jumpAction.performed -= OnJump;
        attackAction.performed -= OnAttack;

        moveAction.Disable();
        attackAction.Disable();
        jumpAction.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
        Debug.Log("Moving");
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        Debug.Log("Jumped");
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        Debug.Log("Attacked");
    }
}
