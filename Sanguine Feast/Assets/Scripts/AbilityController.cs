using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityController : MonoBehaviour
{

    public static List<InputAction> abilities = new List<InputAction>();
    int numIndex = 0;

    private MovementBehaviour mb;
    private BatAbility ba;
    private PlayerControls pcsMB;
    private BloodSucking bs;
    private Hypnotism hyp;

    // Start is called before the first frame update
    void Start()
    {
        //pcsMB = mb.pcs;
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        mb = g.GetComponent<MovementBehaviour>();
        ba = g.GetComponent<BatAbility>();
        bs = mb.bs;
        pcsMB = mb.pcs;
        hyp = g.GetComponent<Hypnotism>();
        print(abilities.Count);
        if (abilities.Count < 1)
        {
            abilities.Add(pcsMB.Gameplay.Ability1);
            abilities.Add(pcsMB.Gameplay.Ability2);
            abilities.Add(pcsMB.Gameplay.Ability3);
        }
        mb.gc.enabled = false;
        g.active = false;
    }

    private void Update()
    {
        if(Cursor.lockState == CursorLockMode.Locked || Cursor.visible == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void SetKeyBind(string name, string button)
    {
        switch(button)
        {
            case ("Q"):
                numIndex = 0;
                break;
            case ("E"):
                numIndex = 1;
                break;
            case ("R"):
                numIndex = 2;
                break;
        }
        switch (name)
        {
            case ("Bat Transformation"):
                abilities[numIndex].performed -= mb.ShadowStep;
                abilities[numIndex].performed -= mb.ShadowCreation;
                abilities[numIndex].performed -= hyp.OnAbilityPerformed;
                abilities[numIndex].performed -= mb.DefaultAction;

                abilities[numIndex].performed += ba.ShapeShift;
                break;
            case ("Shadow Step"):
                abilities[numIndex].performed -= ba.ShapeShift;
                abilities[numIndex].performed -= mb.ShadowCreation;
                abilities[numIndex].performed -= hyp.OnAbilityPerformed;
                abilities[numIndex].performed -= mb.DefaultAction;

                abilities[numIndex].performed += mb.ShadowStep;
                break;
            case ("Shadow Creation"):

                abilities[numIndex].performed -= ba.ShapeShift;
                abilities[numIndex].performed -= mb.ShadowStep;
                abilities[numIndex].performed -= hyp.OnAbilityPerformed;
                abilities[numIndex].performed -= mb.DefaultAction;

                abilities[numIndex].performed += mb.ShadowCreation;
                break;
            case ("Hypnosis"):

                abilities[numIndex].performed -= ba.ShapeShift;
                abilities[numIndex].performed -= mb.ShadowCreation;
                abilities[numIndex].performed -= mb.ShadowStep;
                abilities[numIndex].performed -= mb.DefaultAction;

                abilities[numIndex].performed += hyp.OnAbilityPerformed;
                break;
            case ("Vampiric Speed"):
                abilities[numIndex].performed -= ba.ShapeShift;
                abilities[numIndex].performed -= mb.ShadowCreation;
                abilities[numIndex].performed -= hyp.OnAbilityPerformed;
                abilities[numIndex].performed -= mb.ShadowStep;
                abilities[numIndex].performed -= mb.DefaultAction;

                break;
            case ("Resurection"):
                abilities[numIndex].performed -= ba.ShapeShift;
                abilities[numIndex].performed -= mb.ShadowCreation;
                abilities[numIndex].performed -= hyp.OnAbilityPerformed;
                abilities[numIndex].performed -= mb.ShadowStep;

                bs.ressurectionUpgrade = true;
                abilities[numIndex].performed += mb.DefaultAction;
                break;
            case ("Turn NPC"):
                abilities[numIndex].performed -= ba.ShapeShift;
                abilities[numIndex].performed -= mb.ShadowCreation;
                abilities[numIndex].performed -= hyp.OnAbilityPerformed;
                abilities[numIndex].performed -= mb.ShadowStep;
                abilities[numIndex].performed -= mb.DefaultAction;


                break;
            default:
                break;
        }

        abilities[numIndex].Enable();
        
    }
}
