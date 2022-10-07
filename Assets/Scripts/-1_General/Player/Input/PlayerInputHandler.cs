using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Platformer3D
{
    public class PlayerInputHandler : Singleton<PlayerInputHandler>
    {
        #region Events
        UnityEvent m_jumpEvent;
        public void AddListenerToJumpButtonPressed(UnityAction p_action)
        {
            m_jumpEvent.AddListener(p_action);
        }
        public void RemoveListenerFromJumpButtonPressed(UnityAction p_action)
        {
            m_jumpEvent.RemoveListener(p_action);
        }
        #endregion

        PlayerControl m_inputActions;
        Vector2 m_movementInput;
        Vector2 m_cameraInput;


        private void Awake()
        {
            m_jumpEvent = new UnityEvent();
        }

        private void Update()
        {
            InputSystem.Update();
        }

        private void OnEnable()
        {
            if (m_inputActions != null) return;
            m_inputActions = new PlayerControl();

            m_inputActions.PlayerMovement.Movement.performed += (m_inputActions) => { m_movementInput = m_inputActions.ReadValue<Vector2>(); };
            m_inputActions.PlayerMovement.Camera.performed += (m_inputActions) => { m_cameraInput = m_inputActions.ReadValue<Vector2>(); };

            m_inputActions.PlayerActions.Jump.performed += JumpButtonPressedCallback;
            m_inputActions.Enable();
        }

        private void OnDisable()
        {
            m_inputActions.Disable();
            m_jumpEvent.RemoveAllListeners();
            m_inputActions.PlayerActions.Jump.performed -= JumpButtonPressedCallback;
        }

        private void JumpButtonPressedCallback(InputAction.CallbackContext p_context)
        {
            m_jumpEvent.Invoke();
        }

        public Vector2 MovementInput { get { return m_movementInput; } }
        public Vector2 CameraInput { get { return m_cameraInput; } }

    }
}

