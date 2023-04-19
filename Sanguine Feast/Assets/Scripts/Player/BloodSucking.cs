using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BloodSucking : MonoBehaviour
{
    public float maxSuckDistance = 2.0f;
    public int bloodGainAmount = 100;
    public float currentBlood = 5;
    public int totalBlood = 0;
    public int nightChance = 95;
    public int dayChance = 60;
    public TMP_Text promptText;
    public Button yesButton;
    public Button noButton;
    public TMP_Text currentBloodText;
    public TMP_Text totalBloodText;
    public TMP_Text successText;
    public bool ressurectionUpgrade = false;

    public PlayerControls pcs;
    private InputAction attack;
    public GameObject ressurectmenu;

    private GameController gc;
    private weirdBattle wb;

    private GameObject player;
    private GameObject npc;
    private bool isPrompting = false;

    private void Awake()
    {
        pcs = new PlayerControls();
        attack = pcs.Gameplay.Attack1;
    }

    private void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        wb = GameObject.Find("GameController").GetComponent<weirdBattle>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentBloodText.text = "Blood: " + (int)currentBlood;
        totalBloodText.text = "Total Blood: " + totalBlood.ToString();
        promptText.enabled = false;
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        attack.Enable();
        attack.performed += OnAttackPerformed;
    }
    private void OnDisable()
    {
        attack.Disable();
        attack.performed -= OnAttackPerformed;
    }

    public void Update()
    {
        currentBloodText.text = "Blood: " + (int)currentBlood;

        if (currentBlood < 0 && !ressurectionUpgrade)
        {
            Menus.endBlood = totalBlood;
            SceneManager.LoadScene("Lose Scene");
        }
        else{
            ressurectmenu.SetActive(true);
        }
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
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
                    if (gc.night)
                    {
                        successText.text = "Success: " + nightChance + "%";
                    }
                    else
                    {
                        successText.text = "Success: " + dayChance + "%";
                    }
                    isPrompting = true;

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

    public void YesButtonClick()
    {
        currentBlood = (int)currentBlood;
        currentBlood += bloodGainAmount;
        totalBlood += bloodGainAmount;
        currentBloodText.text = "Blood: " + currentBlood.ToString();
        totalBloodText.text = "Total Blood: " + totalBlood.ToString();
        Destroy(npc);
        promptText.enabled = false;
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        isPrompting = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (gc.night)
        {
            if (Random.Range(0, 101) >= nightChance)
            {
                wb.battleBegin();
            }
        }
        else
        {
            if (Random.Range(0, 101) >= dayChance)
            {
                wb.battleBegin();
            }
        }
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
    /*
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
    */
}
