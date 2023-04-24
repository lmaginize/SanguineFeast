using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySelectBehaviour : MonoBehaviour
{

    public GameObject abilitySelection;

    public string currentButton;

    public Button qButton;
    public Button eButton;
    public Button rButton;


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

    public void BatTransformButton()
    {

        if (currentButton == "Q")
        {

            qButton.GetComponentInChildren<Text>().text = "Bat Transform";

        }
        else if (currentButton == "E")
        {

            eButton.GetComponentInChildren<Text>().text = "Bat Transform";

        }
        else if (currentButton == "R")
        {

            rButton.GetComponentInChildren<Text>().text = "Bat Transform";

        }

        abilitySelection.SetActive(false);

    }

    public void ShadowStepButton()
    {

        if (currentButton == "Q")
        {

            qButton.GetComponentInChildren<Text>().text = "Shadow Step";

        }
        else if (currentButton == "E")
        {

            eButton.GetComponentInChildren<Text>().text = "Shadow Step";

        }
        else if (currentButton == "R")
        {

            rButton.GetComponentInChildren<Text>().text = "Shadow Step";

        }

        abilitySelection.SetActive(false);

    }

    public void HypnosButton()
    {

        if (currentButton == "Q")
        {

            qButton.GetComponentInChildren<Text>().text = "Hypnotism";

        }
        else if (currentButton == "E")
        {

            eButton.GetComponentInChildren<Text>().text = "Hypnotism";

        }
        else if (currentButton == "R")
        {

            rButton.GetComponentInChildren<Text>().text = "Hypnotism";

        }

        abilitySelection.SetActive(false);

    }

    public void TurnedButton()
    {

        if (currentButton == "Q")
        {

            qButton.GetComponentInChildren<Text>().text = "Turn NPC";

        }
        else if (currentButton == "E")
        {

            eButton.GetComponentInChildren<Text>().text = "Turn NPC";

        }
        else if (currentButton == "R")
        {

            rButton.GetComponentInChildren<Text>().text = "Turn NPC";

        }

        abilitySelection.SetActive(false);

    }

    public void VampiricSpeedButton()
    {

        if (currentButton == "Q")
        {

            qButton.GetComponentInChildren<Text>().text = "Vampiric Speed";

        }
        else if (currentButton == "E")
        {

            eButton.GetComponentInChildren<Text>().text = "Vampiric Speed";

        }
        else if (currentButton == "R")
        {

            rButton.GetComponentInChildren<Text>().text = "Vampiric Speed";

        }

        abilitySelection.SetActive(false);

    }

    public void ResurectionButton()
    {

        if (currentButton == "Q")
        {

            qButton.GetComponentInChildren<Text>().text = "Resurection";

        }
        else if (currentButton == "E")
        {

            eButton.GetComponentInChildren<Text>().text = "Resurection";

        }
        else if (currentButton == "R")
        {

            rButton.GetComponentInChildren<Text>().text = "Resurection";

        }

        abilitySelection.SetActive(false);

    }

    public void ShadowCreationButton()
    {

        if (currentButton == "Q")
        {

            qButton.GetComponentInChildren<Text>().text = "Shadow Creation";

        }
        else if (currentButton == "E")
        {

            eButton.GetComponentInChildren<Text>().text = "Shadow Creation";

        }
        else if (currentButton == "R")
        {

            rButton.GetComponentInChildren<Text>().text = "Shadow Creation";

        }

        abilitySelection.SetActive(false);

    }

}
