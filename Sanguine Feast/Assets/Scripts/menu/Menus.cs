using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Menus : MonoBehaviour
{
    public GameObject mainScreen;
    public GameObject controlScreen;

    public void OnButtonStartGame(){
        SceneManager.LoadScene("Main Scene");
    }

    public void OnButtonHelp(){
        mainScreen.SetActive(false);
        controlScreen.SetActive(true);
    }

    public void OnButtonExitHelp(){
        mainScreen.SetActive(true);
        controlScreen.SetActive(false);
    }

    public void OnButtonExit(){
        Debug.Log("Exiting");
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
