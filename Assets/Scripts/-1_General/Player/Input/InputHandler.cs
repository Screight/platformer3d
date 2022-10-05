using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectD
{
    public class InputHandler : MonoBehaviour
    {
        private float m_moveAmount;
        Vector2 m_cameraInput;

        bool m_sprintButtonInput;
        bool m_rollButtonInput;

        PlayerControl m_inputActions;
        PlayerLocomotion m_playerLocomotion;

        Vector2 m_movementInput;

        int m_isInteractingHash;
        AnimatorHandler m_animatorHandler;

        #region Input Events
        //SPRINT
        UnityEvent m_sprintButtonPressed;
        UnityEvent m_sprintButtonReleased;
        public void AddListenerToSprintButtonPressed(UnityAction p_listener) { m_sprintButtonPressed.AddListener(p_listener); }
        public void RemoveListenerFromSprintButtonPressed(UnityAction p_listener) { m_sprintButtonPressed.RemoveListener(p_listener); }
        public void AddListenerToSprintButtonReleased(UnityAction p_listener) { m_sprintButtonReleased.AddListener(p_listener); }
        public void RemoveListenerFromSprintButtonReleased(UnityAction p_listener) { m_sprintButtonReleased.RemoveListener(p_listener); }
        // ROLL
        UnityEvent m_rollButtonPressed;
        UnityEvent m_rollButtonReleased;
        public void AddListenerToRollButtonPressed(UnityAction p_listener) { m_rollButtonPressed.AddListener(p_listener); }
        public void RemoveListenerFromRollButtonPressed(UnityAction p_listener) { m_rollButtonPressed.RemoveListener(p_listener); }
        public void AddListenerToRollButtonReleased(UnityAction p_listener) { m_rollButtonReleased.AddListener(p_listener); }
        public void RemoveListenerFromRollButtonReleased(UnityAction p_listener) { m_rollButtonReleased.RemoveListener(p_listener); }
        #endregion

        private void Awake()
        {
            m_sprintButtonPressed = new UnityEvent();
            m_sprintButtonReleased = new UnityEvent();
            m_rollButtonPressed = new UnityEvent();
            m_rollButtonReleased = new UnityEvent();
        }

        private void Start()
        {
            m_isInteractingHash = Animator.StringToHash("isInteracting");
            m_animatorHandler = GetComponentInChildren<AnimatorHandler>();
            m_playerLocomotion = FindObjectOfType<PlayerLocomotion>();
        }

        public void OnEnable()
        {
            if(m_inputActions == null) {
                m_inputActions = new PlayerControl();
                m_inputActions.PlayerMovement.Movement.performed += (m_inputActions) => { m_movementInput = m_inputActions.ReadValue<Vector2>(); };

                m_inputActions.PlayerMovement.Camera.performed += (m_inputActions) => { m_cameraInput = m_inputActions.ReadValue<Vector2>(); };

                // SPRINT INPUT
                m_inputActions.PlayerActions.Sprint.performed +=
                    (m_inputActions) => { m_sprintButtonInput = m_inputActions.ReadValueAsButton(); };
                m_inputActions.PlayerActions.Sprint.canceled +=
                    (m_inputActions) => { m_sprintButtonInput = m_inputActions.ReadValueAsButton(); };
                m_inputActions.PlayerActions.Sprint.performed += (m_inputActions) => { m_sprintButtonPressed.Invoke(); Debug.Log("Sprint button pressed"); };
                m_inputActions.PlayerActions.Sprint.canceled += (m_inputActions) => { m_sprintButtonReleased.Invoke(); };
                // ROLL INPUT
                m_inputActions.PlayerActions.Roll.performed +=
                    (m_inputActions) => { m_rollButtonInput = m_inputActions.ReadValueAsButton(); };
                m_inputActions.PlayerActions.Roll.canceled +=
                    (m_inputActions) => { m_rollButtonInput = m_inputActions.ReadValueAsButton(); };
                m_inputActions.PlayerActions.Roll.performed += (m_inputActions) => { m_rollButtonPressed.Invoke(); Debug.Log("Roll button pressed"); };
                m_inputActions.PlayerActions.Roll.canceled += (m_inputActions) => { m_rollButtonReleased.Invoke(); };
            }
            m_inputActions.Enable();
        }

        private void OnDisable()
        {
            m_inputActions.Disable();
        }

        private void Update()
        {
            TickInput();
        }

        public void TickInput()
        {
            HandleRollInput();
            HandleSprintInput();
        }

        void HandleRollInput()
        {
            if (m_rollButtonInput && !m_animatorHandler.IsInteracting)
            {
                m_playerLocomotion.RollFlag = true;
            }
        }

        void HandleSprintInput()
        {
            if (m_sprintButtonInput && !m_animatorHandler.IsInteracting)
            {
                m_playerLocomotion.SprintFlag = true;
            }
        }

        #region Accesors

        public float MoveAmount { get { return Mathf.Clamp01(Mathf.Abs(m_movementInput.x) + Mathf.Abs(m_movementInput.y)); } }
        public float VerticalInput { get { return m_movementInput.y; } }
        public float HorizontalInput { get { return m_movementInput.x; } }
        public Vector2 CameraInput { get { return new Vector2(m_cameraInput.x, m_cameraInput.y); } }
        public bool SprintInput { get { return m_sprintButtonInput; } }
        public bool RollInput { get { return m_rollButtonInput; } }

        #endregion

    }
}

