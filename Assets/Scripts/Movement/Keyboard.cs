using Player;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Movement
{
    public class Keyboard : IControl
    {
        private readonly InputAction _moveAction;

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
            if (Mathf.Approximately(inputPos.y, 1))
            {
                player.IsMoving = false;
               player.menu_slider_open();
               player.menuOpen.GetComponent<MenuCountdown>().StartTimer();
               //enter_slider(player.menu_open);
            }
            else
            {
                player.IsMoving = true;
                player.menu_slider_close();
                player.menuOpen.GetComponent<MenuCountdown>().EndTimer();
            }
            Vector3 worldMouse =
                Camera.main.ScreenToWorldPoint(new Vector3(inputPos.x, inputPos.y, Camera.main.nearClipPlane));
            Vector3 mouseNext = new Vector3(player.player.transform.position.x+(inputPos.x*10), player.player.transform.position.y,  worldMouse.z);

            // Implement mouse-based movement logic
            if (MenuManager.Instance.currentMenu.activeSelf == false&&inputPos.x!=0)
            {
                
                player.SetFacingDirection(inputPos.x);
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
                            player.player.transform.position=Vector3.MoveTowards(player.player.transform.position, mouseNext,
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