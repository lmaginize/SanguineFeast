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

    public bool isStunned;
    private float stun;
    public float maxStun;
    public float stunLength;
    private float stunTime;

    // Start is called before the first frame update
    void Start()
    {
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
        else if (stun >= maxStun)
        {
            stunTime += Time.deltaTime;

            if (stunTime > stunLength)
            {
                stun = 0;
                isStunned = false;
            }
        }
    }

    public void ReceiveHit(float damage, float stun_)
    {
        if (stun < maxStun - stun_)
        {
            stun += stun;
        }
        else
        {
            stun = maxStun;
            isStunned = true;
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
}
