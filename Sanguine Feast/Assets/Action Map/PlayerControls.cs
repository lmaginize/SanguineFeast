//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Action Map/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""aee56b21-dc15-41e0-9bd8-2cfb807eabe1"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""22f61ff6-283d-42dc-8a20-c70a82d8eb28"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""b48a3ff4-d022-45b8-b155-f32734f361a9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""90b38c1c-0188-4313-9ea5-48fafebd21b8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""720b036e-c3c8-453b-a389-a7a568d5b1c0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack1"",
                    ""type"": ""Button"",
                    ""id"": ""65bcd638-56cd-46ee-a704-baade53d5eea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""d9fe6415-feb2-4397-b14c-7c181494494f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Hold"",
                    ""type"": ""Button"",
                    ""id"": ""359b0dd1-807a-40e1-bbe0-0abadfcf6cb5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleCamera"",
                    ""type"": ""Button"",
                    ""id"": ""4b06dae6-1cdc-4131-b25e-919ba645739d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShadowStep"",
                    ""type"": ""Button"",
                    ""id"": ""4a2079a2-9376-45b8-aac0-60069830a208"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShapeShift"",
                    ""type"": ""Button"",
                    ""id"": ""ceebc09c-4cfb-4236-8269-d935aa68dfeb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CancelShadowStep"",
                    ""type"": ""Button"",
                    ""id"": ""92e1735c-6a8b-4d1b-b746-cd5945e1522c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShadowCreation"",
                    ""type"": ""Button"",
                    ""id"": ""4eb48cce-4a53-4aa5-a16a-892d3be569ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""144bc589-da06-4bcd-85a0-1d49b7403016"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d62ffb66-7557-44db-90cb-533344b734dd"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Basic"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""db367667-02bd-47c6-9c8c-af40c746add3"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4f12a058-7237-4794-b9b0-f40b565e01a9"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Basic"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ce60235a-f48b-418f-bb1e-2db7c86bb57b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Basic"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8c9518d2-467c-4c4e-96c9-cf4f66357c0c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Basic"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4579f414-e910-4127-a37e-3bc95e17bbf0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Basic"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fa0426e9-aa04-4087-95d8-a3c7f50a3a54"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Basic"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""919f3b21-2f47-43b3-80ad-cd87964bba63"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Basic"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b696683-ca77-4229-8744-277430f57575"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Basic"",
                    ""action"": ""Attack1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26c4db2f-6eda-4fe9-ae62-322aae972aa9"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Basic"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d82a6e8c-21f8-4291-b90b-9e55e6149f76"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Basic"",
                    ""action"": ""Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d01eda06-c953-486b-bbf0-3acca4926fe7"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27cc0001-b649-45ec-a3f8-6154eb4ffc3f"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShadowStep"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f6e7074-ca3a-4dcf-b6cf-62cf9eaa43f8"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShapeShift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""feffe368-ec34-4184-bace-eb72314f625a"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CancelShadowStep"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""166a7162-7992-41b8-979a-e0c000c00392"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShadowCreation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c35542b6-1860-4e1b-97c6-ce8b170f68c9"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Basic"",
            ""bindingGroup"": ""Basic"",
            ""devices"": []
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Look = m_Gameplay.FindAction("Look", throwIfNotFound: true);
        m_Gameplay_Sprint = m_Gameplay.FindAction("Sprint", throwIfNotFound: true);
        m_Gameplay_Attack1 = m_Gameplay.FindAction("Attack1", throwIfNotFound: true);
        m_Gameplay_Crouch = m_Gameplay.FindAction("Crouch", throwIfNotFound: true);
        m_Gameplay_Hold = m_Gameplay.FindAction("Hold", throwIfNotFound: true);
        m_Gameplay_ToggleCamera = m_Gameplay.FindAction("ToggleCamera", throwIfNotFound: true);
        m_Gameplay_ShadowStep = m_Gameplay.FindAction("ShadowStep", throwIfNotFound: true);
        m_Gameplay_ShapeShift = m_Gameplay.FindAction("ShapeShift", throwIfNotFound: true);
        m_Gameplay_CancelShadowStep = m_Gameplay.FindAction("CancelShadowStep", throwIfNotFound: true);
        m_Gameplay_ShadowCreation = m_Gameplay.FindAction("ShadowCreation", throwIfNotFound: true);
        m_Gameplay_Pause = m_Gameplay.FindAction("Pause", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Look;
    private readonly InputAction m_Gameplay_Sprint;
    private readonly InputAction m_Gameplay_Attack1;
    private readonly InputAction m_Gameplay_Crouch;
    private readonly InputAction m_Gameplay_Hold;
    private readonly InputAction m_Gameplay_ToggleCamera;
    private readonly InputAction m_Gameplay_ShadowStep;
    private readonly InputAction m_Gameplay_ShapeShift;
    private readonly InputAction m_Gameplay_CancelShadowStep;
    private readonly InputAction m_Gameplay_ShadowCreation;
    private readonly InputAction m_Gameplay_Pause;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Look => m_Wrapper.m_Gameplay_Look;
        public InputAction @Sprint => m_Wrapper.m_Gameplay_Sprint;
        public InputAction @Attack1 => m_Wrapper.m_Gameplay_Attack1;
        public InputAction @Crouch => m_Wrapper.m_Gameplay_Crouch;
        public InputAction @Hold => m_Wrapper.m_Gameplay_Hold;
        public InputAction @ToggleCamera => m_Wrapper.m_Gameplay_ToggleCamera;
        public InputAction @ShadowStep => m_Wrapper.m_Gameplay_ShadowStep;
        public InputAction @ShapeShift => m_Wrapper.m_Gameplay_ShapeShift;
        public InputAction @CancelShadowStep => m_Wrapper.m_Gameplay_CancelShadowStep;
        public InputAction @ShadowCreation => m_Wrapper.m_Gameplay_ShadowCreation;
        public InputAction @Pause => m_Wrapper.m_Gameplay_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Sprint.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprint;
                @Attack1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack1;
                @Attack1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack1;
                @Attack1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack1;
                @Crouch.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch;
                @Hold.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHold;
                @Hold.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHold;
                @Hold.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHold;
                @ToggleCamera.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToggleCamera;
                @ToggleCamera.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToggleCamera;
                @ToggleCamera.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToggleCamera;
                @ShadowStep.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShadowStep;
                @ShadowStep.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShadowStep;
                @ShadowStep.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShadowStep;
                @ShapeShift.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShapeShift;
                @ShapeShift.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShapeShift;
                @ShapeShift.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShapeShift;
                @CancelShadowStep.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCancelShadowStep;
                @CancelShadowStep.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCancelShadowStep;
                @CancelShadowStep.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCancelShadowStep;
                @ShadowCreation.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShadowCreation;
                @ShadowCreation.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShadowCreation;
                @ShadowCreation.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShadowCreation;
                @Pause.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Attack1.started += instance.OnAttack1;
                @Attack1.performed += instance.OnAttack1;
                @Attack1.canceled += instance.OnAttack1;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Hold.started += instance.OnHold;
                @Hold.performed += instance.OnHold;
                @Hold.canceled += instance.OnHold;
                @ToggleCamera.started += instance.OnToggleCamera;
                @ToggleCamera.performed += instance.OnToggleCamera;
                @ToggleCamera.canceled += instance.OnToggleCamera;
                @ShadowStep.started += instance.OnShadowStep;
                @ShadowStep.performed += instance.OnShadowStep;
                @ShadowStep.canceled += instance.OnShadowStep;
                @ShapeShift.started += instance.OnShapeShift;
                @ShapeShift.performed += instance.OnShapeShift;
                @ShapeShift.canceled += instance.OnShapeShift;
                @CancelShadowStep.started += instance.OnCancelShadowStep;
                @CancelShadowStep.performed += instance.OnCancelShadowStep;
                @CancelShadowStep.canceled += instance.OnCancelShadowStep;
                @ShadowCreation.started += instance.OnShadowCreation;
                @ShadowCreation.performed += instance.OnShadowCreation;
                @ShadowCreation.canceled += instance.OnShadowCreation;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_BasicSchemeIndex = -1;
    public InputControlScheme BasicScheme
    {
        get
        {
            if (m_BasicSchemeIndex == -1) m_BasicSchemeIndex = asset.FindControlSchemeIndex("Basic");
            return asset.controlSchemes[m_BasicSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnAttack1(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnHold(InputAction.CallbackContext context);
        void OnToggleCamera(InputAction.CallbackContext context);
        void OnShadowStep(InputAction.CallbackContext context);
        void OnShapeShift(InputAction.CallbackContext context);
        void OnCancelShadowStep(InputAction.CallbackContext context);
        void OnShadowCreation(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
