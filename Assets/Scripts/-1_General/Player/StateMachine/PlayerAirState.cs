using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D.Player
{
    public class PlayerAirState : AirState
    {
        protected PlayerController PlayerController { get { return m_controller as PlayerController; } }

        Movement m_movementBehaviour;

        protected PlayerAirState(PlayerController p_controller, StateMachine p_stateMachine, ANIMATIONS p_animation) : base(p_controller, p_stateMachine, p_animation)
        {
            m_controller = p_controller;
            m_gravity = PlayerController.PlayerData.gravity_1;

            m_movementBehaviour = new Movement(UnityEngine.Camera.main.transform, p_controller);

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            float deltaTime = Time.deltaTime;
            PlayerData playerData = PlayerController.PlayerData;

            m_movementBehaviour.HandleMovement(deltaTime, playerData, PlayerInputHandler.Instance.MovementInput);

            m_movementBehaviour.HandleRotation(deltaTime, playerData);

        }

        protected override void HandleTransitionToGround()
        {
            float hola1 = PlayerInputHandler.Instance.MovementInput.magnitude;
            bool hola = PlayerInputHandler.Instance.MovementInput.magnitude > 0.01f;
            if (PlayerInputHandler.Instance.MovementInput.magnitude > 0.01f)
            {
                m_stateMachine.ChangeState(PlayerController.LocomotionState);
            }
            else {
                m_stateMachine.ChangeState(PlayerController.IdleState, false);
                PlayerController.AnimatorHandler.PlayTargetAnimation(ANIMATIONS.FALL_TO_LANDIING);
            }
        }
    }
}
