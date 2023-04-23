using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttack : MonoBehaviour
{
    public ParticleSystem pS;
    [SerializeField] private float damagePerBat = .33f;
    [SerializeField] private float stunPerBat = .33f;

    //NEED TO ADD PARTICLE SYSTEM FORCE FIELD FROM CUBE PREFAB TO ENEMIES

    private void Awake()
    {
        pS.enableEmission = false;
    }


    /// <summary>
    /// Attacks enemy
    /// </summary>
    /// <param name="target">Target enemy</param>
    /// <returns></returns>
    public IEnumerator Attack(GameObject target)
    {
        ParticleSystemForceField pc = target.GetComponent<ParticleSystemForceField>();
        pc.enabled = true;
        pS.enableEmission = true;
        yield return new WaitForSeconds(5f);
        pS.enableEmission = false;
        yield return new WaitUntil(() => pS.particleCount == 0);
        pc.enabled = false;
    }
    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.TryGetComponent<HealthBehaviour>(out HealthBehaviour hb))
        {
            hb.ReceiveHit(damagePerBat, stunPerBat);
        }
    }
}
