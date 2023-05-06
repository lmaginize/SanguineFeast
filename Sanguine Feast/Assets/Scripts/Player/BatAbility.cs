using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BatAbility : MonoBehaviour
{
    private PlayerControls pcs;

    private InputAction move;
    private InputAction jump_;
    private InputAction crouch;
    private InputAction sprint;
    //private InputAction shapeShift;

    private GameController gc;
    private MovementBehaviour mb;
    private CamBehaviour camBeh;
    private Rigidbody rb;
    private CollisionBehaviour cb;

    public Vector3 moveDir;
    public float moveSpeed;
    public float moveForce;
    public float slowRate;

    public bool isActive = false;
    public bool speedControl = false;

    void Awake()
    {
        pcs = new PlayerControls();

        move = pcs.Gameplay.Move;
        jump_ = pcs.Gameplay.Jump;
        crouch = pcs.Gameplay.Crouch;
        sprint = pcs.Gameplay.Sprint;
        //shapeShift = pcs.Gameplay.ShapeShift;

        jump_.started += OnJump;
        jump_.canceled += OnJump;
        crouch.started += OnCrouch;
        crouch.canceled += OnCrouch;
        //shapeShift.performed += ShapeShift;

        gc = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        camBeh = GetComponent<CamBehaviour>();
        mb = GetComponent<MovementBehaviour>();
        cb = GetComponent<CollisionBehaviour>();
    }

    private void OnEnable()
    {
        move.Enable();
        jump_.Enable();
        crouch.Enable();
        sprint.Enable();
        //shapeShift.Enable();
        jump_.performed += OnJump;
        jump_.canceled += OnJump;
        crouch.performed += OnCrouch;
        crouch.canceled += OnCrouch;
        //shapeShift.performed += ShapeShift;
    }

    private void OnDisable()
    {
        jump_.performed -= OnJump;
        jump_.canceled -= OnJump;
        crouch.performed -= OnCrouch;
        crouch.canceled -= OnCrouch;
        //shapeShift.performed -= ShapeShift;
        move.Disable();
        jump_.Disable();
        crouch.Disable();
        //shapeShift.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector3(move.ReadValue<Vector2>().x, moveDir.y, move.ReadValue<Vector2>().y);
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            if (mb.canMove)
            {
                mb.canMove = false;
            }

            if (moveDir == Vector3.zero && rb.velocity != Vector3.zero)
            {
                rb.velocity = rb.velocity * slowRate;

                if (rb.velocity.magnitude < 0.1)
                {
                    rb.velocity = Vector3.zero;
                }
            }

            BatMovement();
        }
        else
        {
            if (!rb.useGravity)
            {
                rb.useGravity = true;
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveDir += Vector3.up;
        }
        else if (context.canceled)
        {
            moveDir -= Vector3.up;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveDir -= Vector3.up;
        }
        else if (context.canceled)
        {
            moveDir += Vector3.up;
        }
    }

    void BatMovement()
    {
        if (rb.useGravity)
        {
            rb.useGravity = false;
        }

        if (mb.ungroundDouble)
        {
            mb.ungroundDouble = false;
        }

        Vector3 finalMove = camBeh.camObjs[camBeh.camMode].gameObject.transform.TransformDirection(moveDir);

        rb.AddForce(finalMove.normalized * moveForce, ForceMode.Acceleration);

        if (rb.velocity.magnitude > moveSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, moveSpeed);
        }
    }

    public void ShapeShift(InputAction.CallbackContext context)
    {
        AbilityController.bloodCost.TryGetValue("Bat Transformation", out int value);
        if (!isActive)
        {
            isActive = true;

            rb.useGravity = false;

            mb.canMove = false;

            mb.ungroundDouble = false;

            gameObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);

            camBeh.SetCamMode(1);
            transform.gameObject.GetComponent<BloodSucking>().currentBlood -= value;

        }
        else
        {
            isActive = false;

            rb.useGravity = true;

            mb.canMove = true;

            gameObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, transform.localScale.z);

            camBeh.SetCamMode(0);
        }
    }
}
