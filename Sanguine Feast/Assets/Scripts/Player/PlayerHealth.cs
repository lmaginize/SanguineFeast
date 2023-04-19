using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool weak;
    public bool inShade;

    // Start is called before the first frame update
    void Start()
    {
        weak = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (weak == true)
        {

            transform.gameObject.GetComponent<BloodSucking>().currentBlood -= 3f * Time.deltaTime;

        }

    }

    /*void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Shade")
        {
            inShade = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag == "Shade")
        {
            inShade = false;
        }
    }*/
}
