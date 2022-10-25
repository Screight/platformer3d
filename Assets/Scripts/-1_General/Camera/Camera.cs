using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    public class Camera : MonoBehaviour
    {
        float m_rotationX;
        float m_rotationY;
        [SerializeField] float m_targetDistance;
        [SerializeField] float m_rotationSpeed;
        [SerializeField] GameObject m_target;
        [SerializeField] float m_cameraOpening;

        [SerializeField] float m_cameraLerp;

        Transform m_cameraTransform;

        private void Awake()
        {
            m_cameraTransform = transform;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void LateUpdate()
        {
            float deltaTime = Time.deltaTime;

            Rotate();
            Vector3 finalPosition = m_target.transform.position - m_cameraTransform.forward * m_targetDistance;
            transform.position = Vector3.Slerp(transform.position, finalPosition, m_cameraLerp * deltaTime);
        }

        void Rotate()
        {
            float deltaTime = Time.deltaTime;

            Vector2 cameraInput = PlayerInputHandler.Instance.CameraInput;

            //m_rotationX += mouseInputY * m_rotationSpeed * deltaTime;
            //m_rotationY += mouseInputX * m_rotationSpeed * deltaTime;
            Debug.Log(cameraInput);
            m_rotationX += -cameraInput.y;
            m_rotationY += cameraInput.x;

            m_rotationX = Mathf.Clamp(m_rotationX, -m_cameraOpening, m_cameraOpening);

            Vector3 finalRotation;
            finalRotation.x = m_rotationX;
            finalRotation.y = m_rotationY;
            finalRotation.z = m_cameraTransform.eulerAngles.z;

            m_cameraTransform.eulerAngles = finalRotation;
            return;

            //m_cameraTransform.eulerAngles = new Vector3(m_rotationX, m_rotationY, 0);

        }

    }

}