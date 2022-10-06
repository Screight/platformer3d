using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D.Player
{
    public class PlayerIdleState : PlayerGroundState
    {
        PlayerController m_controller;
        public PlayerIdleState(PlayerController p_controller, StateMachine p_stateMachine, ANIMATIONS p_animation) : base(p_controller, p_stateMachine, "Idle State", p_animation)
        {
            m_controller = p_controller;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {

            if (m_controller.AnimatorHandler.Movement != 0)
            {
                m_controller.AnimatorHandler.Movement = 0;
            }

            base.LogicUpdate();
            if (m_controller.InputHandler.MovementInput.sqrMagnitude > 0.01f)
            {
                m_stateMachine.ChangeState(m_controller.LocomotionState);
                return;
            }
        }

    }
}
