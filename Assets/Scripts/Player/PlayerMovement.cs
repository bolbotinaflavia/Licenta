using System.Collections.Generic;
using Movement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Mouse = Movement.Mouse;
using Keyboard = Movement.Keyboard;
using EyeTrack = Movement.EyeTrack;


    public class PlayerMovement:MonoBehaviour
    {
        public static PlayerMovement instance;
        public InputActionAsset inputActions;
        public IControl current_control;
        public PlayerManager playerManager;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
        }
        private void Start()
        {
            InputActionMap playerActionMap = inputActions.FindActionMap("Player");

            InputAction m1= playerActionMap.FindAction("MouseMove");

            // Initialize movement strategies
           current_control = new Mouse(m1);
            // Set the initial movement strategy
            current_control.Enable();
            //playerManager.SetMovementStrategy(current_control);

            // Example of switching strategies based on input
            // playerManager.SetMovementStrategy(keyboardMovement);
            // playerManager.SetMovementStrategy(trackedDeviceMovement);
        }

        public void change_strategy(InputAction strategy)
        {
            if (strategy.name == "MouseMove")
            {
                current_control = new Mouse(strategy);
                current_control.Enable();
                Debug.Log("move with:mouse");
            }

            if (strategy.name == "KeyboardMove")
            {
                current_control = new Keyboard(strategy);
                current_control.Enable();
                Debug.Log("move with:keyboard=>"+ret_icontrol_name(current_control));
            }

            if (strategy.name=="EyeMove")
            {
                current_control = new EyeTrack(strategy);
                current_control.Enable();
                Debug.Log("move with:eye_track");
            }
        }

        public string ret_icontrol_name(IControl icontrol)
        {
            return icontrol.GetType().Name;
        }
    }