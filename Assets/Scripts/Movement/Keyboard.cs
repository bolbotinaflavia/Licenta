using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;
    

namespace Movement
{
    public class Keyboard : IControl
    {
        private InputAction _moveAction;

        public Keyboard(InputAction moveAction)
        {
            this._moveAction = moveAction;
            
        }

        public void Enable()
        {
            _moveAction.Enable();
        }

        public void Disable()
        {
            _moveAction.Disable();
        }

        public InputAction get_action()
        {
            return _moveAction;
        }

        public void enter_slider(UnityEngine.UI.Slider s)
        {
            throw new System.NotImplementedException();
        }

        public void exit_slider(UnityEngine.UI.Slider s)
        {
            throw new System.NotImplementedException();
        }

        public void Move(PlayerManager player)
        {
            
            Vector2 inputPos = _moveAction.ReadValue<Vector2>();
            //Debug.Log(inputPos);
            if (inputPos.y == 1)
            {
                player.IsMoving = false;
               player.menu_slider_open();
               player.menu_open.GetComponent<Menu_countdown>().StartTimer();
               //enter_slider(player.menu_open);
            }
            else
            {
                player.IsMoving = true;
                player.menu_slider_close();
                player.menu_open.GetComponent<Menu_countdown>().EndTimer();
            }
            Vector3 worldMouse =
                Camera.main.ScreenToWorldPoint(new Vector3(inputPos.x, inputPos.y, Camera.main.nearClipPlane));
            Vector3 mouseNext = new Vector3(player.player.transform.position.x+(inputPos.x*10), player.player.transform.position.y,  worldMouse.z);

            // Implement mouse-based movement logic
            if (MenuManager.Instance.current_menu.activeSelf == false&&inputPos.x!=0)
            {
                
                player.setFacingDirection(inputPos.x);
                RaycastHit2D hit = Physics2D.Raycast(inputPos, UnityEngine.Vector2.zero);
                if ((hit.collider != null && hit.collider.gameObject == player.player) || player.new_item == true)

                {
                    player.IsMoving = false;
                    return;
                }
                else
                {
                    if (player.player.transform.position.x < 398)
                    {
                        //IsMoving = false;
                        player.player.transform.position = UnityEngine.Vector3.MoveTowards(
                            player.player.transform.position,
                            new Vector2(400f, player.player.transform.position.y), Time.deltaTime * 50f);
                    }
                    else
                    {

                        player.IsMoving = true;
                            player.player.transform.position=UnityEngine.Vector3.MoveTowards(player.player.transform.position, mouseNext,
                                Time.deltaTime * 50f);
                    }
                }

            }


            else
            {
                player.IsMoving = false;
                return;
            }
        }
    }
}