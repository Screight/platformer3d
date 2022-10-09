using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D.Player
{
    public class PlayerGroundState : GroundState
    {
        protected PlayerController PlayerController { get { return m_controller as PlayerController; } }
        public PlayerGroundState(PlayerController p_controller, StateMachine p_stateMachine, string p_name, ANIMATIONS p_animation) : base(p_stateMachine, p_controller, p_name, p_animation)
        {
            m_controller = p_controller;
            
        }

        public override void Enter(bool p_changeToDefaultAnim)
        {
            base.Enter(p_changeToDefaultAnim);
            PlayerInputHandler.Instance.AddListenerToJumpButtonPressed(HandleJump);
            m_controller.MovementY = -0.05f;
        }

        public override void Exit()
        {
            base.Exit();
            PlayerInputHandler.Instance.RemoveListenerFromJumpButtonPressed(HandleJump);
        }

        protected override void HandleTransitionToAir()
        {
            m_stateMachine.ChangeState(PlayerController.FallState);
        }

        ~PlayerGroundState()
        {
            PlayerInputHandler.Instance.RemoveListenerFromJumpButtonPressed(HandleJump);
        }

        public void HandleJump() {
            if(m_controller.StateMachine.CurrentState != this) { return; }
            m_stateMachine.ChangeState(PlayerController.JumpState);
        }

    }

}