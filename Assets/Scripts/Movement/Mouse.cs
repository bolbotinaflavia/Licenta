using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

namespace Movement
{



    public class Mouse : IControl

    {
        private InputAction mousePositionAction;
        //public string name;

        
        public Mouse(InputAction mousePositionAction)
        {
            this.mousePositionAction = mousePositionAction;
            Debug.Log(mousePositionAction.ReadValue<Vector2>());
        }

        public void Enable()
        {
            mousePositionAction.Enable();
        }

        public void Disable()
        {
            mousePositionAction.Disable();
        }

        public InputAction get_action()
        {
            return mousePositionAction;
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
            Vector2 inputPos = mousePositionAction.ReadValue<Vector2>();
            Vector3 worldMouse =
                Camera.main.ScreenToWorldPoint(new Vector3(inputPos.x, inputPos.y, Camera.main.nearClipPlane));
            Vector3 mouseNext = new Vector3(worldMouse.x, player.player.transform.position.y, worldMouse.z);

            // Implement mouse-based movement logic
            if (MenuManager.Instance.current_menu.activeSelf == false)
            {

                float distance = mouseNext.x - player.player.transform.position.x;
                player.setFacingDirection(distance);
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
                        player.transform.position =
                            UnityEngine.Vector3.MoveTowards(player.player.transform.position, mouseNext,
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