using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectD
{
    public class CameraHandler : Singleton<CameraHandler>
    {
        [SerializeField] Transform m_targetTransform;
        [SerializeField] Transform m_cameraTransform;
        [SerializeField] Transform m_cameraPivotTransform;

        Vector3 m_cameraTransformPosition;
        Transform m_myTransform;

        LayerMask m_ignoreLayers;
        Vector3 m_cameraVelocity = Vector3.zero;

        [SerializeField] float m_lookSpeed = 0.1f;
        [SerializeField] float m_followSpeed = 0.1f;
        [SerializeField] float m_pivotSpeed = 0.03f;
        
        float m_lookAngle;
        float m_pivotAngle;
        [SerializeField] float m_maximumPivot = 35;
        [SerializeField] float m_minimumPivot = -35;

        InputHandler m_inputHandler;

        // Camera Collision
        float m_defaultPosition;
        float m_targetPosition;
        [SerializeField] float m_cameraSphereRadius = 0.2f;
        float m_cameraCollisionOffSet = 0.2f;
        float m_minimumCollisionOffset = 0.2f;

        protected override void Awake()
        {
            base.Awake();

            m_inputHandler = FindObjectOfType<InputHandler>();

            m_myTransform = transform;
            m_defaultPosition = m_cameraTransform.localPosition.z;
            m_ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            FollowTarget(delta);
            Vector2 cameraInput = m_inputHandler.CameraInput;
            HandleCameraRotation(delta, cameraInput.x, cameraInput.y);
        }

        // TODO:
        // Let the player adjust the camera sensibility
        public void FollowTarget(float p_delta)
        {
            Vector3 targetPosition = Vector3.SmoothDamp(m_myTransform.position, m_targetTransform.position, ref m_cameraVelocity, p_delta / m_followSpeed);
            m_myTransform.position = targetPosition;

            HandleCameraCollisions(p_delta);
        }

        public void HandleCameraRotation(float p_delta, float p_mouseXInput, float p_mouseYInput)
        {
            m_lookAngle += (p_mouseXInput * m_lookSpeed) / p_delta;

            m_pivotAngle -= (p_mouseYInput * m_pivotSpeed) / p_delta;
            m_pivotAngle = Mathf.Clamp(m_pivotAngle, m_minimumPivot, m_maximumPivot);

            Vector3 rotation = Vector3.zero;
            rotation.y = m_lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            m_myTransform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = m_pivotAngle;
            targetRotation = Quaternion.Euler(rotation);
            m_cameraPivotTransform.localRotation = targetRotation;
        }

        private void HandleCameraCollisions(float p_delta)
        {
            m_targetPosition = m_defaultPosition;
            RaycastHit hit;
            Vector3 direction = m_cameraTransform.position - m_cameraPivotTransform.position;
            direction.Normalize();

            if (Physics.SphereCast( m_cameraPivotTransform.position,                 
                                    m_cameraSphereRadius, 
                                    direction, out hit, 
                                    Mathf.Abs(m_targetPosition),
                                    m_ignoreLayers))
            {
                float dis = Vector3.Distance(m_cameraPivotTransform.position, hit.point);
                m_targetPosition = -(dis - m_cameraCollisionOffSet);
            }

            if (Mathf.Abs(m_targetPosition) < m_minimumCollisionOffset)
            {
                m_targetPosition = -m_minimumCollisionOffset;
            }

            m_cameraTransformPosition.z = Mathf.Lerp(m_cameraTransform.localPosition.z, m_targetPosition, p_delta / 0.2f);
            m_cameraTransform.localPosition = m_cameraTransformPosition;
        }

    }
}

