using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectD
{
    public class PlayerGroundedState : PlayerState
    {
        protected float m_inputX;
        protected bool m_hasTransitioned = false;
        public PlayerGroundedState(PlayerController p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_name, ANIMATIONS p_animation) : base(p_player, p_stateMachine, p_playerData, p_name, p_animation)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        public override void Enter()
        {
            base.Enter();
            m_hasTransitioned = false;
        }

        public override void Exit()
        {
            base.Exit();
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }

}