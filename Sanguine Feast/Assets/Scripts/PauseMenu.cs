using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    private InputAction pause;
    private PlayerControls pcs;
    public static bool isPaused;


    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject UI;
    bool canPauseAgain;
    private void Awake()
    {
        pcs = new PlayerControls();
        pause = pcs.Gameplay.Pause;

        pause.started += OnPause;
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
                if (canPauseAgain)
                {
                    Resume();
                }
            }
            else
            {
                Pause();
            }
        }
        checkPauseOpen();
    }


    public void Resume()
    {
        pausePanel.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
        UI.SetActive(true);
    }

    private void Pause()
    {
        pausePanel.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
        UI.SetActive(false);
    }


    void checkPauseOpen()
    {
        if (pausePanel.activeSelf == true)
        {
            canPauseAgain = true;
        }
        else
        {
            canPauseAgain = false;
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

}