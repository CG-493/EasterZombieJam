using System.Collections;
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
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 moveInput;

    [SerializeField] private float jumpForce;

    [Header("Player Rotation")]
    private bool facingRight = true;
    private GameObject player;

    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float coolDown;
    [SerializeField] private bool canFire;
    public float dire;


    private void Awake()
    {
        moveAction = pInputActions.FindAction("Player/Move");
        attackAction = pInputActions.FindAction("Player/Attack");
        jumpAction = pInputActions.FindAction("Player/Jump");

        if (moveAction == null || jumpAction == null)
        {
            Debug.LogError("Input Actions missing! Check InputActions asset.");
        }

        canFire = true;

        rb = GetComponent<Rigidbody2D>();   
        player = this.gameObject;
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

        if (moveInput.x < 0)
        {
            player.transform.localScale = new Vector3(-5, 5, 5);
            dire = -1;
        }

        if (moveInput.x > 0)
        {
            player.transform.localScale = new Vector3(5, 5, 5);
            dire = 1;
        }

        //Debug.Log("Moving");
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if(IsGrounded())
        {
            //Debug.Log("OnGround");
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);

        }


       // Debug.Log("Jumped");
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        Fire();

       // Debug.Log("Attacked");
    }

    private bool IsGrounded()
    {
        return rb.linearVelocity.y == 0;
    }

    private void Fire()
    {
        if (!canFire)
        {
            return;
        }
        
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        StartCoroutine(StartCooldown());
    }

    IEnumerator StartCooldown()
    {
        canFire = false;
        yield return new WaitForSeconds(coolDown);

        canFire = true;
    }
}
