using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.VisualScripting;


namespace Sliders_scripts
{
    public class Control_menu: Menu_countdown
    {
        //de vazut cum selectezi input ul si il activezi
        public PlayerMovement playerMovement;
        public InputAction input;
        public string name;
        protected override void OnTimerComplete()
        {
            //i = GetComponent<Control>();
            if(input!=null)
                Select_item();
        }

        public void FindControls()
        {
            InputActionMap playerActionMap = playerMovement.inputActions.FindActionMap("Player");
            input = new InputAction();
            input= playerActionMap.FindAction(name);
            //InputAction mousePositionAction = playerActionMap.FindAction("Position");
           // InputAction moveAction = playerActionMap.FindAction("2D Vector");
            //InputAction devicePositionAction = playerActionMap.FindAction("devicePosition");
        }



        // Start is called before the first frame update
        void Start()
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

                Color c = new Color(0.9568627f, 0.7058824f, 0.1058824f);
                Color c_back = new Color(0.4901961F, 0.4392157F, 0.4431373F);

                foreach (InputAction inp in playerMovement.inputActions)
                {
                    inp.Disable();

                }

                foreach (var s in FindObjectsOfType<Slider>())
                {
                    if (s.IsActive())
                    {
                        if (!s.name.Equals("Back")&&!s.name.Equals("HP"))
                            s.fillRect.GetComponent<Image>().color = c;
                    }
                }

                //weapon.InUse = true;
                menu_option.fillRect.GetComponent<Image>().color = new Color(0.9019608f, 0.282353f, 0.1803922f);
                input.Enable();
                Debug.Log(input.name);
                PlayerMovement.instance.change_strategy(input);
            }
            else
                Debug.Log("Control already enabled");
        }
        public void UpdateUI()
        {
            if (input!= null)
            {
                if(input.enabled==true)
                    menu_option.fillRect.GetComponent<Image>().color = new Color(0.9019608f,0.282353f,0.1803922f);
            }
        }
    }
    
}