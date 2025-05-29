using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Eyeware.BeamEyeTracker;
using Eyeware.BeamEyeTracker.Unity;

namespace DefaultNamespace.HUD
{
    public class MeniuHUDBehaviour:BeamEyeTrackerMonoBehaviour
    {
        public enum OnScreenPositionM
        {
            TopLeft,
            TopMiddle,
            TopRight,
            SemiTopLeft,
            SemiTopRight,
            MiddleLeft,
            MiddleMiddle,
            MiddleRight,
            SemiBottomLeft,
            SemiBottomRight,
            BottomLeft,
            BottomMiddle,
            BottomRight,
        }

        public static readonly IReadOnlyDictionary<OnScreenPositionM
            , Vector2> VIEWPORT_COORDINATES = new Dictionary<OnScreenPositionM, Vector2>
        {
            { OnScreenPositionM.TopLeft ,new Vector2(0.0f,1.0f)},
            { OnScreenPositionM.TopMiddle,new Vector2(0.5f,1.0f)},
            { OnScreenPositionM.TopRight,new Vector2(1.0f,1.0f)},
            { OnScreenPositionM.SemiTopLeft,new Vector2(0.25f,0.75f)},
            { OnScreenPositionM.SemiTopRight,new Vector2(0.75f,0.75f)},
            { OnScreenPositionM.MiddleLeft,new Vector2(0.0f,0.5f)},
            { OnScreenPositionM.MiddleMiddle,new Vector2(0.5f,0.5f)},
            { OnScreenPositionM.MiddleRight,new Vector2(1.0f,0.5f)},
            { OnScreenPositionM.SemiBottomLeft,new Vector2(0.25f,0.25f)},
            { OnScreenPositionM.SemiBottomRight , new Vector2(0.25f,0.75f)},
            { OnScreenPositionM.BottomLeft,new Vector2(0.0f,0.0f)},
            { OnScreenPositionM.BottomMiddle ,new Vector2(0.5f,0.0f)},
            { OnScreenPositionM.BottomRight,new Vector2(1.0f,0.0f)},
        };
        // Config
        public float fadeOutSpeed = 2f;
        public float fadeInSpeed = 10f;
        public float minOpacity = 0.2f;

        // Unity components cached for performance
        private CanvasGroup canvasGroup;
        private Canvas canvas;
        private RectTransform rectTransform;

        // State
        private bool isFadingOut = false;
        private bool isFadingIn = false;

        private bool isSelected = false;
        public bool IsSelected
        {
            get => isSelected;
            set=> isSelected = value;
        }
        void Start()
        {
            // Get the CanvasGroup component or add one if not present
            canvasGroup = GetComponent<CanvasGroup>();
            
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
            // Set initial opacity to fully visible
            canvasGroup.alpha = 1f;
            canvas = GetComponentInParent<Canvas>();
            rectTransform = GetComponent<RectTransform>();
        }
           void Update()
        {
            if (betInputDevice == null)
            {
                return;
            }
            OnScreenPositionM onScreenPosition = getScreenPositionForThisElement();
            if (onScreenPosition == OnScreenPositionM.MiddleMiddle)
            {
                return;
            }

            float likelihoodOfUserLookingThis = 1.0f;

            switch (onScreenPosition)
            {
                case OnScreenPositionM.TopLeft:
                    likelihoodOfUserLookingThis =
                        betInputDevice.isLookingAtTopLeftCorner.ReadValue();
                    break;
                case OnScreenPositionM.TopMiddle:
                    likelihoodOfUserLookingThis=betInputDevice.isLookingAtTopMiddle.ReadValue();
                    break;
                case OnScreenPositionM.TopRight:
                    likelihoodOfUserLookingThis=
                    betInputDevice.isLookingAtTopRightCorner.ReadValue();
                    break;
                case OnScreenPositionM.SemiTopLeft:
                    likelihoodOfUserLookingThis=
                        betInputDevice.isLookingAtSemiTopLeftCorner.ReadValue();
                    break;
                case OnScreenPositionM.SemiTopRight:
                    likelihoodOfUserLookingThis=
                        betInputDevice.isLookingAtSemiTopRightCorner.ReadValue();
                    break;
                case OnScreenPositionM.MiddleLeft:
                    likelihoodOfUserLookingThis=
                        betInputDevice.isLookingAtCenterLeft.ReadValue();
                    break;
                case OnScreenPositionM.MiddleMiddle:
                    likelihoodOfUserLookingThis=
                        betInputDevice.isLookingAtMiddleCenter.ReadValue();
                    break;
                case OnScreenPositionM.MiddleRight:
                    likelihoodOfUserLookingThis=
                        betInputDevice.isLookingAtCenterRight.ReadValue();
                    break;
                case OnScreenPositionM.SemiBottomLeft:
                    likelihoodOfUserLookingThis=
                        betInputDevice.isLookingAtSemiBottomLeftCorner.ReadValue();
                    break;
                case OnScreenPositionM.SemiBottomRight:
                    likelihoodOfUserLookingThis=
                        betInputDevice.isLookingAtSemiBottomRightCorner.ReadValue();
                    break;
                case OnScreenPositionM.BottomLeft:
                    likelihoodOfUserLookingThis=
                        betInputDevice.isLookingAtBottomLeftCorner.ReadValue();
                    break;
                case OnScreenPositionM.BottomMiddle:
                    likelihoodOfUserLookingThis=
                        betInputDevice.isLookingAtBottomMiddle.ReadValue();
                    break;
                case OnScreenPositionM.BottomRight:
                    likelihoodOfUserLookingThis=
                        betInputDevice.isLookingAtBottomRightCorner.ReadValue();
                    break;
            }

            bool isLookingAtThis = likelihoodOfUserLookingThis > 0.5f;

            if (betInputDevice.trackingStatus.ReadValue() != 1)
            {
                // When there is no data coming from the eye tracker, we assume the user is
                // looking at the element as that is the safe choice from a UX perspective.
                isLookingAtThis = true;
            }

            if (!isLookingAtThis)
            {
                // Start fading out if not already fading out
                if (!isFadingOut)
                {
                    isFadingOut = true;
                    isFadingIn = false;
                    //StopAllCoroutines();
                    IsSelected = false;
                  StartCoroutine(FadeTo(minOpacity, fadeOutSpeed));
                }
            }
            else
            {
                // Start fading in if not already fading in
                if (!isFadingIn)
                {
                    isFadingIn = true;
                    isFadingOut = false;
                    //StopAllCoroutines();
                    isSelected = true;
                   StartCoroutine(FadeTo(1f, fadeInSpeed));
                }
            }
        }

        private OnScreenPositionM getScreenPositionForThisElement()
        {
            // We calculate the center of the element referred to the screen space, whose approach
            // depends on the canvas' render mode
            Vector2 screenSpaceCenter = new Vector2(0, 0);
            if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                Vector3[] worldCorners = new Vector3[4];
                rectTransform.GetWorldCorners(worldCorners);
                // add up opposite corners to later get the mean/center of the element
                screenSpaceCenter = new Vector2(
                    worldCorners[0].x + worldCorners[2].x,
                    worldCorners[0].y + worldCorners[2].y
                );
            }
            else
            {
                Vector3[] localCorners = new Vector3[4];
                rectTransform.GetLocalCorners(localCorners);
                // Get the camera to use according to the canvas' render mode
                Camera canvasCamera;
                if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
                {
                    canvasCamera = canvas.worldCamera;
                }
                else
                {
                    canvasCamera = Camera.main;
                }
                if (canvasCamera == null)
                {
                    return OnScreenPositionM.MiddleMiddle;
                }

                // Add the top left and bottom right corners
                foreach (var i in new[] { 0, 2 })
                {
                    Vector3 worldPos = rectTransform.TransformPoint(localCorners[i]);
                    Vector3 screenPoint = canvasCamera.WorldToScreenPoint(worldPos);
                    screenSpaceCenter += new Vector2(screenPoint.x, screenPoint.y);
                }
            }
            screenSpaceCenter /= 2f; // calculate middle point

            // We calculate the center of the element within the -normalized- screen space
            Vector2 viewportSpaceCenter = new Vector2(
                screenSpaceCenter[0] / Screen.width,
                screenSpaceCenter[1] / Screen.height
            );

            // We calculate the distance of the center of the element to each of the corners of the screen
            var distances = Enum.GetValues(typeof(OnScreenPositionM))
                .Cast<OnScreenPositionM>()
                .ToDictionary(
                    position => position,
                    position =>
                        Vector2.Distance(viewportSpaceCenter, VIEWPORT_COORDINATES[position])
                );

            return distances.OrderBy(d => d.Value).First().Key;
        }

        private System.Collections.IEnumerator FadeTo(float targetAlpha, float speed)
        {
            // Smoothly transition to the target alpha
            while (!Mathf.Approximately(canvasGroup.alpha, targetAlpha))
            {
                canvasGroup.alpha = Mathf.MoveTowards(
                    canvasGroup.alpha,
                    targetAlpha,
                    speed * Time.deltaTime
                );
                yield return null;
            }
        }
    }
}