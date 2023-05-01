using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public Rigidbody rb;
    public BloodSucking bs;
    public float damage;
    public float stun;

    private void Awake()
    {
        bs = GameObject.Find("Player").GetComponent<BloodSucking>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        transform.up = rb.velocity.normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (stun > 0)
        {
            if (collision.gameObject.CompareTag("NPC"))
            {
                collision.gameObject.GetComponent<HealthBehaviour>().ReceiveHit(damage, stun);
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                bs.currentBlood -= damage;
            }
        }

        Destroy(gameObject);
    }
}
