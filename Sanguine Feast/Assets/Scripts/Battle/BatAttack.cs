using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttack : MonoBehaviour
{
    public ParticleSystem pS;
     private int damagePerBat = 1;
     private float stunPerBat = 5;
    public BloodSucking playerBS;
    public GameObject target;
    private bool attacking;

    //NEED TO ADD PARTICLE SYSTEM FORCE FIELD FROM CUBE PREFAB TO ENEMIES

    private void Awake()
    {
        pS.enableEmission = false;
    }

    private void Update()
    {
        if (target == null)
            Destroy(gameObject);
    }


    /// <summary>
    /// Attacks enemy
    /// </summary>
    /// <param name="target">Target enemy</param>
    /// <returns></returns>
    private IEnumerator Attack()
    {
        ParticleSystemForceField pc = target.GetComponent<ParticleSystemForceField>();
        pc.enabled = true;
        pS.enableEmission = true;
        yield return new WaitForSeconds(5f);
        pS.enableEmission = false;
        yield return new WaitUntil(() => pS.particleCount == 0);
        pc.enabled = false;
        Destroy(gameObject);
    }

    public void startAttack(GameObject enemyTarget, BloodSucking playerBlood)
    {
        target = enemyTarget;
        playerBS = playerBlood;
        StartCoroutine(Attack());
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.TryGetComponent<HealthBehaviour>(out HealthBehaviour hb))
        {
            if (!attacking)
                StartCoroutine(damageOverTime(hb));
        }
    }

    IEnumerator damageOverTime(HealthBehaviour hb)
    {
        attacking = true;
        while(true)
        {
            yield return new WaitForSeconds(.5f);
            hb.ReceiveHit(damagePerBat, stunPerBat);
            playerBS.currentBlood += damagePerBat;
        }
    }

    
}
