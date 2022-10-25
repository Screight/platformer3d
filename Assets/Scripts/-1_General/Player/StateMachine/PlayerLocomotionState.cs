using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D.Player
{
    public class PlayerLocomotionState : PlayerGroundState
    {
        float m_timeToStartAccelerating = 0.6f;
        float m_timeToReachMaxSpeed = 0.9f;
        float m_maxSpeed = 5;
        float m_currentSpeed = 0;

        float m_acceleration;

        public PlayerLocomotionState(PlayerController p_controller, StateMachine p_stateMachine, ANIMATIONS p_animation) : base(p_controller, p_stateMachine, "Locomotion State", p_animation)
        {
            m_controller = p_controller;
            m_acceleration = m_maxSpeed / m_timeToReachMaxSpeed;
        }

        public override void Enter(bool p_changeToDefaultAnim)
        {
            base.Enter(p_changeToDefaultAnim);
            m_currentSpeed = 0;
            m_state = STATE.ENTER;
        }

        enum STATE { ENTER, STILL, ACCELERATING, FINISHED}
        STATE m_state;

        public override void LogicUpdate()
        {
            float deltaTime = Time.deltaTime;
            PlayerData playerData = PlayerController.PlayerData;
            float inputMagnitude = PlayerInputHandler.Instance.MovementInput.magnitude;

            #region Movement

            Vector2 input = PlayerInputHandler.Instance.MovementInput;

            if (m_state != STATE.FINISHED) { HandleInitialAcceleration(deltaTime, ref input); }

            m_movementBehaviour.HandleMovement(deltaTime, playerData, input);
            PlayerController.AnimatorHandler.UpdateAnimatorValues(inputMagnitude, PlayerController.PlayerData.walToRunAxisTransition);

            m_movementBehaviour.HandleRotation(deltaTime, playerData);

            #endregion

            #region Exit Condition
            if (inputMagnitude < 0.001f)
            {
                m_stateMachine.ChangeState(PlayerController.IdleState, false);

                if(PlayerController.AnimatorHandler.Movement >= 0.5 && m_state == STATE.FINISHED) {
                    m_controller.AnimatorHandler.PlayTargetAnimation(ANIMATIONS.RUN_TO_STOP);
                }
                else {
                    m_controller.AnimatorHandler.PlayTargetAnimation(ANIMATIONS.IDLE);
                }

                return;
            }
            #endregion

        }

        private void HandleInitialAcceleration(float p_deltaTime, ref Vector2 input)
        {
            float timeFromStart = Time.time - m_startTime;

            switch (m_state)
            {
                case STATE.ENTER: {
                        m_controller.AnimatorHandler.PlayTargetAnimation(ANIMATIONS.RUN_PREPARATION);
                        m_state = STATE.STILL;
                    }
                    break;
                case STATE.STILL:
                    {
                        if (timeFromStart > m_timeToStartAccelerating) {
                            m_state = STATE.ACCELERATING;
                            m_controller.AnimatorHandler.PlayTargetAnimation(ANIMATIONS.LOCOMOTION);
                        }
                        else { m_currentSpeed += m_acceleration * p_deltaTime; }
                    }
                    break;
                case STATE.ACCELERATING:
                    {
                        if (timeFromStart > m_timeToReachMaxSpeed) {
                            m_state = STATE.FINISHED;
                            m_currentSpeed = m_maxSpeed;
                        }
                        else { m_currentSpeed += m_acceleration * p_deltaTime; }
                    }
                    
                    break;
            }
        }

    }
}
