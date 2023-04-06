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
    public CamBehaviour camBeh;

    public PlayerControls pcs;

    private InputAction move;
    private InputAction sprint_;
    private InputAction jump_;
    private InputAction crouch;

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
    public float crouchSpeed = 3f;
    public float walkSpeed = 6f;
    public float sprintSpeed = 12f;
    public float speedCap = 10f;
    public float nightMultiplier;

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
        pcs = new PlayerControls();

        move = pcs.Gameplay.Move;
        sprint_ = pcs.Gameplay.Sprint;
        jump_ = pcs.Gameplay.Jump;
        crouch = pcs.Gameplay.Crouch;

        sprint_.performed += Sprint;
        sprint_.canceled += Sprint;
        jump_.performed += Jump;
        crouch.performed += OnCrouch;

        up = Vector3.up;
        gc = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody>();
        cb = GetComponent<CollisionBehaviour>();
        coll = GetComponent<CapsuleCollider>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        camBeh = GetComponent<CamBehaviour>();
    }

    private void OnEnable()
    {
        move.Enable();
        sprint_.Enable();
        jump_.Enable();
        crouch.Enable();
        sprint_.performed += Sprint;
        sprint_.canceled += Sprint;
        jump_.performed += Jump;
        crouch.performed += OnCrouch;
    }

    private void OnDisable()
    {
        sprint_.performed -= Sprint;
        sprint_.canceled -= Sprint;
        jump_.performed -= Jump;
        crouch.performed -= OnCrouch;
        move.Disable();
        sprint_.Disable();
        jump_.Disable();
        crouch.Disable();
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
            if (camBeh.camMode != 2)
            {
                moveDir = transform.TransformDirection(new Vector3(move.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y));
            }
            else
            {
                moveDir = new Vector3(move.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y);
                transform.LookAt(new Vector3(transform.position.x + rb.velocity.x, transform.position.y, transform.position.z + rb.velocity.z));
            }
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

    /// <summary>
    /// Contains the script for walking based on where the player is touching the ground (needs revising for corners)
    /// </summary>
    void Movement()
    {
        Vector3 sideForward = transform.InverseTransformDirection(rb.velocity) - Vector3.up * transform.InverseTransformDirection(rb.velocity).y;

        if (cb.grounded)
        {
            //Set speed and jump distance
            if (crouched)
            {
                ChangeSpeed(crouchSpeed);
            }
            else if (sprinting)
            {
                ChangeSpeed(sprintSpeed);
            }
            else
            {
                ChangeSpeed(walkSpeed);
            }

            finalMove = Vector3.ProjectOnPlane(moveDir, cb.groundNormal);

            rb.AddForce(finalMove * moveForce * (gc.night ? nightMultiplier : 1), ForceMode.Acceleration);

            if (sideForward.magnitude > speedCap * (gc.night ? nightMultiplier : 1))
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, speedCap);
            }
        }
        else
        {
            airMoveSpeed = speedCap * 0.75f * (gc.night ? nightMultiplier : 1);

            rb.AddForce(moveDir * airMoveForce, ForceMode.Acceleration);

            if (sideForward.magnitude > airMoveSpeed)
            {
                rb.velocity = Vector3.ClampMagnitude(transform.TransformDirection(sideForward), airMoveSpeed) + Vector3.up * rb.velocity.y;
            }
        }
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed && !crouched)
        {
            Crouch();
        }
        else if (context.performed)
        {
            UnCrouch();
        }
    }

    public void Crouch()
    {
        coll.height /= 1.5f;
        coll.center = new Vector3(0, -0.25f, 0);

        if (camBeh.camMode == 0)
        {
            camOffset = crouchOffset;
            speedCap = crouchSpeed;
            camBeh.cam.transform.localPosition = camOffset;
        }

        crouched = true;
    }

    public void UnCrouch()
    {
        coll.height *= 1.5f;
        coll.center = new Vector3(0, 0, 0);

        if (camBeh.camMode == 0)
        {
            camOffset = standOffset;
            speedCap = walkSpeed;
            camBeh.cam.transform.localPosition = camOffset;
        }

        crouched = false;
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

            if (crouched)
            {
                UnCrouch();
            }
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
            rb.AddForce(up * jumpForce * ((moveDir != Vector3.zero) ? rb.velocity.magnitude * 0.1f + 1 : 1f), ForceMode.Impulse);
        }          
    }
}
