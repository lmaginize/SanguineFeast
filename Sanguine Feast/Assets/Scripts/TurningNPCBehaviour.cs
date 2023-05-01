using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurningNPCBehaviour : MonoBehaviour
{
    public int bloodCost;
    public int maxTurningDistance;
    public float timeTurning;

    public PlayerControls pcs;
    private InputAction ability;

    public void Awake()
    {
        pcs = new PlayerControls();
        //ability = pcs.Gameplay.Turning;
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

    private void OnAbilityPerformed(InputAction.CallbackContext context)
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxTurningDistance))
        {
            if (hit.transform.gameObject.name.Contains("NPC") && hit.transform.gameObject.TryGetComponent<NPCBehaviour>(out NPCBehaviour npc))
            {
                //best to put this as a bool on a script on the npcs
                //this way all the nav mesh stuff could be all on one script
                GetComponent<BloodSucking>().currentBlood -= bloodCost;
                npc.isTurned = true;
                npc.StartCoroutine(npc.Turned());
                StartCoroutine(UnTurn(npc));
            }
        }
    }

    public IEnumerator UnTurn(NPCBehaviour npc)
    {
        yield return new WaitForSeconds(timeTurning);
        yield return npc.isTurned = false;
        StopAllCoroutines();
    }
}
