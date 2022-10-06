using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D.Player
{
    public class PlayerLocomotionState : PlayerGroundState
    {
        PlayerController m_controller;
        Transform m_cameraTransform;

        public PlayerLocomotionState(PlayerController p_controller, StateMachine p_stateMachine, ANIMATIONS p_animation) : base(p_controller, p_stateMachine, "Locomotion State", p_animation)
        {
            m_controller = p_controller;
            m_cameraTransform = Camera.main.transform;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (m_controller.InputHandler.MovementInput.sqrMagnitude < 0.01f)
            {
                m_stateMachine.ChangeState(m_controller.IdleState);
                return;
            }

            float delta = Time.deltaTime;
            HandleMovement(delta);
            HandleRotation(delta);
        }

        public void HandleMovement(float p_delta)
        {
            Vector2 input = m_controller.InputHandler.MovementInput;

            Vector3 moveDirection = m_cameraTransform.forward * input.y;
            moveDirection += m_cameraTransform.right * input.x;
            moveDirection.Normalize();

            Transform playerTransform = m_controller.transform;

            RaycastHit hit;
            Physics.Raycast(playerTransform.position, -playerTransform.up, out hit, 10.0f, -1, QueryTriggerInteraction.Ignore);

            Vector3 projectedVelocityUnitary = Vector3.ProjectOnPlane(moveDirection, hit.normal);
            projectedVelocityUnitary *= m_controller.PlayerData.movementSpeed;

            m_controller.CharacterController.Move(projectedVelocityUnitary * p_delta);

            m_controller.AnimatorHandler.UpdateAnimatorValues(input.magnitude);

        }

        public void HandleRotation(float p_delta)
        {
            Transform playerTransform = m_controller.transform;
            Vector3 targetDir = Vector3.zero;

            targetDir = m_cameraTransform.forward * m_controller.InputHandler.MovementInput.y;
            targetDir += m_cameraTransform.right * m_controller.InputHandler.MovementInput.x;

            targetDir.Normalize();
            targetDir.y = 0;
            if (targetDir == Vector3.zero) { targetDir = playerTransform.forward; }

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(playerTransform.rotation, tr, m_controller.PlayerData.rotationSpeed * p_delta);
            playerTransform.rotation = targetRotation;
        }

    }
}
