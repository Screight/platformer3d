using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    abstract public class GroundState : State
    {
        protected GroundState(StateMachine p_stateMachine, Controller p_controller, string p_name, ANIMATIONS p_animation) : base(p_stateMachine, p_controller, p_name, p_animation)
        {
        }

        public override void Enter(bool p_changeToDefaultAnim)
        {
            base.Enter(p_changeToDefaultAnim);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (!m_controller.CharacterController.isGrounded)
            {
                HandleTransitionToAir();
                return;
            }
            m_controller.MovementY = -0.4f;
        }

        public override void Exit()
        {
            base.Exit();
            m_controller.MovementY = 0;
        }
        abstract protected void HandleTransitionToAir();

    }
}