using Eyeware.BeamEyeTracker;
using Eyeware.BeamEyeTracker.Unity;
using Player;
using Tobii.Research.Unity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;


namespace Movement
{
    public class EyeTrack:IControl
    {
       
        public EyeTrack Instance;
        private readonly InputAction _move;
        private API api;
        private BeamEyeTrackerInputDevice _eyeTrackerInputDevice;
        public GazeTrail Trail;

        public Pointer p;
       // public string name;
       
        public EyeTrack(InputAction moveAction)
        {
            this._move = moveAction;
        }

        public void Enable()
        {
            _move.Enable();
            
                api = new API("licenta", new ViewportGeometry());
                if(api != null)
                { 
                //Camera.main.gameObject.GetComponent<CameraControlBehaviour>().cameraControlIsPaused = false;
                api.StartRecenterSimGameCamera();
                api.AttemptStartingTheBeamEyeTracker();
                Debug.Log(api.GetVersion().Major);
                Debug.Log(api.GetTrackingDataReceptionStatus());
                }
            else
            {
                Debug.Log("API is null");
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

        public InputAction get_click_action()
        {
            return _move;
        }

        public void enter_slider(Slider s)
        {
            Debug.Log("You looked at this slider: "+s.gameObject.name);
        }

        public void exit_slider(Slider s)
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

        public void UpdateUI()
        {
            if (this == null)
                return;
        }

        public void Move_pregame()
        {
            if (this == null)
                return;
        }

        public void Move(PlayerManager player)
        {
            if (api.GetTrackingDataReceptionStatus() == TrackingDataReceptionStatus.ReceivingTrackingData)
            {
                Point inputpos = api.GetLatestTrackingStateSet().UserState.UnifiedScreenGaze.PointOfRegard;
                p = new Pointer();
                Vector3 worldMouse =
                    Camera.main.ScreenToWorldPoint(new Vector3(inputpos.X, inputpos.Y,
                        Camera.main.nearClipPlane));
                Vector3 mouseNext = new Vector3(worldMouse.x, player.player.transform.position.y, worldMouse.z);
                // Implement mouse-based movement logic
                if (MenuManager.Instance.currentMenu.activeSelf == false)
                {
                    player.menuOpen.gameObject.SetActive(true);
                    float distance = mouseNext.x - player.player.transform.position.x;
                    player.SetFacingDirection(distance);
                    
                    
                    // if (player.GetComponent<Collider2D>().bounds
                    //     .Contains(new Vector3(inputpos.X, inputpos.Y, Camera.main.nearClipPlane)))
                    // {
                    //     Debug.Log("the menu should open");
                    //     player.IsMoving = false;
                    //     player.isHover = true;
                    //     player.menu_slider_open();
                    //     player.menuOpen.GetComponent<MenuCountdown>().OnClicked();
                    // }
                    if (player.isMoving != false)
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
                    player.menuOpen.gameObject.SetActive(false);
                    player.IsMoving = false;
                }

            }
        }
    }
}