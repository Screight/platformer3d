using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D.Player
{
    public class PlayerJumpState : PlayerAirState
    {
        #region Jump
        float m_gravity_1;
        float m_initialSpeed_1;
        float m_gravity_2;
        float m_initialSpeed_2;
        float m_gravity_3;
        float m_initialSpeed_3;
        #endregion

        public PlayerJumpState(PlayerController p_controller, StateMachine p_stateMachine, ANIMATIONS p_animation) : base(p_controller, p_stateMachine, p_animation)
        {
            m_name = "Jump State";

            PlayerData playerData = PlayerController.PlayerData;

            m_gravity_1 = playerData.gravity_1;
            m_initialSpeed_1 = playerData.initialSpeed_1;
            m_gravity_2 = playerData.gravity_2;
            m_initialSpeed_2 = playerData.initialSpeed_3;
            m_gravity_3 = playerData.gravity_3;
            m_initialSpeed_3 = playerData.initialSpeed_3;
        }

        public override void Enter(bool p_changeToDefaultAnim)
        {
            base.Enter(p_changeToDefaultAnim);
            if (p_changeToDefaultAnim)
            {
                PlayerController.AnimatorHandler.PlayTargetAnimation(ANIMATIONS.JUMP);
            }
            m_verticalSpeed += m_initialSpeed_1;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if(m_verticalSpeed < 0)
            {
                m_controller.StateMachine.ChangeState(PlayerController.FallState);
            }
        }
    }
}
