using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D.Player
{
    public class Movement
    {
        Transform m_cameraTransform;
        PlayerController m_controller;
        
        public Movement(Transform p_cameraTransform, PlayerController p_controller)
        {
            m_cameraTransform = p_cameraTransform;
            m_controller = p_controller;
        }

        public virtual void HandleMovement(float p_deltaTime, PlayerData p_playerData, Vector2 input)
        {
            if(!m_controller.CanMove) { return; }

            Vector3 moveDirection = m_cameraTransform.forward * input.y;
            moveDirection += m_cameraTransform.right * input.x;
            moveDirection.Normalize();

            Transform playerTransform = m_controller.transform;

            RaycastHit hit;
            Physics.Raycast(playerTransform.position, -playerTransform.up, out hit, 10.0f, -1, QueryTriggerInteraction.Ignore);

            Vector3 projectedVelocityUnitary = Vector3.ProjectOnPlane(moveDirection, hit.normal);

            PlayerData playerData = p_playerData;
            float speed = 0;
            float inputMagnitude = input.magnitude;

            if (inputMagnitude < playerData.walToRunAxisTransition)
            {
                speed = playerData.walkSpeed;
            }
            else { speed = playerData.runSpeed; }

            projectedVelocityUnitary *= speed;

            m_controller.Movement += projectedVelocityUnitary * p_deltaTime;
        }

        public void HandleRotation(float p_deltaTime, PlayerData p_playerData)
        {
            if (!m_controller.CanRotate) { return; }

                Vector2 input = PlayerInputHandler.Instance.MovementInput;

            Vector3 moveDirection = m_cameraTransform.forward * input.y;
            moveDirection += m_cameraTransform.right * input.x;
            moveDirection.Normalize();

            Transform playerTransform = m_controller.transform;

            RaycastHit hit;
            Physics.Raycast(playerTransform.position, -playerTransform.up, out hit, 10.0f, -1, QueryTriggerInteraction.Ignore);

            Vector3 target = Vector3.ProjectOnPlane(moveDirection, hit.normal);

            PlayerData playerData = p_playerData;

            if (target == Vector3.zero) { target = playerTransform.forward; }

            Quaternion targetRotation = Quaternion.LookRotation(target, playerTransform.up);

            playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, targetRotation, m_controller.PlayerData.rotationSpeed * Time.deltaTime);

            return;
            if (m_controller.AnimatorHandler.Movement > 0.55f)
            {
                float maxAngle = 120;
                float diff = Vector3.Angle(playerTransform.forward, target);
                if (diff > maxAngle)
                {
                    m_controller.AnimatorHandler.PlayTargetAnimation(ANIMATIONS.RUN_TO_STOP);
                    m_controller.CanRotate = false;
                }
            }

        }

    }
}
