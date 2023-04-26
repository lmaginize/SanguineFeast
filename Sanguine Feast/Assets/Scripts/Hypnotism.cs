using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Hypnotism : MonoBehaviour
{

    public int bloodCost;
    public int maxHypnotismDistance;
    public float timeHypnotised;

    public PlayerControls pcs;
    private InputAction ability;

    public void Awake()
    {
        pcs = new PlayerControls();
        //ability = pcs.Gameplay.Hypnotism;
    }

    //private void OnEnable()
    //{
    //    ability.Enable();
    //    ability.performed += OnAbilityPerformed;
    //}
    //private void OnDisable()
    //{
    //    ability.Disable();
    //    ability.performed -= OnAbilityPerformed;
    //}

    public void OnAbilityPerformed(InputAction.CallbackContext context)
    {

        GetComponent<BloodSucking>().currentBlood -= bloodCost;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxHypnotismDistance))
        {
            if (hit.transform.gameObject.name.Contains("NPC"))
            {
                Debug.Log("hypno");
                //best to put this as a bool on a script on the npcs
                //this way all the nav mesh stuff could be all on one script
                hit.transform.gameObject.GetComponent<NPCBehaviour>().isHypnotised = true;
                StartCoroutine(Unhypnotised(hit.transform.gameObject.GetComponent<NPCBehaviour>()));
            }
        }
    }

    public IEnumerator Unhypnotised(NPCBehaviour npc)
    {
        yield return new WaitForSeconds(timeHypnotised);
        yield return npc.isHypnotised = false;
        StopAllCoroutines();
    }
}
