using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D.Player
{

    public class PlayerJumpState : PlayerAirState
    {
        #region Jump

        JumpInfo[] m_jumpInfoArray;
        float m_jumpSeed;

        #endregion

        public PlayerJumpState(PlayerController p_controller, StateMachine p_stateMachine, ANIMATIONS p_animation) : base(p_controller, p_stateMachine, p_animation)
        {
            m_name = "Jump State";
            m_internalName = "player_jump_state";

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

            int index = PlayerController.JumpCount - 1;
            if(index < 0) { index = 1; }

            JumpInfo jumpInfo = m_jumpInfoArray[index];

            m_gravity = jumpInfo.gravity;
            m_verticalSpeed += jumpInfo.initialSpeed;
            m_controller.AnimatorHandler.PlayTargetAnimation(jumpInfo.animation);
            m_controller.IsGroundedEnabled = false;
            m_controller.StartCoroutine(EnableGrounded());
        }

        IEnumerator EnableGrounded()
        {
            yield return new WaitForSeconds(0.2f);
            m_controller.IsGroundedEnabled = true;
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

        public void AddSpeed(float p_speed) { m_verticalSpeed += p_speed; }
        public float VerticalSpeed { set { m_verticalSpeed = value; } }
        public float JumpSpeed { set { m_jumpSeed = value; } }

    }
}
