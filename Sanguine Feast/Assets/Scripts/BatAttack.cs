using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttack : MonoBehaviour
{
    public ParticleSystem pS;
    [SerializeField] private int damagePerBat = 2;
    [SerializeField] private float stunPerBat = .33f;
    public BloodSucking playerBS;
    public GameObject target;

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
            hb.ReceiveHit(damagePerBat, stunPerBat);
            playerBS.currentBlood += damagePerBat;
            playerBS.totalBlood += damagePerBat;
        }
    }

    
}
