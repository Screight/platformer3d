using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectD
{
    public class PlayerSprintState : PlayerMoveState
    {

        public PlayerSprintState(PlayerController p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, ANIMATIONS p_animation) : base(p_player, p_stateMachine, p_playerData, "Sprint", p_animation)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        public override void Enter()
        {
            base.Enter();
            m_speed = m_playerData.sprintSpeed;
            m_animatorVerticalValue = 2;

            if (m_player.AnimatorHandler.CurrentAnimation != ANIMATIONS.LOCOMOTION)
            {
                m_player.AnimatorHandler.PlayTargetAnimation(m_animation, true);
            }
            m_player.InputHandler.AddListenerToSprintButtonReleased(HandleSprintInputReleased);

        }

        public override void Exit()
        {
            base.Exit();
            m_player.InputHandler.AddListenerToSprintButtonReleased(HandleSprintInputReleased);
        }

        ~PlayerSprintState()
        {
            m_player.InputHandler.RemoveListenerFromSprintButtonReleased(HandleSprintInputReleased);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (m_player.InputHandler.MoveAmount == 0)
            {
                m_stateMachine.ChangeState(m_player.IdleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
        private void HandleSprintInputReleased()
        {
            if (!m_isActive) { return; }
            m_stateMachine.ChangeState(m_player.RunState);
        }

    }

}