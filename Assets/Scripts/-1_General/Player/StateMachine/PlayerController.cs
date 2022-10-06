using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D.Player
{
    public class PlayerController : Controller
    {

        #region State Variables
        [SerializeField] PlayerData m_playerData;
        StateMachine m_stateMachine;

        PlayerIdleState m_idleState;
        PlayerLocomotionState m_locomotionState;
        PlayerFallState m_fallState;

        #endregion

        PlayerAnimatorHandler m_animatorHandler;
        PlayerInputHandler m_inputHandler;

        #region Unity Callback Functions
        private void Awake()
        {

            m_animatorHandler = GetComponentInChildren<PlayerAnimatorHandler>();
            m_inputHandler = GetComponent<PlayerInputHandler>();
            m_characterController = GetComponent<CharacterController>();

            m_stateMachine = new StateMachine();

            m_idleState = new PlayerIdleState(this, m_stateMachine, ANIMATIONS.LOCOMOTION);
            m_locomotionState = new PlayerLocomotionState(this, m_stateMachine, ANIMATIONS.LOCOMOTION);
            m_fallState = new PlayerFallState(this, m_stateMachine, ANIMATIONS.FALLING);
        }
        private void Start()
        {
            m_stateMachine.Initialize(m_idleState);
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

        #region Accessors

        public PlayerIdleState IdleState { get { return m_idleState; } }
        public PlayerLocomotionState LocomotionState { get { return m_locomotionState; } }
        public PlayerFallState PlayerFallState { get { return m_fallState; } }

        public PlayerInputHandler InputHandler
        {
            get { return m_inputHandler; }
        }

        public PlayerData PlayerData
        {
            get { return m_playerData; }
        }

        public PlayerAnimatorHandler AnimatorHandler
        {
            get { return m_animatorHandler; }
        }

        #endregion
    }
}

