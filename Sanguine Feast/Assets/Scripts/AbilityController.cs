using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityController : MonoBehaviour
{

    List<InputAction> abilities = new List<InputAction>();
    int numIndex = 0;

    public MovementBehaviour mb;
    public BatAbility ba;
    private PlayerControls pcsMB;

    // Start is called before the first frame update
    void Start()
    {
        pcsMB = mb.pcs;
        abilities.Add(pcsMB.Gameplay.Ability1);
        abilities.Add(pcsMB.Gameplay.Ability2);
        abilities.Add(pcsMB.Gameplay.Ability3);
        
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
                break;
            case ("VampireSpeed"):
                break;
            case ("Ressurection"):
                break;
            default:
                break;
        }
        abilities[numIndex].Enable();
        numIndex++;
        
    }
}