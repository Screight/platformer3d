using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    abstract public class AirState : State
    {
        protected float m_verticalSpeed;
        protected float m_gravity;
        protected AirState(Controller p_controller, StateMachine p_stateMachine, ANIMATIONS p_animation) : base(p_stateMachine, p_controller, "Idle State", p_animation)
        {
            m_controller = p_controller;
        }

        public override void Enter(bool p_changeToDefaultAnim)
        {
            base.Enter(p_changeToDefaultAnim);
        }

        public override void Exit()
        {
            base.Exit();
            m_verticalSpeed = 0;
        }

        public override void LogicUpdate()
        {
            if (m_controller.CharacterController.isGrounded)
            {
                HandleTransitionToGround();
                return;
            }
            HandleGravity();
        }

        private void HandleGravity()
        {
            float time = Time.deltaTime;
            float gravity = m_gravity;
            m_verticalSpeed += gravity * time;
            float distance = m_verticalSpeed * time;
            m_controller.MovementY += distance;
        }

        protected abstract void HandleTransitionToGround();
    }
}
