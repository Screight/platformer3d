using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectD
{
    public class PlayerRollState : PlayerMoveState
    {
        public PlayerRollState(PlayerController p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, ANIMATIONS p_animation) : base(p_player, p_stateMachine, p_playerData, "Roll", p_animation)
        {
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }

        public override void Enter()
        {
            base.Enter();
            m_cameraObject = Camera.main.transform;
            m_myTransform = m_player.transform;
            if (m_player.InputHandler.SprintInput)
            {
                m_speed = m_playerData.sprintSpeed;
            }
            else {
                m_speed = m_playerData.movementSpeed;
            }

            m_player.AnimatorHandler.PlayTargetAnimation(m_animation, true);

            // Movement
            m_moveDirection = m_cameraObject.forward * m_player.InputHandler.VerticalInput;
            m_moveDirection += m_cameraObject.right * m_player.InputHandler.HorizontalInput;
            m_moveDirection.Normalize();
            m_moveDirection.y = 0;

            Vector3 projectedVelocityUnitary = Vector3.ProjectOnPlane(m_moveDirection, m_normalVector);
            m_player.Velocity = m_speed * projectedVelocityUnitary;

            // Rotation
            Vector3 targetDir = Vector3.zero;
            float moveOverride = m_player.InputHandler.MoveAmount;

            targetDir = m_cameraObject.forward * m_player.InputHandler.VerticalInput;
            targetDir += m_cameraObject.right * m_player.InputHandler.HorizontalInput;

            targetDir.Normalize();
            targetDir.y = 0;
            if (targetDir == Vector3.zero) { targetDir = m_myTransform.forward; }

            Quaternion tr = Quaternion.LookRotation(targetDir);
            m_myTransform.rotation = tr;

        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void LogicUpdate()
        {
            
        }

        public void HandleRollTransition()
        {
            if (!m_isActive) { return; }
            if (m_player.InputHandler.MoveAmount == 0)
            {
                m_stateMachine.ChangeState(m_player.IdleState);
                return;
            }
            if (m_player.InputHandler.SprintInput) { m_stateMachine.ChangeState(m_player.SprintState); }
            else { m_stateMachine.ChangeState(m_player.RunState); }
        }

    }

}