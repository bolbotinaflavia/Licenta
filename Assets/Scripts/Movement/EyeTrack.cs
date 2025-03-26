using Tobii.Research.Unity;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;

namespace Movement
{
    public class EyeTrack:IControl
    {
        public EyeTrack instance;
        private InputAction move;

        public Eyes eyes;
       // public string name;
       
        public EyeTrack(InputAction moveAction)
        {
            this.move = moveAction;
        }

        public void Enable()
        {
            move.Enable();
        }

        public void Disable()
        {
            move.Disable();
        }

        public InputAction get_action()
        {
            return move;
        }

        public void enter_slider(Slider s)
        {
            throw new System.NotImplementedException();
        }

        public void exit_slider(Slider s)
        {
            throw new System.NotImplementedException();
        }

        public void Move(PlayerManager player)
        {
            
            Vector2 inputPos = move.ReadValue<Vector2>();
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