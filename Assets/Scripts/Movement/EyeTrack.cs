#if !UNITY_WEBGL
using Eyeware.BeamEyeTracker;
using Eyeware.BeamEyeTracker.Unity;
#endif
using Player;
using StaticObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


namespace Movement
{
    public class EyeTrack : IControl
    {
        private readonly InputAction _move;
#if !UNITY_WEBGL
        private API api;
        public API Api
        {
            get => api;
        }
        private BeamEyeTrackerInputDevice _eyeTrackerInputDevice;
#endif

        public Pointer p;
        // public string name;

        public EyeTrack(InputAction moveAction)
        {
            this._move = moveAction;
        }

        public void Enable()
        {
#if !UNITY_WEBGL
            api = new API("licenta", new ViewportGeometry());
            if (api != null)
            {
                api.StartRecenterSimGameCamera();
                api.AttemptStartingTheBeamEyeTracker();
                Debug.Log(api.GetVersion().Major);
                Debug.Log(api.GetTrackingDataReceptionStatus());
                if (api.GetTrackingDataReceptionStatus() == TrackingDataReceptionStatus.NotReceivingTrackingData)
                {
                    Debug.Log("not receiving data");
                }
                else
                {
                    _move.Enable();
                }
            }
            else
            {
                Debug.Log("API is null");
            }
#endif
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
            Debug.Log("You looked at this slider: " + s.gameObject.name);
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
            if (!PlayerManager.Instance.IsMoving) return;
#if !UNITY_WEBGL
            if (api.GetTrackingDataReceptionStatus() == TrackingDataReceptionStatus.ReceivingTrackingData)
            {
                Point inputpos = api.GetLatestTrackingStateSet().UserState.UnifiedScreenGaze.PointOfRegard;
                p = new Pointer();
                Vector3 worldMouse =
                    Camera.main.ScreenToWorldPoint(new Vector3(inputpos.X, inputpos.Y,
                        Camera.main.nearClipPlane));
                Vector3 mouseNext = new Vector3(worldMouse.x, player.player.transform.position.y, worldMouse.z);
                // Implement mouse-based movement logic
                if (MenuManager.Instance.current.activeSelf == false)
                {
                    PlayerManager.Instance.menuOpen.GetComponent<CanvasGroup>().alpha = 1f;
                    PlayerManager.Instance.menuOpen.gameObject.SetActive(true);
                    float distance = mouseNext.x - player.player.transform.position.x;
                    player.SetFacingDirection(distance);
                    RaycastHit2D hit = Physics2D.Raycast(new Vector2(inputpos.X, inputpos.Y), Vector2.zero);
                    if ((hit.collider != null && hit.collider.gameObject == player.player) || player.newItem ||
                        player.beginBattle)
                    {
                        player.IsMoving = false;
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
                                new Vector2(400f, player.player.transform.position.y), Time.deltaTime * 50f);
                        }

                        if (Door.Instance.Opened == true && player.player.transform.position.x <
                            Door.Instance.transform.position.x + 100f)
                        {
                            player.player.transform.position = Vector3.MoveTowards(
                                player.player.transform.position,
                                new Vector2(Door.Instance.transform.position.x + 150f,
                                    player.player.transform.position.y), Time.deltaTime * player.speed);
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
        #endif
        }
    }
}