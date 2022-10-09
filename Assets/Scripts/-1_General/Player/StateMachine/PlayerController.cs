using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D.Player
{
    public class PlayerController : Controller
    {
        #region State Variables
        [SerializeField] PlayerData m_playerData;

        PlayerIdleState m_idleState;
        PlayerLocomotionState m_locomotionState;
        PlayerFallState m_fallState;
        PlayerJumpState m_jumpState;

        float m_gravityDefault;
        int m_jumpCount = 0;
        const int MAX_NUMBER_OF_JUMPS = 3;
        #endregion

        #region Unity Callback Functions
        protected override void Awake()
        {
            base.Awake();

            m_characterController = GetComponent<CharacterController>();

            m_stateMachine = new StateMachine();

            m_idleState = new PlayerIdleState(this, m_stateMachine, ANIMATIONS.LOCOMOTION);
            m_locomotionState = new PlayerLocomotionState(this, m_stateMachine, ANIMATIONS.LOCOMOTION);
            m_fallState = new PlayerFallState(this, m_stateMachine, ANIMATIONS.FALLING);
            m_jumpState = new PlayerJumpState(this, m_stateMachine, ANIMATIONS.JUMP);

            m_gravityDefault = m_playerData.gravity_1;

        }
        private void Start()
        {
            m_stateMachine.Initialize(m_fallState, true);
        }

        protected override void Update()
        {
            base.Update();
            m_stateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            m_stateMachine.CurrentState.PhysicsUpdate();
        }
        #endregion

        #region Accessors

        #region States
        public PlayerIdleState IdleState { get { return m_idleState; } }
        public PlayerLocomotionState LocomotionState { get { return m_locomotionState; } }
        public PlayerFallState FallState { get { return m_fallState; } }
        public PlayerJumpState JumpState { get { return m_jumpState; } }
        #endregion

        public PlayerData PlayerData
        {
            get { return m_playerData; }
        }

        public new PlayerAnimatorHandler AnimatorHandler
        {
            get { return m_animatorHandler as PlayerAnimatorHandler; }
        }

        public float GravityDefault { 
            get { return m_gravityDefault; }
            private set { }
        }


        public int MaxNumberOfJumps { get { return MAX_NUMBER_OF_JUMPS; } }
        public int JumpCount
        {
            get { return m_jumpCount; }
            set
            {
                int result = value;
                if(value < 0) { result = 0; }
                else if(value >= MAX_NUMBER_OF_JUMPS) { result = MAX_NUMBER_OF_JUMPS; }
                else
                {
                    result = value;
                }
                m_jumpCount = result;
            }
        }

        #endregion
    }
}

