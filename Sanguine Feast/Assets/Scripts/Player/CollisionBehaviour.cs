using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBehaviour : MonoBehaviour
{
    public CapsuleCollider coll;
    public Rigidbody rb;
    public MovementBehaviour mb;

    /// <summary>
    /// Grounding Shit
    /// </summary>
    public PhysicMaterial normal;
    public PhysicMaterial slip;
    public ContactPoint[] contacts;
    public RaycastHit hit;
    public Vector3 dir;
    public Vector3 point;
    public Vector3 curveCenterBottom;
    public bool passthrough;

    /// <summary>
    /// Carried Values
    /// </summary>
    public float maxSlope = 40;
    public bool grounded = false;
    public Vector3 groundNormal;

    void Awake()
    {
        mb = GetComponent<MovementBehaviour>();
        coll = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Calls GroundCheck, inserting its contact points
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        passthrough = grounded;

        contacts = new ContactPoint[collision.contactCount];
        collision.GetContacts(contacts);
        GroundCheck(contacts);

        if (passthrough && grounded)
        {
            rb.velocity = Vector3.ProjectOnPlane(mb.moveDir * mb.speedCap, groundNormal);
        }
    }

    /// <summary>
    /// Use in case groundcheck failure
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay(Collision collision)
    {
        if (mb.canMove)
        {
            curveCenterBottom = coll.bounds.center - transform.up * (coll.bounds.extents.y - coll.radius);

            if (!grounded)
            {
                contacts = new ContactPoint[collision.contactCount];
                collision.GetContacts(contacts);
                GroundCheck(contacts);
            }
            else if (Physics.SphereCast(transform.position, coll.radius, -transform.up, out hit) && groundNormal != hit.normal)
            {
                groundNormal = hit.normal;
            }
        }
    }

    /// <summary>
    /// Use in case ground detection failure
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        //Check if ungrounded
        if (collision == null || collision.contactCount == 0)
        {
            if (grounded && mb.ungroundDouble)
            {
                UngroundDoubleCheck();
            }
            else
            {
                grounded = false;
                coll.material = slip;
                mb.ungroundDouble = false;
            }
        }
    }

    void UngroundDoubleCheck()
    {
        curveCenterBottom = coll.bounds.center - transform.up * (coll.bounds.extents.y - coll.radius);

        if (Physics.SphereCast(transform.position, coll.radius, -transform.up, out hit))
        {
            Vector3 normal = hit.normal;

            if (Physics.Raycast(hit.point, transform.up, out hit, coll.height / 2) && Mathf.Abs(Vector3.Angle(normal, transform.up)) <= maxSlope)
            {
                rb.MovePosition(transform.position - transform.up * hit.distance);
                rb.velocity = Vector3.ProjectOnPlane(mb.moveDir * rb.velocity.magnitude, hit.normal);
            }
            else
            {
                grounded = false;
                coll.material = slip;
                mb.ungroundDouble = false;
            }
        }
        else
        {
            grounded = false;
            coll.material = slip;
            mb.ungroundDouble = false;
        }
    }

    /// <summary>
    /// Script used to find a grounding or wall hop-off point
    /// </summary>
    /// <param name="contacts_">
    /// Contacts gathered when OnCollisionEnter is called
    /// </param>
    void GroundCheck(ContactPoint[] contacts_)
    {
        point = Vector3.zero;
        groundNormal = Vector3.zero;

        curveCenterBottom = coll.bounds.center - transform.up * (coll.bounds.extents.y - coll.radius);

        foreach (ContactPoint c in contacts_)
        {
            dir = (curveCenterBottom - c.point).normalized;

            //Ground detect
            if (Mathf.Abs(Vector3.Angle(dir, transform.up)) < 90 && Mathf.Abs(Vector3.Angle(c.normal, transform.up)) <= maxSlope)
            {
                if (Mathf.Abs(Vector3.Angle(groundNormal, dir)) <= 4)
                {
                    groundNormal = c.normal;
                }
                else
                {
                    groundNormal = dir;
                }

                grounded = true;

                mb.ungroundDouble = true;

                coll.material = normal;
            }
        }
    }
}
