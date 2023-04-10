using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamBehaviour : MonoBehaviour
{
    private PlayerControls pcs;

    private InputAction look;
    private InputAction changeCam;

    public GameObject[] camObjs;
    public Camera[] cams;

    private Vector2 lookDir;
    public Vector3[] camPos;
    public float sensitivity = 50f;

    public float maxLook;
    public float minLook;
    public int camMode = 0;

    public Camera cam;

    /// <summary>
    /// Locks the cursor to the middle of the screen on start
    /// </summary>
    void Awake()
    {
        pcs = new PlayerControls();

        changeCam = pcs.Gameplay.ToggleCamera;
        look = pcs.Gameplay.Look;

        changeCam.performed += ToggleCamera;

        Cursor.lockState = CursorLockMode.Locked;

        camObjs[1].SetActive(false);
        camObjs[2].SetActive(false);
        cam = cams[camMode];
    }

    private void OnEnable()
    {
        changeCam.Enable();
        look.Enable();
        changeCam.performed += ToggleCamera;
    }

    private void OnDisable()
    {
        changeCam.performed -= ToggleCamera;
        changeCam.Disable();
        look.Disable();
    }

    private float xRotation;
    private float yRotation;

    /// <summary>
    /// Calls CameraMovement
    /// </summary>
    void Update()
    {
        lookDir = look.ReadValue<Vector2>();

        switch (camMode)
        {
            case 0:
                FirstPerson();
                break;

            case 1:
                ThirdPersonCam();
                break;

            case 2:
                OrthoCam();
                break;
        }
    }

    private void ToggleCamera(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            camMode++;

            if (camMode > 2)
            {
                camMode = 0;
            }

            for (int x = 0; x < camObjs.Length; x++)
            {
                if (x == camMode)
                {
                    camObjs[x].SetActive(true);
                }
                else
                {
                    camObjs[x].SetActive(false);
                }

                if (camMode == x && x == 2)
                {
                    camObjs[2].transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }

            cam = cams[camMode];
        }
    }

    /// <summary>
    /// Controls the camera
    /// </summary>
    void FirstPerson()
    {
        xRotation = Mathf.Clamp(xRotation - lookDir.y * sensitivity * Time.deltaTime, minLook, maxLook);
        camObjs[0].transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * lookDir.x * sensitivity * Time.deltaTime);
    }

    private void ThirdPersonCam()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, cams[1].transform.position - transform.position, out hit, camPos[1].magnitude * 1.1f))
        {
            float hitFix = Mathf.Clamp(hit.distance - 0.1f, 0.1f, camPos[1].magnitude);
            cams[1].transform.localPosition = camPos[1].normalized * hitFix;
        }
        else
        {
            cams[1].transform.localPosition = camPos[1];
        }

        xRotation = Mathf.Clamp(xRotation - lookDir.y * sensitivity * Time.deltaTime, minLook, maxLook);
        yRotation += lookDir.x * sensitivity * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        camObjs[1].transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    private void OrthoCam()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, cams[2].transform.position - transform.position, out hit, camPos[2].magnitude * 1.1f))
        {
            float hitFix = Mathf.Clamp(hit.distance - 0.1f, 0.1f, camPos[2].magnitude);
            cams[2].transform.localPosition = camPos[2].normalized * hitFix;
        }
        else
        {
            cams[2].transform.localPosition = camPos[2];
        }

        camObjs[2].transform.LookAt(transform.position + Vector3.forward);
    }
}
