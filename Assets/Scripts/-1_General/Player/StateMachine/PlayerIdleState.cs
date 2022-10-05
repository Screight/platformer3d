using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectD
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(PlayerController p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, ANIMATIONS p_animation) : base(p_player, p_stateMachine, p_playerData, "Idle", p_animation)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        public override void Enter()
        {
            base.Enter();
            if(m_player.AnimatorHandler.CurrentAnimation != ANIMATIONS.LOCOMOTION)
            {
                m_player.AnimatorHandler.PlayTargetAnimation(m_animation, true);
            }
            m_player.AnimatorHandler.Vertical = 0;
            m_player.Velocity = Vector3.zero;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            if(m_player.AnimatorHandler.Vertical != 0)
            {
                m_player.AnimatorHandler.Vertical = 0;
            }
            
            base.LogicUpdate();
            if (m_player.InputHandler.MoveAmount != 0) {
                m_stateMachine.ChangeState(m_player.RunState);
            }

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }

}