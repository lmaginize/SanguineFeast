using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public BloodSucking bs;
    public float damage;
    public float stun;

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
    }
}
