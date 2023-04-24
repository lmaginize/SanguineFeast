using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityController : MonoBehaviour
{

    public static List<InputAction> abilities = new List<InputAction>();
    int numIndex = 0;

    public MovementBehaviour mb;
    public BatAbility ba;
    private PlayerControls pcsMB;
    public BloodSucking bs;
    public Hypnotism hyp;

    // Start is called before the first frame update
    void Start()
    {
        //pcsMB = mb.pcs;
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        mb = g.GetComponent<MovementBehaviour>();
        ba = g.GetComponent<BatAbility>();
        bs = mb.bs;
        pcsMB = mb.pcs;
        //hyp = g.GetComponent<Hypnotism>();
        if (abilities.Count < 1)
        {
            abilities.Add(pcsMB.Gameplay.Ability1);
            abilities.Add(pcsMB.Gameplay.Ability2);
            abilities.Add(pcsMB.Gameplay.Ability3);
        }
        SetKeyBind("shadowTp");
        SetKeyBind("ShadowCreate");
        SetKeyBind("batFly");
    }

    public void SetKeyBind(string name)
    {
        switch (name)
        {
            case ("batFly"):
                abilities[numIndex].performed += ba.ShapeShift;
                break;
            case ("shadowTp"):
                abilities[numIndex].performed += mb.ShadowStep;
                break;
            case ("ShadowCreate"):
                abilities[numIndex].performed += mb.ShadowCreation;
                break;
            case ("Hypno"):
                abilities[numIndex].performed += hyp.OnAbilityPerformed;
                break;
            case ("VampireSpeed"):
                break;
            case ("Ressurection"):
                bs.ressurectionUpgrade = true;
                break;
            case ("Turning"):
                break;
            default:
                break;
        }
        abilities[numIndex].Enable();
        numIndex++;
        
    }
}
