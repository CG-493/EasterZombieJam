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
    [SerializeField] private float speed = 4f;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 moveInput;

    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private BoxCollider2D rayCollider;
    [SerializeField] LayerMask groundMask;
    

    private void Awake()
    {
        moveAction = pInputActions.FindAction("Player/Move");
        attackAction = pInputActions.FindAction("Player/Attack");
        jumpAction = pInputActions.FindAction("Player/Jump");

        if (moveAction == null || jumpAction == null)
        {
            Debug.LogError("Input Actions missing! Check InputActions asset.");
        }

        rb = GetComponent<Rigidbody2D>();   
    }

    private void Update()
    {
        IsGrounded();
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

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();

        Vector3 input = new Vector3(moveInput.x, 0, 0);
        input = Vector3.ClampMagnitude(input, 1f);
        Vector3 worldMovement = transform.TransformDirection(input) * speed;

        rb.linearVelocity = new Vector2(
            worldMovement.x,
            rb.linearVelocity.y
            );

        Debug.Log("Moving");
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if(IsGrounded())
        {
            Debug.Log("OnGround");
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
        }


        Debug.Log("Jumped");
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        Debug.Log("Attacked");
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit2D = Physics2D.BoxCast(rayCollider.bounds.center,rayCollider.bounds.size,0, Vector2.down, 0.1f);
        return hit2D.collider != null;
    }
}
