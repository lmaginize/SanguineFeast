using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    //2 minutes per 12 hours,
    [SerializeField] private int seconds;
    [SerializeField] private float deltaSeconds;
    private const int MAX_TIME_FOR_DAY = 240;
    //private List<int> timeIntervalChangePointsOfDay = new List<int> { 0, 40, 80, 120};
    private bool rotated;
    private bool isChangingCycle;
    public GameObject sunObject;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(dayNight());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChangingCycle)
        {
            deltaSeconds += Time.deltaTime;
            seconds = (int)deltaSeconds;
        }
        if (seconds >= MAX_TIME_FOR_DAY)
        {
            deltaSeconds -= 240;
        }

        if (!rotated)
            switch (seconds)
            {
                case (0):
                case (40):
                case (80):
                    StartCoroutine(ChangeCycle(45f));
                    rotated = true;
                    break;
                case (120):
                    StartCoroutine(ChangeCycle(135f));
                    rotated = true;
                    break;
            }
        else
        {
            switch (seconds)
            {
                case (1):
                case (41):
                case (81):
                case (121):
                    rotated = false;
                    break;
                default:
                    break;
            }
        }


    }

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
        isChangingCycle = false;
        yield return null;
    }

    /*private IEnumerator dayNight()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            sunObject.transform.Rotate(new Vector3(2.0f/3.0f, 0f, 0f));
        }
    }*/
}
