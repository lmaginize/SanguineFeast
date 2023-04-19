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

    public void OnButtonRessurect(){
        if(BS.totalBlood >= 1000){
            BS.totalBlood = BS.totalBlood - 1000;
            BS.ressurectionUpgrade = false;
            ressurectmenu.SetActive(false);
        }
        
    }

    public void OnButtonDeny(){
        Menus.endBlood = BS.totalBlood;
        SceneManager.LoadScene("Lose Scene");
    }
}
