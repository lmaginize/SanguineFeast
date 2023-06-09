using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Menus : MonoBehaviour
{
    public static int endBlood;

    public GameObject mainScreen;
    public GameObject controlScreen;

    public TMP_Text totalblood;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (SceneManager.GetActiveScene().name == "Lose Scene" || SceneManager.GetActiveScene().name == "Win Scene")
        {

            totalblood.text = endBlood.ToString();

        }
    }

    public void OnButtonStartGame(){
        SceneManager.LoadScene("JasonMainScene");
    }
    public void OnButtonMainMenu(){
        SceneManager.LoadScene("Main Menu");
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
