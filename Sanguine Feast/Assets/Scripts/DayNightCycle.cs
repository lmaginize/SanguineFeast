using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    //2 minutes per 12 hours,
    [SerializeField] private int seconds;
    [SerializeField] private float deltaSeconds;
    private const int MAX_TIME_FOR_DAY = 241;
    private bool isChangingCycle;
    public GameObject sunObject;
    public GameObject moonObject;

    public bool isNight;
    // Start is called before the first frame update
    void Start()
    {
        sunObject.transform.rotation = Quaternion.Euler(270f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChangingCycle)
        {
            deltaSeconds += Time.deltaTime;
            seconds = (int)deltaSeconds;

            switch (seconds)
            {
                case (120):
                case (240):
                    StartCoroutine(ChangeCycle(135f));
                    isNight = true;
                    break;
                case (160):
                case (200):
                    StartCoroutine(ChangeCycle(45f));
                    isNight = false;
                    break;
            }
        }

        if (seconds >= MAX_TIME_FOR_DAY)
        {
            deltaSeconds -= 240;
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
        yield return new WaitForSeconds(1);
        deltaSeconds += 1;
        isChangingCycle = false;
    }

}
