using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BloodSucking : MonoBehaviour
{
    public float maxSuckDistance = 1.0f;
    public int bloodGainAmount = 100;
    public int currentBlood = 5;
    public int totalBlood = 0;
    public TMP_Text promptText;
    public Button yesButton;
    public Button noButton;
    public TMP_Text currentBloodText;
    public TMP_Text totalBloodText;

    private GameObject player;
    private GameObject npc;
    private bool isPrompting = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentBloodText.text = "Blood: " + currentBlood.ToString();
        totalBloodText.text = "Total Blood: " + totalBlood.ToString();
        promptText.enabled = false;
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
    }
    /*
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxSuckDistance))
            {
                if (hit.transform.gameObject.name.Contains("NPC"))
                {
                    npc = hit.transform.gameObject;
                    if (!isPrompting)
                    {
                        Time.timeScale = 0f;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        promptText.enabled = true;
                        yesButton.gameObject.SetActive(true);
                        noButton.gameObject.SetActive(true);
                        isPrompting = true;
                    }
                }
            }
           
        }
        else
        {
            if (isPrompting)
            {
                promptText.enabled = false;
                yesButton.gameObject.SetActive(false);
                noButton.gameObject.SetActive(false);
                isPrompting = false;
            }
        }
    }
    */
    public void YesButtonClick()
    {
        Debug.Log("Yes button clicked!");
        currentBlood += bloodGainAmount;
        totalBlood += bloodGainAmount;
        currentBloodText.text = "Blood: " + currentBlood.ToString();
        totalBloodText.text = "Total Blood: " + totalBlood.ToString();
        GameObject.Destroy(npc);
        promptText.enabled = false;
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        isPrompting = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void NoButtonClick()
    {
        promptText.enabled = false;
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        isPrompting = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name.Contains("NPC"))
        {
            npc = collision.collider.gameObject;
            if (!isPrompting)
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                promptText.enabled = true;
                yesButton.gameObject.SetActive(true);
                noButton.gameObject.SetActive(true);
                isPrompting = true;
            }
        }
        else
        {
            if (isPrompting)
            {
                promptText.enabled = false;
                yesButton.gameObject.SetActive(false);
                noButton.gameObject.SetActive(false);
                isPrompting = false;
            }
        }
    }
    
}
