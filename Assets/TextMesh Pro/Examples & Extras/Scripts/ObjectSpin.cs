﻿using UnityEngine;
using UnityEngine.Serialization;

namespace TextMesh_Pro.Examples___Extras.Scripts
{
    
    public class ObjectSpin : MonoBehaviour
    {

#pragma warning disable 0414

        [FormerlySerializedAs("SpinSpeed")] public float spinSpeed = 5;
        [FormerlySerializedAs("RotationRange")] public int rotationRange = 15;
        private Transform _mTransform;

        private float _mTime;
        private Vector3 _mInitialRotation;
        private Vector3 _mInitialPosition;
        private int Frames { get; set; }

        public enum MotionType { Rotation, BackAndForth, Translation };
        [FormerlySerializedAs("Motion")] public MotionType motion;

        private void Awake()
        {
            _mTransform = transform;
            _mInitialRotation = _mTransform.rotation.eulerAngles;
            _mInitialPosition = _mTransform.position;

            //var _l = GetComponent<Light>();
        }


        // Update is called once per frame
        private void Update()
        {
            if (motion == MotionType.Rotation)
            {
                _mTransform.Rotate(0, spinSpeed * Time.deltaTime, 0);
            }
            else if (motion == MotionType.BackAndForth)
            {
                _mTime += spinSpeed * Time.deltaTime;
                _mTransform.rotation = Quaternion.Euler(_mInitialRotation.x, Mathf.Sin(_mTime) * rotationRange + _mInitialRotation.y, _mInitialRotation.z);
            }
            else
            {
                _mTime += spinSpeed * Time.deltaTime;

                float x = 15 * Mathf.Cos(_mTime * .95f);
                float y = 10; // *Mathf.Sin(m_time * 1f) * Mathf.Cos(m_time * 1f);
                float z = 0f; // *Mathf.Sin(m_time * .9f);    

                _mTransform.position = _mInitialPosition + new Vector3(x, z, y);

                // Drawing light patterns because they can be cool looking.
                //if (frames > 2)
                //    Debug.DrawLine(m_transform.position, m_prevPOS, m_lightColor, 100f);

                Frames += 1;
            }
        }
    }
}