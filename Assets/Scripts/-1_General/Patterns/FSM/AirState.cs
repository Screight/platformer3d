using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    abstract public class AirState : State
    {
        float m_verticalSpeed;

        protected AirState(Controller p_controller, StateMachine p_stateMachine, ANIMATIONS p_animation) : base(p_stateMachine, p_controller, "Idle State", p_animation)
        {
            m_controller = p_controller;
        }

        public override void Enter()
        {
            base.Enter();
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
            CharacterController controller = m_controller.CharacterController;
            if (controller.isGrounded)
            {
                Debug.Log("Grounded");
            }
            if (!controller.isGrounded)
            {
                float time = Time.deltaTime;
                float gravity = -9.8f;
                m_verticalSpeed += gravity * time;
                float distance = m_verticalSpeed * time;
                controller.Move(new Vector3(0, distance, 0));
            }

        }

        protected abstract void HandleTransitionToGround();
    }
}
