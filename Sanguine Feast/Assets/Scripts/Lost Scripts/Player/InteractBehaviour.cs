using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractBehaviour : MonoBehaviour
{
    public GameController gc;
    public MovementBehaviour mb;
    public Camera cam;

    private PlayerControls pcs;

    private InputAction interact_;

    public float reach;

    void Awake()
    {
        pcs = InputManager.pcs;

        interact_ = pcs.Gameplay.Hold;
        interact_.performed += Interact;

        cam = GetComponentInChildren<Camera>();
    }

    private void OnEnable()
    {
        interact_.Enable();
        interact_.performed += Interact;
    }

    private void OnDisable()
    {
        interact_.performed -= Interact;
        interact_.Disable();
    }

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        mb = GetComponent<MovementBehaviour>();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (mb.canMove && context.performed)
        {
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit) && hit.distance <= reach)
            {
                IInteractable ib = hit.collider.gameObject.GetComponent<IInteractable>();

                if (ib != null)
                {
                    ib.Interact();
                }
            }
        }
    }
}