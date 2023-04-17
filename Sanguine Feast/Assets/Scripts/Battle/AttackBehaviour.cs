using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    private BloodSucking bs;

    private int type = 0;

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
        if (CompareTag("Player"))
        {
            type = 1;
        }

        bs = GameObject.Find("Player").GetComponent<BloodSucking>();
    }

    public void Punch()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, reach))
        {
            switch (type)
            {
                case 1:

                    if (hit.collider.gameObject.CompareTag("NPC"))
                    {
                        hit.collider.gameObject.GetComponent<HealthBehaviour>().ReceiveHit(damage, stun);
                    }

                    break;

                default:

                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        bs.currentBlood -= damage;
                    }

                    break;
            }
        }
    }

    public void Shoot(Vector3 dir)
    {
        Rigidbody rb_ = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        ProjectileBehaviour pb = rb_.gameObject.GetComponent<ProjectileBehaviour>();
        pb.damage = damage;

        if (type == 1)
        {
            pb.stun = stun;
        }

        rb_.velocity = dir * projectileSpeed;
    }

    public void SneakAttack()
    {

    }

    public void StartDrain()
    {

    }
}
