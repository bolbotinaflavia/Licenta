using System;
using Movement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Mouse = Movement.Mouse;
using Keyboard = Movement.Keyboard;
using EyeTrack = Movement.EyeTrack;

namespace Player
{
    public class PlayerMovement:MonoBehaviour
    {
        private static PlayerMovement _instance;

        public static PlayerMovement Instance
        {
            get => _instance;
            set => _instance = value;
        }
        public InputActionAsset inputActions;
        public InputActionMap inputActionMap;
        public IControl CurrentControl;
        public PlayerManager managerPlayer;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }
        }
        private void Start()
        {
            InputActionMap playerActionMap = inputActions.FindActionMap("Player");
            inputActionMap = playerActionMap;
            InputAction m1= playerActionMap.FindAction("MouseMove");
            InputAction m2= playerActionMap.FindAction("MouseClick");

            // Initialize movement strategies
            CurrentControl = new Mouse(m1,m2);
            // Set the initial movement strategy
            CurrentControl.Enable();
            //playerManager.SetMovementStrategy(current_control);

            // Example of switching strategies based on input
            // playerManager.SetMovementStrategy(keyboardMovement);
            // playerManager.SetMovementStrategy(trackedDeviceMovement);
        }

        public void change_strategy(InputAction strategy)
        {
            if (strategy.name == "MouseMove")
            {
                InputAction strategy2= inputActionMap.FindAction("MouseClick");
                CurrentControl = new Mouse(strategy,strategy2);
                CurrentControl.Enable();
                Debug.Log("move with:mouse");
            }

            if (strategy.name == "KeyboardMove")
            {
                InputAction strategy2= inputActionMap.FindAction("KeyboardClick");
                CurrentControl = new Keyboard(strategy, strategy2);
                CurrentControl.Enable();
                CurrentControl.load_sliders();
                Debug.Log("move with:keyboard=>"+ret_icontrol_name(CurrentControl));
            }

            if (strategy.name=="EyeMove")
            {
                CurrentControl = new EyeTrack(strategy);
                CurrentControl.Enable();
                if (CurrentControl.get_action().enabled == false)
                {
                    InputAction s= inputActionMap.FindAction("MouseMove");
                    CurrentControl.Disable();
                    change_strategy(s);
                    
                }
                else
                    Debug.Log("move with:eye_track");
            }
        }

        public string ret_icontrol_name(IControl icontrol)
        {
            return icontrol.GetType().Name;
        }
    }
}