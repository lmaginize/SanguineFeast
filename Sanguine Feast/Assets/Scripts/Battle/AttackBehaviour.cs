using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    private BloodSucking bs;
    private LayerMask lm;

    private int type = 0;

    public float radius;
    public float[] damage;
    public float[] reach;
    public float[] stun;
    public float[] cooldown;
    public bool[] attack;

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
        lm = LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer));

        StartCoroutine("AttackLoop");
    }

    public void Punch()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, radius, transform.forward, out hit, reach[0]))
        {
            switch (type)
            {
                case 1:

                    if (hit.collider.gameObject.CompareTag("NPC"))
                    {
                        hit.collider.gameObject.GetComponent<HealthBehaviour>().ReceiveHit(damage[0], stun[0]);
                    }

                    break;

                default:

                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        bs.currentBlood -= damage[0];
                    }

                    break;
            }
        }
    }

    public void Shoot(Vector3 dir)
    {
        Rigidbody rb_ = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        ProjectileBehaviour pb = rb_.gameObject.GetComponent<ProjectileBehaviour>();
        pb.damage = damage[1];

        if (type == 1)
        {
            pb.stun = stun[1];
        }

        rb_.velocity = dir * projectileSpeed;
    }

    public void SneakAttack()
    {
        HealthBehaviour hb;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, reach[2]))
        {
            if (hit.collider.gameObject.CompareTag("NPC"))
            {
                hb = hit.collider.gameObject.GetComponent<HealthBehaviour>();
                hb.ReceiveHit(hb.health, stun[2]);
                //BS receive blood ^
            }
        }
    }

    public void StartDrain()
    {

    }

    IEnumerator AttackLoop()
    {
        while (true)
        {
            if (attack[0])
            {
                Punch();

                yield return new WaitForSeconds(cooldown[0]);
            }
            else if (attack[1])
            {
                Shoot(transform.forward);

                yield return new WaitForSeconds(cooldown[1]);
            }
            else if (attack[2])
            {
                SneakAttack();

                yield return new WaitForSeconds(cooldown[2]);
            }
            else if (attack[3])
            {
                StartDrain();

                yield return new WaitForSeconds(cooldown[3]);
            }

            yield return null;
        }
    }
}
