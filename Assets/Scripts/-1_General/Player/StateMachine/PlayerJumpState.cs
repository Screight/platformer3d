using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D.Player
{

    public class PlayerJumpState : PlayerAirState
    {
        #region Jump

        JumpInfo[] m_jumpInfoArray;

        #endregion

        public PlayerJumpState(PlayerController p_controller, StateMachine p_stateMachine, ANIMATIONS p_animation) : base(p_controller, p_stateMachine, p_animation)
        {
            m_name = "Jump State";

            m_jumpInfoArray = new JumpInfo[3];

            PlayerData playerData = PlayerController.PlayerData;

            // NOT DYNAMIC
            m_jumpInfoArray[0].gravity = playerData.gravity_1;
            m_jumpInfoArray[0].initialSpeed = playerData.initialSpeed_1;
            m_jumpInfoArray[0].animation = ANIMATIONS.JUMP;

            m_jumpInfoArray[1].gravity = playerData.gravity_2;
            m_jumpInfoArray[1].initialSpeed = playerData.initialSpeed_2;
            m_jumpInfoArray[1].animation = ANIMATIONS.JUMP_2;

            m_jumpInfoArray[2].gravity = playerData.gravity_3;
            m_jumpInfoArray[2].initialSpeed = playerData.initialSpeed_3;
            m_jumpInfoArray[2].animation = ANIMATIONS.JUMP_3;
        }

        public override void Enter(bool p_changeToDefaultAnim)
        {
            base.Enter(p_changeToDefaultAnim);

            PlayerController.JumpCount++;
            JumpInfo jumpInfo = m_jumpInfoArray[PlayerController.JumpCount - 1];

            m_gravity = jumpInfo.gravity;
            m_verticalSpeed += jumpInfo.initialSpeed;
            m_controller.AnimatorHandler.PlayTargetAnimation(jumpInfo.animation);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if(m_verticalSpeed < 0)
            {
                m_controller.StateMachine.ChangeState(PlayerController.FallState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            if(PlayerController.JumpCount == PlayerController.MaxNumberOfJumps)
            {
                PlayerController.JumpCount = 0;
            }
        }

    }
}
