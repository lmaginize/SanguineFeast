using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Variables
    public bool night;
    public GameObject im;
    [SerializeField] private int seconds;
    [SerializeField] private float deltaSeconds;
    private const int MAX_TIME_FOR_DAY = 241;
    private bool isChangingCycle;
    public GameObject sunObject;
    public GameObject moonObject;
    public Slider dayNightCycleSlider;
    #endregion

    #region Start, Update
    void Start()
    {
        sunObject.transform.rotation = Quaternion.Euler(270f, 0f, 0f);
        night = true;
        Instantiate(im);
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
            }

            switch (seconds)
            {
                case (120):
                    StartCoroutine(ChangeCycle(135f));
                    night = false;
                    break;
                case (160):
                case (200):
                    StartCoroutine(ChangeCycle(45f));
                    night = false;
                    break;
                case (240):
                    StartCoroutine(ChangeCycle(135f));
                    night = true;
                    break;
            }
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
