using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    private InputAction pause;
    private PlayerControls pcs;
    public static bool isPaused;


    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject UI;
    [SerializeField] Slider mouseSlider;

    [SerializeField] TMP_Text mouseValue;
    private CamBehaviour camScript;
    private void Awake()
    {
        pcs = new PlayerControls();
        pause = pcs.Gameplay.Pause;

        pause.started += OnPause;
    }

    private void Start()
    {
        camScript = FindObjectOfType<CamBehaviour>();
        mouseSlider.maxValue = camScript.sensitivity;
        mouseSlider.value = camScript.sensitivity;
        mouseValue.text = camScript.sensitivity.ToString();
    }
    private void OnEnable()
    {
        pause.Enable();
    }

    private void OnDisable()
    {
        pause.Disable();
    }

    public void OnPause(InputAction.CallbackContext obj)
    {
        if (obj.started)
        {
            if (isPaused)
            {
                if (pausePanel.activeSelf == true)
                {
                    Resume();
                }
            }
            else
            {
                Pause();


            }
        }
    }

    


    public void Resume()
    {
        UI.SetActive(true);
        pausePanel.SetActive(false);

        isPaused = false;
        Time.timeScale = 1f;
       
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Pause()
    {
        UI.SetActive(false);
        pausePanel.SetActive(true);

        isPaused = true;
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void MouseSlider()
    {
        mouseValue.text = mouseSlider.value.ToString("F0");
        camScript.sensitivity = mouseSlider.value;
    }

}