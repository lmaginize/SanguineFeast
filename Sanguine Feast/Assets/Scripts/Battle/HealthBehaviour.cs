using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBehaviour : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public GameObject healthBarObj;
    public Slider healthBar;
    public GameObject stunBarObj;
    public Slider stunBar;
    public GameObject canvas;
    public Transform players;
    public NPCBehaviour npcBeh;
    public PatrollerBehaviour pb;
    private ParticleSystemForceField psff;
    private PatrollerManager pm;

    private float stun;
    public float maxStun;
    public float stunLength;
    private float stunTime;

    public float alertRadius;

    private bool isPatroller = false;

    private void Awake()
    {
        canvas = transform.GetChild(0).gameObject;
        healthBarObj = transform.GetChild(0).GetChild(0).gameObject;
        healthBar = healthBarObj.GetComponent<Slider>();
        stunBarObj = transform.GetChild(0).GetChild(1).gameObject;
        stunBar = stunBarObj.GetComponent<Slider>();

        healthBar.maxValue = maxHealth;
        health = maxHealth;
        healthBar.value = health;
        stunBar.maxValue = maxStun;
        stunBar.value = 0;
        players = GameObject.Find("Player").transform;

        if (!name.Contains("Patroller"))
        {
            npcBeh = GetComponent<NPCBehaviour>();
            psff = GetComponent<ParticleSystemForceField>();
            psff.enabled = false;
        }
        else
        {
            pb = GetComponent<PatrollerBehaviour>();
            isPatroller = true;
            pm = GameObject.Find("PatrollerManager").GetComponent<PatrollerManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health != maxHealth)
        {
            healthBarObj.SetActive(true);
        }
        else
        {
            healthBarObj.SetActive(false);
        }

        if (stun != 0)
        {
            stunBarObj.SetActive(true);
        }
        else
        {
            stunBarObj.SetActive(false);
        }

        if (healthBarObj.activeInHierarchy || stunBarObj.activeInHierarchy)
        {
            canvas.transform.LookAt(players);
        }

        if (health <= 0)
        {
            if (isPatroller)
            {
                pm.InvokeOneDown(pm.patrollers.IndexOf(gameObject.GetComponent<PatrollerBehaviour>()));
            }

            Destroy(gameObject);
        }
    }

    public void ReceiveHit(float damage, float stun_)
    {
        if (stun < maxStun - stun_)
        {
            stun += stun_;
        }
        else
        {
            stun = maxStun;
            
            if (!name.Contains("Patroller"))
            {
                npcBeh.StunNPC();
            }
            else
            {
                pb.StunNPC();
            }
        }

        stunBar.value = stun;

        if (damage < health)
        {
            health -= damage;
        }
        else
        {
            health -= health;
        }

        healthBar.value = health;

        Collider[] arr = Physics.OverlapSphere(transform.position, alertRadius, LayerMask.GetMask("Patroller"));

        for (int x = 0; x < arr.Length; x++)
        {
            print("found");
            arr[x].gameObject.GetComponent<PatrollerBehaviour>().WokeUpAndChoseAnger();
        }
    }

    public void UnStun()
    {
        stun = 0;
    }
}
