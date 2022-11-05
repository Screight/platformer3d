using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D.Player
{
    public class PlayerLocomotionState : PlayerGroundState
    {
        float m_currentSpeed = 0;
        bool m_isAccelerating = false;

        public PlayerLocomotionState(PlayerController p_controller, StateMachine p_stateMachine, ANIMATIONS p_animation) : base(p_controller, p_stateMachine, "Locomotion State", p_animation)
        {
            m_controller = p_controller;
        }

        public override void Enter(bool p_changeToDefaultAnim)
        {
            base.Enter(p_changeToDefaultAnim);
            m_currentSpeed = 0;
            m_state = STATE.ENTER;
            m_movementBehaviour.ResetSpeed();
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

            m_movementBehaviour.HandleMovement(deltaTime, playerData, input, m_isAccelerating);

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

        public bool IsAccelerating { set{ m_isAccelerating = value; } }
    }
}
