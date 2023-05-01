using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class AbilityController : MonoBehaviour
{

    public static List<InputAction> abilities = new List<InputAction>();
    int numIndex = 0;

    public GameObject player;
    private MovementBehaviour mb;
    private BatAbility ba;
    private PlayerControls pcsMB;
    private BloodSucking bs;
    private GameController gc;
    [Tooltip("Ability text under the day night cycle ui")]
    public TMP_Text text;
    public static List<string> abilityText = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        //pcsMB = mb.pcs;
        player = GameObject.FindGameObjectWithTag("Player");
        mb = player.GetComponent<MovementBehaviour>();
        gc = mb.gc;
        ba = player.GetComponent<BatAbility>();
        bs = mb.bs;
        pcsMB = mb.pcs;
        print(abilities.Count);
        if (abilities.Count < 1)
        {
            abilities.Add(pcsMB.Gameplay.Ability1);
            abilities.Add(pcsMB.Gameplay.Ability2);
            abilities.Add(pcsMB.Gameplay.Ability3);
            abilityText.Add("Q");
            abilityText.Add("E");
            abilityText.Add("R");
        }
        PlayerActivation(false);
        SetAbilityTextUI();
        //superSpeedEnabler
    }

    //private void Update()
    //{
    //    if(Cursor.lockState == CursorLockMode.Locked || Cursor.visible == false)
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //        Cursor.visible = true;
    //    }
    //}

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

        abilityText[numIndex] = button;

        switch (name)
        {
            case ("Bat Transformation"):
                abilities[numIndex].performed -= mb.ShadowStep;
                abilities[numIndex].performed -= mb.ShadowCreation;
                abilities[numIndex].performed -= mb.Hypnotism;
                abilities[numIndex].performed -= mb.Turning;
                abilities[numIndex].performed -= mb.DefaultAction;
                abilities[numIndex].performed -= mb.SuperSpeedEnabler;

                abilities[numIndex].performed += ba.ShapeShift;
                break;
            case ("Shadow Step"):
                abilities[numIndex].performed -= ba.ShapeShift;
                abilities[numIndex].performed -= mb.ShadowCreation;
                abilities[numIndex].performed -= mb.Hypnotism;
                abilities[numIndex].performed -= mb.Turning;
                abilities[numIndex].performed -= mb.DefaultAction;
                abilities[numIndex].performed -= mb.SuperSpeedEnabler;

                abilities[numIndex].performed += mb.ShadowStep;
                break;
            case ("Shadow Creation"):

                abilities[numIndex].performed -= ba.ShapeShift;
                abilities[numIndex].performed -= mb.ShadowStep;
                abilities[numIndex].performed -= mb.Hypnotism;
                abilities[numIndex].performed -= mb.Turning;
                abilities[numIndex].performed -= mb.DefaultAction;
                abilities[numIndex].performed -= mb.SuperSpeedEnabler;

                abilities[numIndex].performed += mb.ShadowCreation;
                break;
            case ("Hypnosis"):

                abilities[numIndex].performed -= ba.ShapeShift;
                abilities[numIndex].performed -= mb.ShadowCreation;
                abilities[numIndex].performed -= mb.ShadowStep;
                abilities[numIndex].performed -= mb.DefaultAction;
                abilities[numIndex].performed -= mb.Turning;
                abilities[numIndex].performed -= mb.SuperSpeedEnabler;

                abilities[numIndex].performed += mb.Hypnotism;
                break;
            case ("Vampiric Speed"):
                abilities[numIndex].performed -= ba.ShapeShift;
                abilities[numIndex].performed -= mb.ShadowCreation;
                abilities[numIndex].performed -= mb.Hypnotism;
                abilities[numIndex].performed -= mb.Turning;
                abilities[numIndex].performed -= mb.ShadowStep;
                abilities[numIndex].performed -= mb.DefaultAction;

                abilities[numIndex].performed += mb.SuperSpeedEnabler;

                break;
            case ("Resurection"):
                abilities[numIndex].performed -= ba.ShapeShift;
                abilities[numIndex].performed -= mb.ShadowCreation;
                abilities[numIndex].performed -= mb.Hypnotism;
                abilities[numIndex].performed -= mb.Turning;
                abilities[numIndex].performed -= mb.ShadowStep;
                abilities[numIndex].performed -= mb.SuperSpeedEnabler;

                bs.ressurectionUpgrade = true;
                abilities[numIndex].performed += mb.DefaultAction;

                break;
            case ("Turn NPC"):
                abilities[numIndex].performed -= ba.ShapeShift;
                abilities[numIndex].performed -= mb.ShadowCreation;
                abilities[numIndex].performed -= mb.Hypnotism;
                abilities[numIndex].performed -= mb.Turning;
                abilities[numIndex].performed -= mb.ShadowStep;
                abilities[numIndex].performed -= mb.DefaultAction;
                abilities[numIndex].performed -= mb.SuperSpeedEnabler;

                abilities[numIndex].performed += mb.Turning;

                break;

            default:
                break;
        }
        
        if(name != "Resurection")
        {
            if (name == "Shadow Step")
            {
                abilityText[numIndex] = name + ": " + abilityText[numIndex] + " - " + 15 + " blood";
            }
            if (name == "Shadow Creation")
            {
                abilityText[numIndex] = name + ": " + abilityText[numIndex] + " - " + 5 + " blood";
            }
            if (name == "Bat Transformation")
            {
                abilityText[numIndex] = name + ": " + abilityText[numIndex] + " - " + 25 + " blood";
            }
            if (name == "Vampiric Speed")
            {
                abilityText[numIndex] = name + ": " + abilityText[numIndex] + " - " + 5 + " blood per second";
            }
            if (name == "Turn NPC")
            {
                abilityText[numIndex] = name + ": " + abilityText[numIndex] + " - " + 30 + " blood";
            }
            if (name == "Hypnosis")
            {
                abilityText[numIndex] = name + ": " + abilityText[numIndex] + " - " + 10 + " blood";
            }
            
        }

            //abilityText[numIndex] = name + ": " + abilityText[numIndex] + " - " + "X" + " blood";
        abilities[numIndex].Enable();
        
    }

    public void SetAbilityTextUI()
    {
        text.text = "";
        foreach (string t in abilityText)
        {
            if (t.Equals("Q") || t.Equals("E") || t.Equals("R"))
            {

            }
            else
            {
                text.text += t + "\n\n";
            }
        }
    }

    public void PlayerActivation(bool enable)
    {
        gc.enabled = enable;
        mb.enabled = enable;
        ba.enabled = enable;
        bs.enabled = enable;
    }
}
