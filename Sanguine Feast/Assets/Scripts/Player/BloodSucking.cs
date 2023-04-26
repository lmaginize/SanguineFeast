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
   
    public TMP_Text currentBloodText;
    public TMP_Text totalBloodText;
    public TMP_Text successText;
    public bool ressurectionUpgrade = false;

    public PlayerControls pcs;
    private InputAction attack;
    public GameObject ressurectmenu;

    private GameController gc;
    private weirdBattle wb;
    private HealthBehaviour hb;
    private PatrollerManager pm;

    private GameObject player;
    private GameObject npc;
    private bool isSucking = false;

    private void Awake()
    {
        pcs = new PlayerControls();
        attack = pcs.Gameplay.Attack1;

        pm = GameObject.Find("PatrollerManager").GetComponent<PatrollerManager>();
    }

    private void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        wb = GameObject.Find("GameController").GetComponent<weirdBattle>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (currentBloodText != null)
        {
            currentBloodText.text = "Blood: " + (int)currentBlood;
            totalBloodText.text = "Total Blood: " + totalBlood.ToString();
        }
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
        if (currentBloodText != null)
        {
            currentBloodText.text = "Blood: " + (int)currentBlood;


            if (currentBlood < 0 && !ressurectionUpgrade)
            {
                Menus.endBlood = totalBlood;
                SceneManager.LoadScene("Lose Scene");
            }
            else
            {
                ressurectmenu.SetActive(true);
            }
        }
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxSuckDistance))
        {
            if (hit.collider.transform.gameObject.name.Contains("NPC") && hit.collider.gameObject.GetComponent<NPCBehaviour>().isStunned)
            {
                npc = hit.transform.gameObject;
                hb = npc.GetComponent<HealthBehaviour>();
                if (!pm.bloodSucking)
                {
                    hb.health--;
                    currentBlood++;
                    totalBlood++;
                    currentBloodText.text = "Blood: " + currentBlood.ToString();
                    totalBloodText.text = "Total Blood: " + totalBlood.ToString();
                    /*Cursor.lockState = CursorLockMode.None;
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
                    }*/

                    pm.bloodSucking = true;
                }
            }    
        }
        else
        {
            pm.bloodSucking = false;
        }
    }
    
}
