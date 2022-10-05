using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectD
{
    public abstract class PlayerMoveState : PlayerGroundedState
    {
        protected Vector3 m_moveDirection;
        protected Transform m_cameraObject;
        protected Vector3 m_normalVector;
        protected Transform m_myTransform;
        protected float m_speed;

        protected float m_animatorVerticalValue;

        public PlayerMoveState(PlayerController p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_name, ANIMATIONS p_animation) : base(p_player, p_stateMachine, p_playerData, p_name, p_animation)
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
            m_player.InputHandler.AddListenerToRollButtonPressed(HandleRollInputPressed);
        }

        public override void Exit()
        {
            base.Exit();
            m_player.InputHandler.RemoveListenerFromRollButtonPressed(HandleRollInputPressed);
        }

        ~PlayerMoveState()
        {
            m_player.InputHandler.RemoveListenerFromRollButtonPressed(HandleRollInputPressed);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            float delta = Time.deltaTime;
            HandleMovement(delta);
            HandleRotation(delta);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        protected virtual void HandleMovement(float p_delta)
        {

            m_moveDirection = m_cameraObject.forward * m_player.InputHandler.VerticalInput;
            m_moveDirection += m_cameraObject.right * m_player.InputHandler.HorizontalInput;
            m_moveDirection.Normalize();
            m_moveDirection.y = 0;

            Vector3 projectedVelocityUnitary = Vector3.ProjectOnPlane(m_moveDirection, m_normalVector);
            m_player.Velocity = m_speed * projectedVelocityUnitary;

            UpdateAnimatorValues(m_player.InputHandler.MoveAmount, 0);
        }

        private void HandleRotation(float p_delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = m_player.InputHandler.MoveAmount;

            targetDir = m_cameraObject.forward * m_player.InputHandler.VerticalInput;
            targetDir += m_cameraObject.right * m_player.InputHandler.HorizontalInput;

            targetDir.Normalize();
            targetDir.y = 0;
            if (targetDir == Vector3.zero) { targetDir = m_myTransform.forward; }

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(m_myTransform.rotation, tr, m_playerData.rotationSpeed * p_delta);
            m_myTransform.rotation = targetRotation;
        }

        private void UpdateAnimatorValues(float p_verticalMovement, float p_horizontalMovement)
        {
            #region Vertical
            float v = 0;
            if (p_verticalMovement > 0 && p_verticalMovement < 0.55f) { v = 0.5f; }
            else if (p_verticalMovement > 0.55f) { v = m_animatorVerticalValue; }
            else if (p_verticalMovement < 0 && p_verticalMovement > -0.55f) { v = -0.5f; }
            else if (p_verticalMovement < -0.55f) { v = -m_animatorVerticalValue; }
            #endregion

            #region Horizontal
            float h = 0;
            if (p_horizontalMovement > 0 && p_horizontalMovement < 0.55f) { h = 0.5f; }
            else if (p_horizontalMovement > 0.55f) { h = 1; }
            else if (p_horizontalMovement < 0 && p_horizontalMovement > -0.55f) { h = -0.5f; }
            else if (p_horizontalMovement < -0.55f) { h = -1; }
            #endregion

            m_player.AnimatorHandler.Horizontal = h;
            m_player.AnimatorHandler.Vertical = v;
        }

        private void HandleRollInputPressed()
        {
            if (!m_isActive || m_name == "Roll") { return; }
            m_stateMachine.ChangeState(m_player.RollState);
        }

    }

}