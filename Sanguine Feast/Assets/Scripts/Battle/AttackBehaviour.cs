using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackBehaviour : MonoBehaviour
{
    private BloodSucking bs;
    private LayerMask lm;
    private Rigidbody prb;

    private PlayerControls pcs;

    private InputAction attack_;
    private InputAction interact;
    private InputAction batAttack;

    private int type = 0;

    public float radius;
    public float[] damage;
    public float[] reach;
    public float[] stun;
    public float[] cooldown;
    public bool[] attack;

    public float stunAngle;

    public GameObject projectile;
    public float rangeDamage;
    public float projectileSpeed;

    public GameObject batAttackObj;
    private GameObject batAttackSpawned;

    public GameObject target;

    // Start is called before the first frame update
    void Awake()
    {
        if (CompareTag("Player"))
        {
            type = 1;

            pcs = new PlayerControls();

            attack_ = pcs.Gameplay.Attack1;
            interact = pcs.Gameplay.Hold;
            batAttack = pcs.Gameplay.BatAttack;

            attack_.performed += _ => attack[0] = true;
            interact.performed += _ => attack[2] = true;
            batAttack.performed += _ => attack[5] = true;
        }
        else
        {
            target = GameObject.Find("Player");
            prb = target.GetComponent<Rigidbody>();
        }

        bs = GameObject.Find("Player").GetComponent<BloodSucking>();
        lm = LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer));

        StartCoroutine("AttackLoop");
    }

    private void OnEnable()
    {
        if (type == 1)
        {
            attack_.Enable();
            interact.Enable();
            batAttack.Enable();

            attack_.performed += _ => attack[0] = true;
            interact.performed += _ => attack[2] = true;
            batAttack.performed += _ => attack[5] = true;
        }
    }

    private void OnDisable()
    {
        if (type == 1)
        {
            attack_.performed -= _ => attack[0] = true;
            interact.performed -= _ => attack[2] = true;
            batAttack.performed -= _ => attack[5] = true;

            attack_.Disable();
            interact.Disable();
            batAttack.Disable();
        }
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
                        if (hit.collider.name.Contains("Patroller") || !hit.collider.gameObject.GetComponent<NPCBehaviour>().isStunned)
                        {
                            if (Vector3.Angle(transform.position - hit.collider.gameObject.transform.position, hit.collider.gameObject.transform.forward) > stunAngle)
                            {
                                hit.collider.gameObject.GetComponent<HealthBehaviour>().ReceiveHit(damage[2], stun[2]);
                            }
                            else
                            {
                                hit.collider.gameObject.GetComponent<HealthBehaviour>().ReceiveHit(damage[0], stun[0]);
                            }
                        }
                        else
                        {
                            StartDrain();
                        }
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
        print("pew");
        Rigidbody rb_ = Instantiate(projectile, transform.position + transform.forward * 1, Quaternion.identity).GetComponent<Rigidbody>();
        ProjectileBehaviour pb = rb_.gameObject.GetComponent<ProjectileBehaviour>();
        pb.damage = damage[1];

        if (type == 1)
        {
            pb.stun = stun[1];
        }

        rb_.AddForce(dir.normalized * projectileSpeed, ForceMode.VelocityChange);
    }

    public void StartDrain()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, reach[2]))
        {
            if (hit.collider.gameObject.CompareTag("NPC") && hit.collider.gameObject.GetComponent<HealthBehaviour>() && Vector3.Angle(hit.collider.gameObject.transform.position - transform.position, hit.collider.gameObject.transform.forward) > 90)
            {
                hit.collider.gameObject.GetComponent<NPCBehaviour>().Lock();
            }
        }
    }

    public void BatAttack()
    {
        if (Physics.SphereCast(transform.position, radius, transform.forward, out RaycastHit hit, reach[5]))
        {
            print(true);
            if(hit.collider.gameObject.TryGetComponent<HealthBehaviour>(out HealthBehaviour hb) && hit.collider.gameObject.CompareTag("NPC"))
            {
                batAttackSpawned = Instantiate(batAttackObj, transform.position, Quaternion.identity);
                BatAttack b = batAttackSpawned.GetComponent<BatAttack>();
                b.startAttack(hit.collider.gameObject, bs);
            }
        }
    }

    IEnumerator AttackLoop()
    {
        while (true)
        {
            if (attack[0])
            {
                Punch();

                attack[0] = false;
                yield return new WaitForSeconds(cooldown[0]);
            }
            else if (attack[1])
            {
                Vector3 dir;

                if (type == 0)
                {
                    float acrossTime = Vector3.Distance(transform.position, target.transform.position + prb.velocity / projectileSpeed) / projectileSpeed;
                    dir = ((target.transform.position + prb.velocity * acrossTime + Vector3.up * acrossTime * Mathf.Sqrt(Physics.gravity.magnitude)) - transform.position);
                }
                else
                {
                    dir = Camera.main.transform.forward;
                }

                Shoot(dir);

                attack[1] = false;
                yield return new WaitForSeconds(cooldown[1]);
            }
            else if(attack[5])
            {
                BatAttack();

                attack[5] = false;
                yield return new WaitUntil(() => batAttackSpawned == null);
            }

            yield return null;
        }
    }
}
