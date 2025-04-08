﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace TextMesh_Pro.Examples___Extras.Scripts
{
    
    public class TMPFrameRateCounter : MonoBehaviour
    {
        [FormerlySerializedAs("UpdateInterval")] public float updateInterval = 5.0f;
        private float _mLastInterval;
        private int _mFrames;

        public enum FpsCounterAnchorPositions { TopLeft, BottomLeft, TopRight, BottomRight };

        [FormerlySerializedAs("anchorPosition_c")] [FormerlySerializedAs("anchorPosition")] [FormerlySerializedAs("AnchorPosition")] public FpsCounterAnchorPositions anchorPositionC = FpsCounterAnchorPositions.TopRight;

        private string _htmlColorTag;
        private const string FPSLabel = "{0:2}</color> <#8080ff>FPS \n<#FF8000>{1:2} <#8080ff>MS";

        private TextMeshPro _mTextMeshPro;
        private Transform _mFrameCounterTransform;
        private Camera _mCamera;

        private FpsCounterAnchorPositions _lastAnchorPosition;

        private void Awake()
        {
            if (!enabled)
                return;

            _mCamera = Camera.main;
            Application.targetFrameRate = 9999;

            GameObject frameCounter = new GameObject("Frame Counter");

            _mTextMeshPro = frameCounter.AddComponent<TextMeshPro>();
            _mTextMeshPro.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
            _mTextMeshPro.fontSharedMaterial = Resources.Load<Material>("Fonts & Materials/LiberationSans SDF - Overlay");


            _mFrameCounterTransform = frameCounter.transform;
            if (_mCamera != null) _mFrameCounterTransform.SetParent(_mCamera.transform);
            _mFrameCounterTransform.localRotation = Quaternion.identity;

            _mTextMeshPro.enableWordWrapping = false;
            _mTextMeshPro.fontSize = 24;
            //m_TextMeshPro.FontColor = new Color32(255, 255, 255, 128);
            //m_TextMeshPro.edgeWidth = .15f;
            //m_TextMeshPro.isOverlay = true;

            //m_TextMeshPro.FaceColor = new Color32(255, 128, 0, 0);
            //m_TextMeshPro.EdgeColor = new Color32(0, 255, 0, 255);
            //m_TextMeshPro.FontMaterial.renderQueue = 4000;

            //m_TextMeshPro.CreateSoftShadowClone(new Vector2(1f, -1f));

            Set_FrameCounter_Position(anchorPositionC);
            _lastAnchorPosition = anchorPositionC;


        }

        private void Start()
        {
            _mLastInterval = Time.realtimeSinceStartup;
            _mFrames = 0;
        }

        private void Update()
        {
            if (anchorPositionC != _lastAnchorPosition)
                Set_FrameCounter_Position(anchorPositionC);

            _lastAnchorPosition = anchorPositionC;

            _mFrames += 1;
            float timeNow = Time.realtimeSinceStartup;

            if (timeNow > _mLastInterval + updateInterval)
            {
                // display two fractional digits (f2 format)
                float fps = _mFrames / (timeNow - _mLastInterval);
                float ms = 1000.0f / Mathf.Max(fps, 0.00001f);

                if (fps < 30)
                    _htmlColorTag = "<color=yellow>";
                else if (fps < 10)
                    _htmlColorTag = "<color=red>";
                else
                    _htmlColorTag = "<color=green>";

                //string format = System.String.Format(htmlColorTag + "{0:F2} </color>FPS \n{1:F2} <#8080ff>MS",fps, ms);
                //m_TextMeshPro.text = format;

                _mTextMeshPro.SetText(_htmlColorTag + FPSLabel, fps, ms);

                _mFrames = 0;
                _mLastInterval = timeNow;
            }
        }


        private void Set_FrameCounter_Position(FpsCounterAnchorPositions anchorPosition)
        {
            //Debug.Log("Changing frame counter anchor position.");
            _mTextMeshPro.margin = new Vector4(1f, 1f, 1f, 1f);

            switch (anchorPosition)
            {
                case FpsCounterAnchorPositions.TopLeft:
                    _mTextMeshPro.alignment = TextAlignmentOptions.TopLeft;
                    _mTextMeshPro.rectTransform.pivot = new Vector2(0, 1);
                    _mFrameCounterTransform.position = _mCamera.ViewportToWorldPoint(new Vector3(0, 1, 100.0f));
                    break;
                case FpsCounterAnchorPositions.BottomLeft:
                    _mTextMeshPro.alignment = TextAlignmentOptions.BottomLeft;
                    _mTextMeshPro.rectTransform.pivot = new Vector2(0, 0);
                    _mFrameCounterTransform.position = _mCamera.ViewportToWorldPoint(new Vector3(0, 0, 100.0f));
                    break;
                case FpsCounterAnchorPositions.TopRight:
                    _mTextMeshPro.alignment = TextAlignmentOptions.TopRight;
                    _mTextMeshPro.rectTransform.pivot = new Vector2(1, 1);
                    _mFrameCounterTransform.position = _mCamera.ViewportToWorldPoint(new Vector3(1, 1, 100.0f));
                    break;
                case FpsCounterAnchorPositions.BottomRight:
                    _mTextMeshPro.alignment = TextAlignmentOptions.BottomRight;
                    _mTextMeshPro.rectTransform.pivot = new Vector2(1, 0);
                    _mFrameCounterTransform.position = _mCamera.ViewportToWorldPoint(new Vector3(1, 0, 100.0f));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(anchorPosition), anchorPosition, null);
            }
        }
    }
}
