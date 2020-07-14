// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""GameplayControllerA"",
            ""id"": ""3e6b0c71-7748-4e87-a858-cfe41b9646ed"",
            ""actions"": [
                {
                    ""name"": ""RotatePoleLeftHand"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ef03efe7-0579-4ad1-a504-b189b94d726a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DragPoleLeftHand"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9531f09e-174e-4503-9800-c39f909570e5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotatePoleRightHand"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5d140119-2d88-4987-a199-c01725878529"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DragPoleRightHand"",
                    ""type"": ""PassThrough"",
                    ""id"": ""33700003-b1fc-42c7-aabc-5498f626c4ea"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Defense"",
                    ""type"": ""Button"",
                    ""id"": ""b1e2bce9-9d76-45da-bb73-5aae7803e252"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Offense"",
                    ""type"": ""Button"",
                    ""id"": ""bd559bca-2774-4bfe-a975-c5e8372e5662"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""150752c9-9345-4a7f-815e-f0c0e935f056"",
                    ""path"": ""<XInputController>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RotatePoleLeftHand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""899dfc9d-fcfe-4350-afd7-ef4714ad6fb8"",
                    ""path"": ""<XInputController>/leftStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DragPoleLeftHand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84be4f8d-c53f-41ec-a2ec-75b1e1dff870"",
                    ""path"": ""<XInputController>/rightStick/x"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RotatePoleRightHand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0031399d-fd76-4a87-b3a6-f0891a543081"",
                    ""path"": ""<XInputController>/rightStick/y"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DragPoleRightHand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d534404-5bce-4174-a9a6-690dc556a801"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Defense"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3d3d292-19da-4384-8220-e90007b57281"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Offense"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""GameplayControllerB"",
            ""id"": ""67b6c06e-74d2-46a1-b4a0-cc6e179fc559"",
            ""actions"": [
                {
                    ""name"": ""RotatePoleLeftHand"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8b6d25b7-7385-47a6-a404-f8ae0c994667"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DragPoleLeftHand"",
                    ""type"": ""PassThrough"",
                    ""id"": ""23eb0592-8b4b-424c-9342-45c578061978"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotatePoleRightHand"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f9d7def7-e1b7-49fd-a5c5-450be695b99f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DragPoleRightHand"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b160aa58-cc48-44d0-a14e-04ab4124baa8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Defense"",
                    ""type"": ""PassThrough"",
                    ""id"": ""32f1970d-307f-44de-adf3-d81d7c094e62"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Offense"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3be9f69d-63ba-4966-bae6-2bb427469f20"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""55839537-2a6a-4f5b-bdfd-21c40deab50e"",
                    ""path"": ""<XInputController>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RotatePoleLeftHand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26a5080f-f988-4d20-972b-4914c0b05ece"",
                    ""path"": ""<XInputController>/leftStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DragPoleLeftHand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c15ab133-2681-40a4-84c8-da01b55fec1b"",
                    ""path"": ""<XInputController>/rightStick/x"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RotatePoleRightHand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0207ad6-476f-4761-a6ce-11a919807dab"",
                    ""path"": ""<XInputController>/rightStick/y"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(min=-1,max=1)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DragPoleRightHand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6216f726-3e85-4103-8026-3373bf6f37fa"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Defense"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ec04f8a-7e82-44be-8898-0c323dcfcdc6"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Offense"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // GameplayControllerA
        m_GameplayControllerA = asset.FindActionMap("GameplayControllerA", throwIfNotFound: true);
        m_GameplayControllerA_RotatePoleLeftHand = m_GameplayControllerA.FindAction("RotatePoleLeftHand", throwIfNotFound: true);
        m_GameplayControllerA_DragPoleLeftHand = m_GameplayControllerA.FindAction("DragPoleLeftHand", throwIfNotFound: true);
        m_GameplayControllerA_RotatePoleRightHand = m_GameplayControllerA.FindAction("RotatePoleRightHand", throwIfNotFound: true);
        m_GameplayControllerA_DragPoleRightHand = m_GameplayControllerA.FindAction("DragPoleRightHand", throwIfNotFound: true);
        m_GameplayControllerA_Defense = m_GameplayControllerA.FindAction("Defense", throwIfNotFound: true);
        m_GameplayControllerA_Offense = m_GameplayControllerA.FindAction("Offense", throwIfNotFound: true);
        // GameplayControllerB
        m_GameplayControllerB = asset.FindActionMap("GameplayControllerB", throwIfNotFound: true);
        m_GameplayControllerB_RotatePoleLeftHand = m_GameplayControllerB.FindAction("RotatePoleLeftHand", throwIfNotFound: true);
        m_GameplayControllerB_DragPoleLeftHand = m_GameplayControllerB.FindAction("DragPoleLeftHand", throwIfNotFound: true);
        m_GameplayControllerB_RotatePoleRightHand = m_GameplayControllerB.FindAction("RotatePoleRightHand", throwIfNotFound: true);
        m_GameplayControllerB_DragPoleRightHand = m_GameplayControllerB.FindAction("DragPoleRightHand", throwIfNotFound: true);
        m_GameplayControllerB_Defense = m_GameplayControllerB.FindAction("Defense", throwIfNotFound: true);
        m_GameplayControllerB_Offense = m_GameplayControllerB.FindAction("Offense", throwIfNotFound: true);
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

    // GameplayControllerA
    private readonly InputActionMap m_GameplayControllerA;
    private IGameplayControllerAActions m_GameplayControllerAActionsCallbackInterface;
    private readonly InputAction m_GameplayControllerA_RotatePoleLeftHand;
    private readonly InputAction m_GameplayControllerA_DragPoleLeftHand;
    private readonly InputAction m_GameplayControllerA_RotatePoleRightHand;
    private readonly InputAction m_GameplayControllerA_DragPoleRightHand;
    private readonly InputAction m_GameplayControllerA_Defense;
    private readonly InputAction m_GameplayControllerA_Offense;
    public struct GameplayControllerAActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayControllerAActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @RotatePoleLeftHand => m_Wrapper.m_GameplayControllerA_RotatePoleLeftHand;
        public InputAction @DragPoleLeftHand => m_Wrapper.m_GameplayControllerA_DragPoleLeftHand;
        public InputAction @RotatePoleRightHand => m_Wrapper.m_GameplayControllerA_RotatePoleRightHand;
        public InputAction @DragPoleRightHand => m_Wrapper.m_GameplayControllerA_DragPoleRightHand;
        public InputAction @Defense => m_Wrapper.m_GameplayControllerA_Defense;
        public InputAction @Offense => m_Wrapper.m_GameplayControllerA_Offense;
        public InputActionMap Get() { return m_Wrapper.m_GameplayControllerA; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayControllerAActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayControllerAActions instance)
        {
            if (m_Wrapper.m_GameplayControllerAActionsCallbackInterface != null)
            {
                @RotatePoleLeftHand.started -= m_Wrapper.m_GameplayControllerAActionsCallbackInterface.OnRotatePoleLeftHand;
                @RotatePoleLeftHand.performed -= m_Wrapper.m_GameplayControllerAActionsCallbackInterface.OnRotatePoleLeftHand;
                @RotatePoleLeftHand.canceled -= m_Wrapper.m_GameplayControllerAActionsCallbackInterface.OnRotatePoleLeftHand;
                @DragPoleLeftHand.started -= m_Wrapper.m_GameplayControllerAActionsCallbackInterface.OnDragPoleLeftHand;
                @DragPoleLeftHand.performed -= m_Wrapper.m_GameplayControllerAActionsCallbackInterface.OnDragPoleLeftHand;
                @DragPoleLeftHand.canceled -= m_Wrapper.m_GameplayControllerAActionsCallbackInterface.OnDragPoleLeftHand;
                @RotatePoleRightHand.started -= m_Wrapper.m_GameplayControllerAActionsCallbackInterface.OnRotatePoleRightHand;
                @RotatePoleRightHand.performed -= m_Wrapper.m_GameplayControllerAActionsCallbackInterface.OnRotatePoleRightHand;
                @RotatePoleRightHand.canceled -= m_Wrapper.m_GameplayControllerAActionsCallbackInterface.OnRotatePoleRightHand;
                @DragPoleRightHand.started -= m_Wrapper.m_GameplayControllerAActionsCallbackInterface.OnDragPoleRightHand;
                @DragPoleRightHand.performed -= m_Wrapper.m_GameplayControllerAActionsCallbackInterface.OnDragPoleRightHand;
                @DragPoleRightHand.canceled -= m_Wrapper.m_GameplayControllerAActionsCallbackInterface.OnDragPoleRightHand;
                @Defense.started -= m_Wrapper.m_GameplayControllerAActionsCallbackInterface.OnDefense;
                @Defense.performed -= m_Wrapper.m_GameplayControllerAActionsCallbackInterface.OnDefense;
                @Defense.canceled -= m_Wrapper.m_GameplayControllerAActionsCallbackInterface.OnDefense;
                @Offense.started -= m_Wrapper.m_GameplayControllerAActionsCallbackInterface.OnOffense;
                @Offense.performed -= m_Wrapper.m_GameplayControllerAActionsCallbackInterface.OnOffense;
                @Offense.canceled -= m_Wrapper.m_GameplayControllerAActionsCallbackInterface.OnOffense;
            }
            m_Wrapper.m_GameplayControllerAActionsCallbackInterface = instance;
            if (instance != null)
            {
                @RotatePoleLeftHand.started += instance.OnRotatePoleLeftHand;
                @RotatePoleLeftHand.performed += instance.OnRotatePoleLeftHand;
                @RotatePoleLeftHand.canceled += instance.OnRotatePoleLeftHand;
                @DragPoleLeftHand.started += instance.OnDragPoleLeftHand;
                @DragPoleLeftHand.performed += instance.OnDragPoleLeftHand;
                @DragPoleLeftHand.canceled += instance.OnDragPoleLeftHand;
                @RotatePoleRightHand.started += instance.OnRotatePoleRightHand;
                @RotatePoleRightHand.performed += instance.OnRotatePoleRightHand;
                @RotatePoleRightHand.canceled += instance.OnRotatePoleRightHand;
                @DragPoleRightHand.started += instance.OnDragPoleRightHand;
                @DragPoleRightHand.performed += instance.OnDragPoleRightHand;
                @DragPoleRightHand.canceled += instance.OnDragPoleRightHand;
                @Defense.started += instance.OnDefense;
                @Defense.performed += instance.OnDefense;
                @Defense.canceled += instance.OnDefense;
                @Offense.started += instance.OnOffense;
                @Offense.performed += instance.OnOffense;
                @Offense.canceled += instance.OnOffense;
            }
        }
    }
    public GameplayControllerAActions @GameplayControllerA => new GameplayControllerAActions(this);

    // GameplayControllerB
    private readonly InputActionMap m_GameplayControllerB;
    private IGameplayControllerBActions m_GameplayControllerBActionsCallbackInterface;
    private readonly InputAction m_GameplayControllerB_RotatePoleLeftHand;
    private readonly InputAction m_GameplayControllerB_DragPoleLeftHand;
    private readonly InputAction m_GameplayControllerB_RotatePoleRightHand;
    private readonly InputAction m_GameplayControllerB_DragPoleRightHand;
    private readonly InputAction m_GameplayControllerB_Defense;
    private readonly InputAction m_GameplayControllerB_Offense;
    public struct GameplayControllerBActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayControllerBActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @RotatePoleLeftHand => m_Wrapper.m_GameplayControllerB_RotatePoleLeftHand;
        public InputAction @DragPoleLeftHand => m_Wrapper.m_GameplayControllerB_DragPoleLeftHand;
        public InputAction @RotatePoleRightHand => m_Wrapper.m_GameplayControllerB_RotatePoleRightHand;
        public InputAction @DragPoleRightHand => m_Wrapper.m_GameplayControllerB_DragPoleRightHand;
        public InputAction @Defense => m_Wrapper.m_GameplayControllerB_Defense;
        public InputAction @Offense => m_Wrapper.m_GameplayControllerB_Offense;
        public InputActionMap Get() { return m_Wrapper.m_GameplayControllerB; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayControllerBActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayControllerBActions instance)
        {
            if (m_Wrapper.m_GameplayControllerBActionsCallbackInterface != null)
            {
                @RotatePoleLeftHand.started -= m_Wrapper.m_GameplayControllerBActionsCallbackInterface.OnRotatePoleLeftHand;
                @RotatePoleLeftHand.performed -= m_Wrapper.m_GameplayControllerBActionsCallbackInterface.OnRotatePoleLeftHand;
                @RotatePoleLeftHand.canceled -= m_Wrapper.m_GameplayControllerBActionsCallbackInterface.OnRotatePoleLeftHand;
                @DragPoleLeftHand.started -= m_Wrapper.m_GameplayControllerBActionsCallbackInterface.OnDragPoleLeftHand;
                @DragPoleLeftHand.performed -= m_Wrapper.m_GameplayControllerBActionsCallbackInterface.OnDragPoleLeftHand;
                @DragPoleLeftHand.canceled -= m_Wrapper.m_GameplayControllerBActionsCallbackInterface.OnDragPoleLeftHand;
                @RotatePoleRightHand.started -= m_Wrapper.m_GameplayControllerBActionsCallbackInterface.OnRotatePoleRightHand;
                @RotatePoleRightHand.performed -= m_Wrapper.m_GameplayControllerBActionsCallbackInterface.OnRotatePoleRightHand;
                @RotatePoleRightHand.canceled -= m_Wrapper.m_GameplayControllerBActionsCallbackInterface.OnRotatePoleRightHand;
                @DragPoleRightHand.started -= m_Wrapper.m_GameplayControllerBActionsCallbackInterface.OnDragPoleRightHand;
                @DragPoleRightHand.performed -= m_Wrapper.m_GameplayControllerBActionsCallbackInterface.OnDragPoleRightHand;
                @DragPoleRightHand.canceled -= m_Wrapper.m_GameplayControllerBActionsCallbackInterface.OnDragPoleRightHand;
                @Defense.started -= m_Wrapper.m_GameplayControllerBActionsCallbackInterface.OnDefense;
                @Defense.performed -= m_Wrapper.m_GameplayControllerBActionsCallbackInterface.OnDefense;
                @Defense.canceled -= m_Wrapper.m_GameplayControllerBActionsCallbackInterface.OnDefense;
                @Offense.started -= m_Wrapper.m_GameplayControllerBActionsCallbackInterface.OnOffense;
                @Offense.performed -= m_Wrapper.m_GameplayControllerBActionsCallbackInterface.OnOffense;
                @Offense.canceled -= m_Wrapper.m_GameplayControllerBActionsCallbackInterface.OnOffense;
            }
            m_Wrapper.m_GameplayControllerBActionsCallbackInterface = instance;
            if (instance != null)
            {
                @RotatePoleLeftHand.started += instance.OnRotatePoleLeftHand;
                @RotatePoleLeftHand.performed += instance.OnRotatePoleLeftHand;
                @RotatePoleLeftHand.canceled += instance.OnRotatePoleLeftHand;
                @DragPoleLeftHand.started += instance.OnDragPoleLeftHand;
                @DragPoleLeftHand.performed += instance.OnDragPoleLeftHand;
                @DragPoleLeftHand.canceled += instance.OnDragPoleLeftHand;
                @RotatePoleRightHand.started += instance.OnRotatePoleRightHand;
                @RotatePoleRightHand.performed += instance.OnRotatePoleRightHand;
                @RotatePoleRightHand.canceled += instance.OnRotatePoleRightHand;
                @DragPoleRightHand.started += instance.OnDragPoleRightHand;
                @DragPoleRightHand.performed += instance.OnDragPoleRightHand;
                @DragPoleRightHand.canceled += instance.OnDragPoleRightHand;
                @Defense.started += instance.OnDefense;
                @Defense.performed += instance.OnDefense;
                @Defense.canceled += instance.OnDefense;
                @Offense.started += instance.OnOffense;
                @Offense.performed += instance.OnOffense;
                @Offense.canceled += instance.OnOffense;
            }
        }
    }
    public GameplayControllerBActions @GameplayControllerB => new GameplayControllerBActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IGameplayControllerAActions
    {
        void OnRotatePoleLeftHand(InputAction.CallbackContext context);
        void OnDragPoleLeftHand(InputAction.CallbackContext context);
        void OnRotatePoleRightHand(InputAction.CallbackContext context);
        void OnDragPoleRightHand(InputAction.CallbackContext context);
        void OnDefense(InputAction.CallbackContext context);
        void OnOffense(InputAction.CallbackContext context);
    }
    public interface IGameplayControllerBActions
    {
        void OnRotatePoleLeftHand(InputAction.CallbackContext context);
        void OnDragPoleLeftHand(InputAction.CallbackContext context);
        void OnRotatePoleRightHand(InputAction.CallbackContext context);
        void OnDragPoleRightHand(InputAction.CallbackContext context);
        void OnDefense(InputAction.CallbackContext context);
        void OnOffense(InputAction.CallbackContext context);
    }
}
