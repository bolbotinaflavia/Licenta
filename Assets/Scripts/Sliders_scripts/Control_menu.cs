using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace Sliders_scripts
{
    public class ControlMenu: MenuCountdown
    {
        //de vazut cum selectezi input ul si il activezi
        public PlayerMovement playerMovement;
        public InputAction input;
        [FormerlySerializedAs("name")] public string nameControl;
        public Image fade;
        protected override void OnTimerComplete()
        {
            //i = GetComponent<Control>();
            if (input != null)
                Select_item();
            UpdateUI();
            StartCoroutine(Deselect());
        }

        public void FindControls()
        {
            InputActionMap playerActionMap = playerMovement.inputActions.FindActionMap("Player");
            input = new InputAction();
            input= playerActionMap.FindAction(nameControl);
            //InputAction mousePositionAction = playerActionMap.FindAction("Position");
           // InputAction moveAction = playerActionMap.FindAction("2D Vector");
            //InputAction devicePositionAction = playerActionMap.FindAction("devicePosition");
        }



        // Start is called before the first frame update
        private void Start()
        {
            //weapon = w.gameObject.GetComponent<Weapons>();
            playerMovement = FindObjectOfType<PlayerMovement>();
            FindControls();
            UpdateUI();
        
        }
        public void Select_item()
        {
            if (input.enabled==false)
            {
                foreach (InputAction inp in playerMovement.inputActions)
                {
                    inp.Disable();
                }
                //weapon.InUse = true;
                PlayerMovement.Instance.change_strategy(input);
             //   input.Enable();
                if (PlayerMovement.Instance.CurrentControl.get_action().Equals(nameControl))
                {
                    Debug.Log(input.name);
                }
                else
                {
                    Debug.Log("the input could not be found");
                }
            }
            else
                Debug.Log("Control already enabled");
            menuOption.value = 1;
        }
        public void UpdateUI()
        {
            foreach (var s in FindObjectsOfType<Slider>())
            {
                if (s.IsActive())
                {
                    if (!s.name.Equals("Back") && !s.gameObject.tag.Equals("HP"))
                    {
                        if (s.GetComponent<ControlMenu>() != null)
                        {
                            if (s.GetComponent<ControlMenu>().input.enabled == false)
                            {
                                s.GetComponent<ControlMenu>().fade.color =
                                    new Color(0.4901961f, 0.4392157f, 0.4431373f);
                                s.fillRect.GetComponent<Image>().color =
                                    new Color(0.9568627f, 0.7058824f, 0.1058824f);
                            }
                            else
                            {
                                s.GetComponent<ControlMenu>().fade.color =
                                    new Color(0.2234294f, 0.4823529f, 0.2666667f);
                                s.fillRect.GetComponent<Image>().color =
                                    new Color(0.9568627f, 0.7058824f, 0.1058824f);
                            }
                        }
                    }
                }
            }
        }
    }
    
}