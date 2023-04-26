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

    private float stun;
    public float maxStun;
    public float stunLength;
    private float stunTime;

    private void Awake()
    {
        healthBarObj = transform.GetChild(0).GetChild(0).gameObject;
        healthBar = healthBarObj.GetComponent<Slider>();
        stunBarObj = transform.GetChild(0).GetChild(1).gameObject;
        stunBar = stunBarObj.GetComponent<Slider>();
        npcBeh = GetComponent<NPCBehaviour>();

        healthBar.maxValue = maxHealth;
        health = maxHealth;
        healthBar.value = health;
        stunBar.maxValue = maxStun;
        stunBar.value = 0;
        players = GameObject.Find("Player").transform;
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
            Destroy(gameObject);
        }
    }

    public void ReceiveHit(float damage, float stun_)
    {
        print(stun + " " + (maxStun - stun_));
        if (stun < maxStun - stun_)
        {
            print("in");
            stun += stun_;
        }
        else
        {
            print("stunned");
            stun = maxStun;
            npcBeh.StunNPC();
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
    }

    public void UnStun()
    {
        stun = 0;
    }
}
