using Player;
using Tobii.Research.Unity;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Movement
{
    public class EyeTrack:IControl
    {
        public EyeTrack Instance;
        private readonly InputAction _move;
        private EyeTracker _eyeTrack;
        public GazeTrail Trail;
       // public string name;
       
        public EyeTrack(InputAction moveAction)
        {
            this._move = moveAction;
        }

        public void Enable()
        {
            _move.Enable();
            if (_eyeTrack == null)
            {
                _eyeTrack = Object.FindObjectOfType<EyeTracker>();
                if (_eyeTrack != null)
                {
                    _eyeTrack.enabled = true;
                    Trail = Object.FindObjectOfType<GazeTrail>();
                    Debug.Log("Connected to Eye Track:" + _eyeTrack.name);
                }
                else
                {
                    Debug.Log("No Eye Track Found");
                }
            }
            else
            {
                Debug.Log("Already connected to Eye Track");
            }
        }

        public void Disable()
        {
            _move.Disable();
        }

        public InputAction get_action()
        {
            return _move;
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
            
            Vector2 inputPos = _move.ReadValue<Vector2>();
            //Debug.Log(inputPos);
            Vector3 worldMouse =
                Camera.main.ScreenToWorldPoint(new Vector3(inputPos.x, inputPos.y, Camera.main.nearClipPlane));
            Vector3 mouseNext = new Vector3(worldMouse.x, player.player.transform.position.y, worldMouse.z);

            // Implement mouse-based movement logic
            if (MenuManager.Instance.currentMenu.activeSelf == false)
            {

                float distance = mouseNext.x - player.player.transform.position.x;
                player.SetFacingDirection(distance);
                RaycastHit2D hit = Physics2D.Raycast(inputPos, Vector2.zero);
                if ((hit.collider != null && hit.collider.gameObject == player.player) || player.NewItem)

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