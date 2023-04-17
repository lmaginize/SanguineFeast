using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    private BloodSucking bs;

    public float radius;
    public float damage;
    public float reach;
    public float stun;

    public GameObject projectile;
    public float rangeDamage;
    public float projectileSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        bs = GameObject.Find("Player").GetComponent<BloodSucking>();
    }

    public void Punch(float damage, float stun)
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, reach))
        {
            if (hit.collider.gameObject.CompareTag("NPC"))
            {
                hit.collider.gameObject.GetComponent<HealthBehaviour>().ReceiveHit(damage, stun);
            }
        }
    }

    public void Punch(float damage)
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, reach))
        {
            if (hit.collider.gameObject.CompareTag("NPC"))
            {
                bs.currentBlood -= damage;
            }
        }
    }

    public void Shoot(float damage, float stun)
    {

    }

    public void Shoot(float damage)
    {

    }

    public void SneakAttack()
    {

    }

    public void DamagePlayer()
    {

    }

    public void DamageNPC()
    {

    }
}
