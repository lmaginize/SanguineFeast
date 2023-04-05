using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementBehaviour : MonoBehaviour
{
    public GameController gc;
    public Rigidbody rb;
    public CollisionBehaviour cb;
    public CapsuleCollider coll;
    public Camera cam;

    private PlayerControls pcs;

    private InputAction move;
    private InputAction look;
    private InputAction sprint_;
    private InputAction jump_;

    public float sensitivity = 50;
    public float maxLook = 90;
    public float minLook = -60;

    public bool canMove = true;
    private bool sprinting = false;

    public Vector3 up;

    /// <summary>
    /// Movement Shit
    /// </summary>
    public int hori;
    public int vert;
    public Vector3 moveDir;
    public float moveForce = 10;
    public float airMoveForce = 1;
    public float airMoveSpeed;
    public float walkSpeed = 10f;
    public float sprintSpeed = 20f;
    public float speedCap = 10f;

    public Vector3 finalMove;
    public Vector3 jumpDir;

    public bool crouched = false;

    public float jumpForce = 200f;

    public bool ungroundDouble = true;

    /// <summary>
    /// Cam and Collider Shit
    /// </summary>
    public Vector3 standOffset = new Vector3(0f, 0.5369999f, 0f);
    public Vector3 crouchOffset = Vector3.zero;

    public Vector3 camOffset;

    // Start is called before the first frame update

    void Awake()
    {
        pcs = InputManager.pcs;

        look = pcs.Gameplay.Look;
        move = pcs.Gameplay.Move;
        sprint_ = pcs.Gameplay.Sprint;
        jump_ = pcs.Gameplay.Jump;

        sprint_.performed += Sprint;
        sprint_.canceled += Sprint;
        jump_.performed += Jump;

        up = Vector3.up;
        gc = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody>();
        cb = GetComponent<CollisionBehaviour>();
        coll = GetComponent<CapsuleCollider>();
        cam = GetComponentInChildren<Camera>();
    }

    private void OnEnable()
    {
        look.Enable();
        move.Enable();
        sprint_.Enable();
        jump_.Enable();
        sprint_.performed += Sprint;
        sprint_.canceled += Sprint;
        jump_.performed += Jump;
    }

    private void OnDisable()
    {
        sprint_.performed -= Sprint;
        sprint_.canceled -= Sprint;
        jump_.performed -= Jump;
        look.Disable();
        move.Disable();
        sprint_.Disable();
        jump_.Disable();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// Frames
    /// </summary>
    void Update()
    {
        if (canMove)
        {
            moveDir = transform.TransformDirection(new Vector3(move.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y));

            CameraMovement();
        }
    }

    /// <summary>
    /// Update, but for physics reliant items
    /// </summary>
    void FixedUpdate()
    {
        if (canMove)
        {
            Movement();
        }
    }

    private float xRotation = 0;

    /// <summary>
    /// Controls the camera
    /// </summary>
    void CameraMovement()
    {
        xRotation = Mathf.Clamp(xRotation - look.ReadValue<Vector2>().y * 5 * sensitivity * Time.deltaTime, minLook, maxLook);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * look.ReadValue<Vector2>().x * 5 * sensitivity * Time.deltaTime);
    }

    /// <summary>
    /// Contains the script for walking based on where the player is touching the ground (needs revising for corners)
    /// </summary>
    void Movement()
    {
        Vector3 sideForward = transform.InverseTransformDirection(rb.velocity) - Vector3.up * transform.InverseTransformDirection(rb.velocity).y;

        if (cb.grounded)
        {
            //Set speed and jump distance
            if (sprinting)
            {
                ChangeSpeed(sprintSpeed);
            }
            else
            {
                ChangeSpeed(walkSpeed);
            }

            finalMove = Vector3.ProjectOnPlane(moveDir, cb.groundNormal);

            rb.AddForce(finalMove * moveForce, ForceMode.Acceleration);

            if (sideForward.magnitude > speedCap)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, speedCap);
            }
        }
        else
        {
            airMoveSpeed = speedCap * 0.75f;

            rb.AddForce(moveDir * airMoveForce, ForceMode.Acceleration);

            if (sideForward.magnitude > airMoveSpeed)
            {
                rb.velocity = Vector3.ClampMagnitude(transform.TransformDirection(sideForward), airMoveSpeed) + Vector3.up * rb.velocity.y;
            }
        }

        if (rb.velocity.magnitude < moveForce / rb.mass && moveDir != Vector3.zero)
        {
            rb.velocity = Vector3.ProjectOnPlane(moveDir * speedCap / 2, cb.groundNormal);
        }
    }

    void ChangeSpeed(float speed)
    {
        if (speedCap != speed)
        {
            speedCap = speed;
        }
    }

    private void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            sprinting = true;
        }
        else if (context.canceled)
        {
            sprinting = false;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (cb.grounded == true && canMove && context.performed && rb != null)
        {
            ungroundDouble = false;
            cb.grounded = true;
            rb.AddForce(up * jumpForce, ForceMode.Impulse);
        }          
    }
}
