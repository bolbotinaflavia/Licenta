using System;
using Inventory;
using Player;
using StaticObjects;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine;

namespace Movement
{



    public class Mouse : IControl

    {
        private  InputAction _mousePositionAction;
        public InputAction MousePositionAction
        {
            get => _mousePositionAction;
            set => _mousePositionAction = value;
        }
        private readonly InputAction _mouseClickAction;

        //public string name;

        
        public Mouse(InputAction mousePositionAction, InputAction mouseClickAction)
        {
            this._mousePositionAction = mousePositionAction;
            this._mouseClickAction = mouseClickAction;
            Debug.Log(mousePositionAction.ReadValue<Vector2>());
        }

        public void Enable()
        {
            _mousePositionAction.Enable();
            _mouseClickAction.Enable();
        }

        public void Disable()
        {
            _mousePositionAction.Disable();
            _mouseClickAction.Disable();
        }

        public InputAction get_action()
        {
            return _mousePositionAction;
        }

        public InputAction get_click_action()
        {
            return _mouseClickAction;
        }

        public void enter_slider(UnityEngine.UI.Slider s)
        {
            throw new System.NotImplementedException();
        }

        public void exit_slider(UnityEngine.UI.Slider s)
        {
            throw new System.NotImplementedException();
        }

        public void load_sliders()
        {
            if (this == null)
                return;
        }

        public void select_sliders()
        {
            if (this == null)
                return;
        }

        public void Move_pregame()
        {
            if (this == null)
                return;
        }
         public void UpdateUI()
        {
            if (this == null)
                return;
        }
        public void Move(PlayerManager player)
        {
            if (!PlayerManager.Instance.IsMoving) return;
            Vector2 inputPos = _mousePositionAction.ReadValue<Vector2>();
            Vector3 worldMouse =
                Camera.main.ScreenToWorldPoint(new Vector3(inputPos.x, inputPos.y, Camera.main.nearClipPlane));
            Vector3 mouseNext = new Vector3(worldMouse.x, player.player.transform.position.y, worldMouse.z);

            // Implement mouse-based movement logic
            if (MenuManager.Instance.currentMenu.activeSelf == false)
            {

                float distance = mouseNext.x - player.player.transform.position.x;
                player.SetFacingDirection(distance);
                RaycastHit2D hit = Physics2D.Raycast(inputPos, Vector2.zero);
                if ((hit.collider != null && hit.collider.gameObject == player.player) || player.newItem||player.beginBattle)
                
                {
                    player.menu_slider_open();
                    player.menuOpen.GetComponent<MenuCountdown>().OnClicked();
                }
                else
                {
                    player.IsMoving = true;
                }
                
                if (player.IsMoving)
                {
                    if (player.player.transform.position.x < 398)
                    {
                        //IsMoving = false;
                        player.player.transform.position = Vector3.MoveTowards(
                            player.player.transform.position,
                            new Vector2(400f, player.player.transform.position.y), Time.deltaTime * 75f);
                    }

                    if (Door.Instance.Opened == true&&player.player.transform.position.x < Door.Instance.transform.position.x+100f)
                    {
                            player.player.transform.position = Vector3.MoveTowards(
                                player.player.transform.position,
                                new Vector2(Door.Instance.transform.position.x+150f, player.player.transform.position.y), Time.deltaTime * 75f);
                    }
                    else
                    {

                        player.IsMoving = true;
                        player.transform.position =
                            Vector3.MoveTowards(player.player.transform.position, mouseNext,
                                Time.deltaTime * 75f);
                    }
                }

            }


            else
            {
                player.IsMoving = false;
            }
        }
    }
}