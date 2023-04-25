using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySelectBehaviour : MonoBehaviour
{

    public AbilityController ab;
    public GameObject abilitySelection;

    public string currentButton;

    public Button qButton;
    public Button eButton;
    public Button rButton;

    

    public void Continue()
    {
        print("would start game");
        ab.player.SetActive(true);
        gameObject.SetActive(false);
        ab.PlayerActivation(true);
        ab.SetAbilityTextUI();
    }

    public void QButton()
    {
        if (abilitySelection.activeInHierarchy == false)
        {
            abilitySelection.SetActive(true);
        }
        else if (abilitySelection.activeInHierarchy == true && currentButton == "Q")
        {
            abilitySelection.SetActive(false);
        }

        currentButton = "Q";
    }

    public void EButton()
    {
        if (abilitySelection.activeInHierarchy == false)
        {
            abilitySelection.SetActive(true);
        }
        else if (abilitySelection.activeInHierarchy == true && currentButton == "E")
        {
            abilitySelection.SetActive(false);
        }

        currentButton = "E";
    }

    public void RButton()
    {
        if (abilitySelection.activeInHierarchy == false)
        {
            abilitySelection.SetActive(true);
        }
        else if (abilitySelection.activeInHierarchy == true && currentButton == "R")
        {
            abilitySelection.SetActive(false);
        }

        currentButton = "R";
    }

    /*
    public void BatTransformButton()
    {

        if (currentButton == "Q")
        {

            qButton.GetComponentInChildren<TMP_Text>().text = "Bat Transform";

        }
        else if (currentButton == "E")
        {

            eButton.GetComponentInChildren<TMP_Text>().text = "Bat Transform";

        }
        else if (currentButton == "R")
        {

            rButton.GetComponentInChildren<TMP_Text>().text = "Bat Transform";

        }
        ab.SetKeyBind("Bat Transform", currentButton);
        abilitySelection.SetActive(false);

    }

    public void ShadowStepButton()
    {

        if (currentButton == "Q")
        {

            qButton.GetComponentInChildren<TMP_Text>().text = "Shadow Step";

        }
        else if (currentButton == "E")
        {

            eButton.GetComponentInChildren<TMP_Text>().text = "Shadow Step";

        }
        else if (currentButton == "R")
        {

            rButton.GetComponentInChildren<TMP_Text>().text = "Shadow Step";

        }
        ab.SetKeyBind("Shadow Step", currentButton);
        abilitySelection.SetActive(false);

    }

    public void HypnosButton()
    {

        if (currentButton == "Q")
        {

            qButton.GetComponentInChildren<TMP_Text>().text = "Hypnotism";

        }
        else if (currentButton == "E")
        {

            eButton.GetComponentInChildren<TMP_Text>().text = "Hypnotism";

        }
        else if (currentButton == "R")
        {

            rButton.GetComponentInChildren<TMP_Text>().text = "Hypnotism";

        }
        ab.SetKeyBind("Hypnotism", currentButton);
        abilitySelection.SetActive(false);

    }

    public void TurnedButton()
    {

        if (currentButton == "Q")
        {

            qButton.GetComponentInChildren<TMP_Text>().text = "Turn NPC";

        }
        else if (currentButton == "E")
        {

            eButton.GetComponentInChildren<TMP_Text>().text = "Turn NPC";

        }
        else if (currentButton == "R")
        {

            rButton.GetComponentInChildren<TMP_Text>().text = "Turn NPC";

        }
        ab.SetKeyBind("Turn NPC", currentButton);
        abilitySelection.SetActive(false);

    }

    public void VampiricSpeedButton()
    {

        if (currentButton == "Q")
        {

            qButton.GetComponentInChildren<TMP_Text>().text = "Vampiric Speed";

        }
        else if (currentButton == "E")
        {

            eButton.GetComponentInChildren<TMP_Text>().text = "Vampiric Speed";

        }
        else if (currentButton == "R")
        {

            rButton.GetComponentInChildren<TMP_Text>().text = "Vampiric Speed";

        }
        ab.SetKeyBind("Vampiric Speed", currentButton);
        abilitySelection.SetActive(false);

    }

    public void ResurectionButton()
    {

        if (currentButton == "Q")
        { 
            qButton.GetComponentInChildren<TMP_Text>().text = "Resurection";

        }
        else if (currentButton == "E")
        {

            eButton.GetComponentInChildren<TMP_Text>().text = "Resurection";

        }
        else if (currentButton == "R")
        {

            rButton.GetComponentInChildren<TMP_Text>().text = "Resurection";

        }
        ab.SetKeyBind("Resurection", currentButton);
        abilitySelection.SetActive(false);

    }

    public void ShadowCreationButton()
    {
        
        if (currentButton == "Q")
        {

            qButton.GetComponentInChildren<TMP_Text>().text = "Shadow Creation";

        }
        else if (currentButton == "E")
        {

            eButton.GetComponentInChildren<TMP_Text>().text = "Shadow Creation";

        }
        else if (currentButton == "R")
        {

            rButton.GetComponentInChildren<TMP_Text>().text = "Shadow Creation";

        }
        ab.SetKeyBind("Shadow Creation", currentButton);
        abilitySelection.SetActive(false);

    }
    */

    public void Button(Button g)
    {
        ButtonSetup(g);
        
        if (currentButton == "Q")
        {
            qButton.GetComponentInChildren<TMP_Text>().text = g.GetComponentInChildren<TMP_Text>().text;
            ab.SetKeyBind(g.GetComponentInChildren<TMP_Text>().text, currentButton);
        }
        else if (currentButton == "E")
        {
            eButton.GetComponentInChildren<TMP_Text>().text = g.GetComponentInChildren<TMP_Text>().text;
            ab.SetKeyBind(g.GetComponentInChildren<TMP_Text>().text, currentButton);
        }
        else if (currentButton == "R")
        {
            rButton.GetComponentInChildren<TMP_Text>().text = g.GetComponentInChildren<TMP_Text>().text;
            ab.SetKeyBind(g.GetComponentInChildren<TMP_Text>().text, currentButton);
        }

        abilitySelection.SetActive(false);

    }
    
    private void ButtonSetup(Button g)
    {
        if(g.GetComponentInChildren<TMP_Text>().text == rButton.GetComponentInChildren<TMP_Text>().text && g != rButton)
        {
            rButton.GetComponentInChildren<TMP_Text>().text = "NONE";
        }
        else if(g.GetComponentInChildren<TMP_Text>().text == eButton.GetComponentInChildren<TMP_Text>().text && g != eButton)
        {
            eButton.GetComponentInChildren<TMP_Text>().text = "NONE";
        }
        else if(g.GetComponentInChildren<TMP_Text>().text == qButton.GetComponentInChildren<TMP_Text>().text && g != qButton)
        {
            qButton.GetComponentInChildren<TMP_Text>().text = "NONE";
        }
    }
}
