using Player;
using UnityEngine.InputSystem;
using UnityEngine;

namespace Movement
{



    public class Mouse : IControl

    {
        private readonly InputAction _mousePositionAction;
        //public string name;

        
        public Mouse(InputAction mousePositionAction)
        {
            this._mousePositionAction = mousePositionAction;
            Debug.Log(mousePositionAction.ReadValue<Vector2>());
        }

        public void Enable()
        {
            _mousePositionAction.Enable();
        }

        public void Disable()
        {
            _mousePositionAction.Disable();
        }

        public InputAction get_action()
        {
            return _mousePositionAction;
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
                if ((hit.collider != null && hit.collider.gameObject == player.player) || player.newItem)

                {
                    player.IsMoving = false;
                }
                else
                {
                    if (player.player.transform.position.x < 398)
                    {
                        //IsMoving = false;
                        player.player.transform.position = Vector3.MoveTowards(
                            player.player.transform.position,
                            new Vector2(400f, player.player.transform.position.y), Time.deltaTime * 50f);
                    }
                    else
                    {

                        player.IsMoving = true;
                        player.transform.position =
                            Vector3.MoveTowards(player.player.transform.position, mouseNext,
                                Time.deltaTime * 50f);
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