using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D.Player
{
    public class PlayerAirState : AirState
    {
        protected PlayerController PlayerController { get { return m_controller as PlayerController; } }
        protected PlayerAirState(PlayerController p_controller, StateMachine p_stateMachine, ANIMATIONS p_animation) : base(p_controller, p_stateMachine, p_animation)
        {
            m_controller = p_controller;
            m_gravity = PlayerController.PlayerData.gravity_1;
        }

        protected override void HandleTransitionToGround()
        {
            if(PlayerInputHandler.Instance.MovementInput.sqrMagnitude > 0.01)
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
