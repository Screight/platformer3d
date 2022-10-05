using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectD
{
    public class PlayerRunState : PlayerMoveState
    {
        public PlayerRunState(PlayerController p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, ANIMATIONS p_animation) : base(p_player, p_stateMachine, p_playerData, "Run", p_animation)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        public override void Enter()
        {
            base.Enter();
            m_speed = m_playerData.movementSpeed;
            m_animatorVerticalValue = 1;

            if (m_player.AnimatorHandler.CurrentAnimation != ANIMATIONS.LOCOMOTION)
            {
                m_player.AnimatorHandler.PlayTargetAnimation(m_animation, true);
            }
            m_player.InputHandler.AddListenerToSprintButtonPressed(HandleSprintInputPressed);
        }

        public override void Exit()
        {
            base.Exit();
            m_player.InputHandler.RemoveListenerFromSprintButtonPressed(HandleSprintInputPressed);
        }

        ~PlayerRunState()
        {
            m_player.InputHandler.RemoveListenerFromSprintButtonPressed(HandleSprintInputPressed);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (m_player.InputHandler.MoveAmount == 0)
            {
                m_stateMachine.ChangeState(m_player.IdleState);
                return;
            }
            if (m_player.InputHandler.SprintInput)
            {
                
                return;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        private void HandleSprintInputPressed() {
            if (!m_isActive) { return; }
            m_stateMachine.ChangeState(m_player.SprintState); 
        }

    }

}