using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ressurectionMenu : MonoBehaviour
{
    private BloodSucking BS;
    public GameObject ressurectmenu;

    private void Start(){
        BS = GameObject.Find("Player").GetComponent<BloodSucking>();
    }

    public void onRessurectBegin(){
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ressurectmenu.SetActive(true);
    }

    public void OnButtonRessurect(){
        if(BS.totalBlood >= 1000){
            BS.totalBlood = BS.totalBlood - 1000;
            BS.ressurectionUpgrade = false;
            ressurectmenu.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }

    public void OnButtonDeny(){
        Menus.endBlood = BS.totalBlood;
        SceneManager.LoadScene("Lose Scene");
    }
}
