using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class weirdBattle : MonoBehaviour
{
    public float costPerUpgrade = 50f;
    public float winChance = .5f;
    public float fightWinChance = .2f;
    public float nightMultiplier = .35f;
    public float dayMultiplier = .05f;
    public float upgradeMultiplier = .05f;
    public float upgrades = 0f;
    public GameObject battleScreen;
    public float bloodAmount = 100f;
    float winCheck = 0f;
    float fightwinCheck = 0f;


    DayNightCycle dayNight;

    // Start is called before the first frame update
    void Start()
    {
        dayNight = FindObjectOfType<DayNightCycle>();
        battleScreen.SetActive(false);
    }

    public void battleBegin(){
        battleScreen.SetActive(true);
    }

    public void OnButtonUpgrade(){
        if(bloodAmount >= 50){
            bloodAmount = bloodAmount - 50;
            upgrades++;
        }
    }

    public void OnButtonFight(){
        fightwinCheck = Random.Range(0, 1);
        
        if(dayNight.isNight == false){
            fightWinChance = fightWinChance - dayMultiplier;
        }
        if(fightWinChance > fightwinCheck){
            Debug.Log("winner");
            battleScreen.SetActive(false);
        }
        else{
            Debug.Log("loser");
            SceneManager.LoadScene("Lose Scene");
        }
        
    }

    public void OnButtonRun(){
        if(upgrades > 0){
            upgradeMultiplier = upgradeMultiplier * upgrades;
        }
        if(dayNight.isNight == true){
            winChance = winChance + nightMultiplier;
        }
        else if(dayNight.isNight == false){
            winChance = winChance - dayMultiplier;
        }
        winCheck = Random.Range(0, 1);
        if(winChance > winCheck){
            Debug.Log("winner");
            battleScreen.SetActive(false);
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
