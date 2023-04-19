using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Variables
    public bool night;
    public int numOfNights = 1;
    [SerializeField] private int seconds;
    [SerializeField] private float deltaSeconds;
    private const int MAX_TIME_FOR_DAY = 241;
    private bool isChangingCycle;
    public GameObject sunObject;
    public GameObject moonObject;
    public Slider dayNightCycleSlider;

    //public GameObject morningShade;
    //public GameObject noonShade;
    //public GameObject eveningShade;

    #endregion

    #region Start, Update
    void Start()
    {
        sunObject.transform.rotation = Quaternion.Euler(270f, 0f, 0f);
        night = true;

        //morningShade.SetActive(false);
        //noonShade.SetActive(false);
        //eveningShade.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        if (!isChangingCycle)
        {
            deltaSeconds += Time.deltaTime;
            seconds = (int)deltaSeconds;

            if(dayNightCycleSlider != null)
                dayNightCycleSlider.value = seconds;

            if (seconds >= MAX_TIME_FOR_DAY)
            {
                deltaSeconds -= 240;
                numOfNights++;
            }

            switch (seconds)
            {
                case (120):
                    StartCoroutine(ChangeCycle(135f));
                    night = false;
                    //morningShade.SetActive(true);
                    break;
                case (160):
                    StartCoroutine(ChangeCycle(45f));
                    night = false;
                    //noonShade.SetActive(true);
                    //morningShade.SetActive(false);
                    break;
                case (200):
                    StartCoroutine(ChangeCycle(45f));
                    night = false;
                    //eveningShade.SetActive(true);
                    //noonShade.SetActive(false);
                    break;
                case (240):
                    StartCoroutine(ChangeCycle(135f));
                    night = true;
                    //morningShade.SetActive(false);
                    //noonShade.SetActive(false);
                    //eveningShade.SetActive(false);
                    break;
            }
        }

        if (numOfNights == 3)
        {
            Menus.endBlood = FindObjectOfType<BloodSucking>().gameObject.GetComponent<BloodSucking>().totalBlood;
            SceneManager.LoadScene("Win Scene");
        }

        /*if (isNight && !moonObject.activeInHierarchy)
        {
            moonObject.SetActive(true);
        }
        else if (!isNight && moonObject.activeInHierarchy)
        {
            moonObject.SetActive(false);
        }*/


    }
    #endregion

    #region DayNightCycle
    private IEnumerator ChangeCycle(float amountRotation)
    {
        //takes 5 seconds
        isChangingCycle = true;
        float totalRotation = 0;
        while (totalRotation <= amountRotation)
        {
            sunObject.transform.Rotate(new Vector3(amountRotation / 50f, 0f, 0f));
            yield return new WaitForSeconds(.1f);
            totalRotation += amountRotation / 50f;
        }
        yield return new WaitForSeconds(1);
        deltaSeconds += 1;
        isChangingCycle = false;
    }
    #endregion
}
