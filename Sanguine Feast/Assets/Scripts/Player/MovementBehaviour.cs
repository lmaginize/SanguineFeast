using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementBehaviour : MonoBehaviour
{
    public LayerMask player;
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
    private InputAction shadowStep;

    public float sensitivity = 50;
    public float maxLook = 90;
    public float minLook = -60;

    public bool canMove = true;
    private bool abilityActive = false;
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
    public float nightMultiplier = 1;

    public const int MAX_DISTANCETP = 50;

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
    public bool isTping;
    RaycastHit[] h;
    // Start is called before the first frame update


    public GameObject tpIndicator;

    public BloodSucking bs;

    void Awake()
    {
        pcs = new PlayerControls();

        move = pcs.Gameplay.Move;
        sprint_ = pcs.Gameplay.Sprint;
        jump_ = pcs.Gameplay.Jump;
        crouch = pcs.Gameplay.Crouch;
        shadowStep = pcs.Gameplay.ShadowStep;

        sprint_.performed += Sprint;
        sprint_.canceled += Sprint;
        jump_.performed += Jump;
        crouch.performed += OnCrouch;
        shadowStep.performed += ShadowStep;

        up = Vector3.up;
        gc = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody>();
        cb = GetComponent<CollisionBehaviour>();
        coll = GetComponent<CapsuleCollider>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        camBeh = GetComponent<CamBehaviour>();
        bs = GetComponent<BloodSucking>();
    }

    private void OnEnable()
    {
        move.Enable();
        sprint_.Enable();
        jump_.Enable();
        crouch.Enable();
        shadowStep.Enable();
        sprint_.performed += Sprint;
        sprint_.canceled += Sprint;
        jump_.performed += Jump;
        crouch.performed += OnCrouch;
        shadowStep.performed += ShadowStep;
        shadowStep.canceled += ShadowStep;
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
        shadowStep.Disable();
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
        /*if(l != null)
        {
            Debug.DrawLine(transform.position, l);
        }*/
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

        if (Physics.Raycast(coll.bounds.center, Camera.main.transform.forward, out RaycastHit rh, 100, ~player))
        {
            if (Physics.Raycast(rh.point, gc.sunObject.transform.forward * -1, out RaycastHit f, 10000))
            {

                Debug.DrawLine(rh.point, f.point, Color.red);
            }
            Debug.DrawLine(transform.position, rh.point, Color.green);
        }


        /*h = Physics.RaycastAll(coll.bounds.center, Camera.main.transform.forward, 100, ~player );

        foreach(RaycastHit k in h)
        {
            Debug.DrawLine(transform.position, k.point);
        }*/
        //h = Physics.BoxCastAll(coll.bounds.center, transform.localScale, Camera.main.transform.forward, transform.rotation, 100);
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

            finalMove = Vector3.ProjectOnPlane(moveDir.normalized, cb.groundNormal);

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
        if (canMove)
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

    private void ShadowStep(InputAction.CallbackContext context)
    {
        if (context.performed && !isTping)
        {
            StartCoroutine(GoingToTp());
            isTping = true;
        }
        else if(context.canceled)
        {
            StopCoroutine(GoingToTp());
            isTping = false;
        }
    }

    /// <summary>
    /// On Call Decreases blood amount by set value and 
    /// </summary>
    /// <returns></returns>
    IEnumerator GoingToTp()
    {
        //Decrease Blood Amount By X
        bs.currentBlood--;
        Vector3 l = Vector3.zero;
        float time = 2.0f;
        float dist = MAX_DISTANCETP;
        if(Physics.Raycast(coll.bounds.center, Camera.main.transform.forward, out RaycastHit rh, 100, ~player))
        {
            if(Physics.Raycast(rh.point, gc.sunObject.transform.forward * -1, out RaycastHit f, 10000) && f.collider != null && !f.collider.gameObject.name.Equals("SunHitCheck"))
            {
                //dist = Vector3.Distance(transform.position, rh.point);
                l = rh.point;
                print(f.collider.gameObject.name);
            }
            else if(rh.collider.gameObject.tag.Equals("Shade"))
            {
                l = rh.point;
            }
        }
        /*
        foreach (RaycastHit rh in h)
        {
            if (Vector3.Distance(transform.position, rh.point) <= MAX_DISTANCETP && dist > Vector3.Distance(transform.position, rh.point))
            {
                if (Physics.Raycast(rh.point, gc.sunObject.transform.forward * -1, out RaycastHit f, 10000))
                {
                    dist = Vector3.Distance(transform.position, rh.point);
                    l = f.point;
                }
            }
            //gc.sunObject.transform.forward
            /*
            if(rh.collider.gameObject.tag.Equals("Shade") && rh.point != Vector3.zero && Vector3.Distance(transform.position, rh.point) <= MAX_DISTANCETP 
                && dist > Vector3.Distance(transform.position, rh.point))
            {
                //dist = Vector3.Distance(transform.position, rh.point);
                l = rh.point;
            }
        }*/

        if (l != Vector3.zero)
        {
            GameObject g = null;
            if (tpIndicator != null)
                g = Instantiate(tpIndicator, l, Quaternion.identity);
            while (time > 0)
            {
                time -= Time.deltaTime;
                Debug.DrawLine(transform.position, l);
                yield return new WaitForEndOfFrame();
            }
            Vector3 h = l;
            h.y = transform.position.y;
            transform.position = h;
            Destroy(g);
            yield return new WaitForEndOfFrame();
            
        }
        isTping = false;
        yield return null;
    }
}
