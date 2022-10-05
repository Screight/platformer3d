using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectD
{
    public class PlayerController : MonoBehaviour
    {

        #region Components
        BoxCollider2D m_boxCollider;
        #endregion

        #region State Variables
        [SerializeField] PlayerData m_playerData;
        PlayerStateMachine m_stateMachine;
        PlayerIdleState m_idleState;
        PlayerRunState m_runState;
        PlayerSprintState m_sprintState;
        PlayerRollState m_rollState;
        #endregion

        #region Other Variables
        Vector2 m_workSpace;
        Vector2 m_currentSpeed;
        int m_facingDirection;
        #endregion

        AnimatorHandler m_animatorHandler;
        InputHandler m_inputHandler;

        Rigidbody m_rigidbody;

        Transform m_cameraObject;

        #region Unity Callback Functions
        private void Awake()
        {
            m_cameraObject = Camera.main.transform;
            m_rigidbody = GetComponent<Rigidbody>();
            
            m_animatorHandler = GetComponentInChildren<AnimatorHandler>();
            m_inputHandler = GetComponent<InputHandler>();

            m_stateMachine = new PlayerStateMachine();

            m_idleState = new PlayerIdleState(this, m_stateMachine, m_playerData, ANIMATIONS.LOCOMOTION);
            m_runState = new PlayerRunState(this, m_stateMachine, m_playerData, ANIMATIONS.LOCOMOTION);
            m_sprintState = new PlayerSprintState(this, m_stateMachine, m_playerData, ANIMATIONS.LOCOMOTION);
            m_rollState = new PlayerRollState(this, m_stateMachine, m_playerData, ANIMATIONS.ROLLING);
        }
        private void Start()
        {
            m_stateMachine.Initialize(m_idleState);
            m_facingDirection = 1;
        }

        private void Update()
        {
            m_stateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            m_stateMachine.CurrentState.PhysicsUpdate();
        }
        #endregion

        #region Getters and Setters
        public AnimatorHandler AnimatorHandler
        {
            get { return m_animatorHandler; }
        }
        public InputHandler InputHandler
        {
            get { return m_inputHandler; }
        }
        public PlayerIdleState IdleState
        {
            get { return m_idleState; }
            set {}
        }
        public PlayerRunState RunState
        {
            get { return m_runState; }
            private set {}
        }
        public PlayerSprintState SprintState
        {
            get { return m_sprintState; }
            private set { }
        }
        public PlayerRollState RollState
        {
            get { return m_rollState; }
            private set { }
        }

        public Vector3 Velocity
        {
            get { return m_rigidbody.velocity; }
            set { m_rigidbody.velocity = value;}
        }

        public Transform CameraTransform
        {
            get { return m_cameraObject; }
            set { }
        }

        #endregion

    }
}

