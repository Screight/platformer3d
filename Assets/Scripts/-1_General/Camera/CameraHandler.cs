using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    public class CameraHandler : Singleton<CameraHandler>
    {
        // Provisional, for more complex behaviour use a separate script
        float m_yaw;
        float m_pitch;
        float m_yawSpeed;
        float m_pitchSpeed;
        float m_followSpeed;

        float m_maximumPivot;
        float m_minimumPivot;

        [SerializeField] CameraData m_data;
        PlayerInputHandler m_inputHandler;

        Transform m_cameraTransform;
        [SerializeField] Transform m_pivotTransform;

        [SerializeField] Transform m_target;
        Vector3 m_currentVelocity;

        protected override void Awake()
        {
            base.Awake();
            m_cameraTransform = transform;

            m_yawSpeed = m_data.yawSpeedCamera;
            m_pitchSpeed = m_data.pitchSpeedCamera;
            m_minimumPivot = m_data.minimumPivot;
            m_maximumPivot = m_data.maximumPivot;
            m_followSpeed = m_data.followSpeed;

            m_inputHandler = FindObjectOfType<PlayerInputHandler>();
        }

        private void Update()
        {
            float delta = Time.deltaTime;
            HandleCameraMovement(delta);
            FollowTarget();
        }

        private void FollowTarget()
        {
            transform.position = Vector3.SmoothDamp(transform.position, m_target.position, ref m_currentVelocity, 1/m_followSpeed);
        }

        private void HandleCameraMovement(float p_delta)
        {
            Vector2 input = m_inputHandler.CameraInput;

            m_yaw += (input.x * m_yawSpeed) * p_delta;
            m_pitch -= (input.y * m_pitchSpeed) * p_delta;
            m_pitch = Mathf.Clamp(m_pitch, m_minimumPivot, m_maximumPivot);

            Vector3 rotation = Vector3.zero;
            rotation.y = m_yaw;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            m_cameraTransform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = m_pitch;
            targetRotation = Quaternion.Euler(rotation);
            m_pivotTransform.localRotation = targetRotation;
        }

        #region Accessors

        public float Pitch {  get { return m_pitch; } }
        public float Yaw {  get { return m_yaw; } }

        #endregion

    }
}

