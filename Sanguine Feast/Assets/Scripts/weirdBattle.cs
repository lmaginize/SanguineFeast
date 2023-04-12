using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class weirdBattle : MonoBehaviour
{
    public float costPerUpgrade = 50f;
    public float winChance = .5f;
    public float fightWinChance = .2f;
    public float nightMultiplier = .25f;
    public float dayMultiplier = .05f;
    public float upgradeMultiplier = .05f;
    public float upgrades = 0f;
    public GameObject battleScreen;
    public TMP_Text fightChance;
    public TMP_Text runChance;
    public float bloodAmount;
    float winCheck = 0f;
    float fightwinCheck = 0f;


    GameController dayNight;

    // Start is called before the first frame update
    void Start()
    {
        dayNight = FindObjectOfType<GameController>();
        battleScreen.SetActive(false);
    }

    public void battleBegin(){
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        battleScreen.SetActive(true);
        if (dayNight.night == false)
        {
            fightChance.text = "Success: " + (fightWinChance - dayMultiplier + (upgrades * upgradeMultiplier / 2)) * 100 + "%";
            runChance.text = "Success: " + (winChance - dayMultiplier + (upgrades * upgradeMultiplier)) * 100 + "%";
        }
        else
        {
            fightChance.text = "Success: " + (fightWinChance + nightMultiplier + (upgrades * upgradeMultiplier / 2)) * 100 + "%";
            runChance.text = "Success: " + (winChance + nightMultiplier + (upgrades * upgradeMultiplier)) * 100 + "%";
        }
    }

    public void OnButtonUpgrade(){
        bloodAmount = GameObject.FindGameObjectWithTag("Player").GetComponent<BloodSucking>().currentBlood;
        if (bloodAmount >= 50){
            GameObject.FindGameObjectWithTag("Player").GetComponent<BloodSucking>().currentBlood -= 50;
            upgrades++;

            if (dayNight.night == false)
            {
                fightChance.text = "Success: " + (fightWinChance - dayMultiplier + (upgrades * upgradeMultiplier / 2)) * 100 + "%";
                runChance.text = "Success: " + (winChance - dayMultiplier + (upgrades * upgradeMultiplier)) * 100 + "%";
            }
            else
            {
                fightChance.text = "Success: " + (fightWinChance + nightMultiplier + (upgrades * upgradeMultiplier / 2)) * 100 + "%";
                runChance.text = "Success: " + (winChance + nightMultiplier + (upgrades * upgradeMultiplier)) * 100 + "%";
            }
        }
    }

    public void OnButtonFight(){
        fightwinCheck = Random.Range(0, 1);
        
        if(dayNight.night == false){
            fightWinChance = fightWinChance - dayMultiplier + (upgrades * upgradeMultiplier / 2);
        }
        else if (dayNight.night == true)
        {
            fightWinChance = fightWinChance + nightMultiplier + (upgrades * upgradeMultiplier / 2);
        }

        if(fightWinChance > fightwinCheck){
            Debug.Log("winner");
            battleScreen.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else{
            Debug.Log("loser");
            SceneManager.LoadScene("Lose Scene");
        }
        
    }

    public void OnButtonRun(){
        if(dayNight.night == true){
            winChance = winChance + nightMultiplier + (upgrades * upgradeMultiplier);
        }
        else if(dayNight.night == false){
            winChance = winChance - dayMultiplier + (upgrades * upgradeMultiplier);
        }
        winCheck = Random.Range(0, 1);
        if(winChance > winCheck){
            Debug.Log("winner");
            battleScreen.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else{
            Debug.Log("loser");
            SceneManager.LoadScene("Lose Scene");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
