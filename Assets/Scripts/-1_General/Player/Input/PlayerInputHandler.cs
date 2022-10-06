using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer3D
{
    public class PlayerInputHandler : MonoBehaviour
    {
        PlayerControl m_inputActions;
        Vector2 m_movementInput;
        Vector2 m_cameraInput;

        private void OnEnable()
        {
            if (m_inputActions != null) return;
            m_inputActions = new PlayerControl();

            m_inputActions.PlayerMovement.Movement.performed += (m_inputActions) => { m_movementInput = m_inputActions.ReadValue<Vector2>(); };
            m_inputActions.PlayerMovement.Camera.performed += (m_inputActions) => { m_cameraInput = m_inputActions.ReadValue<Vector2>(); };

            m_inputActions.Enable();
        }

        private void OnDisable()
        {
            m_inputActions.Disable();
        }

        public Vector2 MovementInput { get { return m_movementInput; } }
        public Vector2 CameraInput { get { return m_cameraInput; } }

    }
}

