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
    private InputAction shapeShift;

    private GameController gc;
    private MovementBehaviour mb;
    private CamBehaviour camBeh;
    private Rigidbody rb;

    public Vector3 moveDir;
    public float moveSpeed;
    public float moveForce;
    public float slowRate;

    public bool isActive = false;
    private bool speedControl;

    void Awake()
    {
        pcs = new PlayerControls();

        move = pcs.Gameplay.Move;
        jump_ = pcs.Gameplay.Jump;
        crouch = pcs.Gameplay.Crouch;
        sprint = pcs.Gameplay.Sprint;
        shapeShift = pcs.Gameplay.ShapeShift;

        jump_.performed += _ => moveDir += Vector3.up;
        jump_.canceled += _ => moveDir -= Vector3.up;
        crouch.performed += _ => moveDir -= Vector3.up;
        crouch.canceled += _ => moveDir += Vector3.up;
        sprint.performed += _ => speedControl = !speedControl;
        shapeShift.performed += ShapeShift;

        gc = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        camBeh = GetComponent<CamBehaviour>();
        mb = GetComponent<MovementBehaviour>();
    }

    private void OnEnable()
    {
        move.Enable();
        jump_.Enable();
        crouch.Enable();
        sprint.Enable();
        shapeShift.Enable();
        jump_.performed += _ => moveDir += Vector3.up;
        jump_.canceled += _ => moveDir -= Vector3.up;
        crouch.performed += _ => moveDir -= Vector3.up;
        crouch.canceled += _ => moveDir += Vector3.up;
        sprint.performed += _ => speedControl = !speedControl;
        shapeShift.performed += ShapeShift;
    }

    private void OnDisable()
    {
        jump_.performed -= _ => moveDir += Vector3.up;
        jump_.canceled -= _ => moveDir -= Vector3.up;
        crouch.performed -= _ => moveDir -= Vector3.up;
        crouch.canceled -= _ => moveDir += Vector3.up;
        sprint.performed -= _ => speedControl = !speedControl;
        shapeShift.performed -= ShapeShift;
        move.Disable();
        jump_.Disable();
        crouch.Disable();
        shapeShift.Disable();
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

    void BatMovement()
    {
        if (rb.useGravity)
        {
            rb.useGravity = false;
        }

        Vector3 finalMove;

        if (speedControl && camBeh.camMode != 2)
        {
            finalMove = camBeh.cam.gameObject.transform.TransformDirection(moveDir);
        }
        else
        {
            finalMove = gameObject.transform.TransformDirection(moveDir);
        }

        rb.AddForce(finalMove.normalized * moveForce, ForceMode.Acceleration);

        if (rb.velocity.magnitude > moveSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, moveSpeed);
        }
    }

    public void ShapeShift(InputAction.CallbackContext context)
    {
        if (!isActive)
        {
            isActive = true;

            rb.useGravity = false;

            mb.canMove = false;

            gameObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);

            camBeh.SetCamMode(1);
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
