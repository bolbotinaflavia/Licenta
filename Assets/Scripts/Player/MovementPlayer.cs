using Movement;
using UnityEngine;
using UnityEngine.InputSystem;
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

            InputAction m1= playerActionMap.FindAction("MouseMove");

            // Initialize movement strategies
            CurrentControl = new Mouse(m1);
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
                CurrentControl = new Mouse(strategy);
                CurrentControl.Enable();
                Debug.Log("move with:mouse");
            }

            if (strategy.name == "KeyboardMove")
            {
                CurrentControl = new Keyboard(strategy);
                CurrentControl.Enable();
                Debug.Log("move with:keyboard=>"+ret_icontrol_name(CurrentControl));
            }

            if (strategy.name=="EyeMove")
            {
                CurrentControl = new EyeTrack(strategy);
                CurrentControl.Enable();
                Debug.Log("move with:eye_track");
            }
        }

        private string ret_icontrol_name(IControl icontrol)
        {
            return icontrol.GetType().Name;
        }
    }
}