using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D.Player
{
    public class PlayerGroundState : GroundState
    {
        protected PlayerController PlayerController { get { return m_controller as PlayerController; } }

        protected Movement m_movementBehaviour;

        public PlayerGroundState(PlayerController p_controller, StateMachine p_stateMachine, string p_name, ANIMATIONS p_animation) : base(p_stateMachine, p_controller, p_name, p_animation)
        {
            m_controller = p_controller;

            m_movementBehaviour = new Movement(UnityEngine.Camera.main.transform, p_controller);
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

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            float deltaTime = Time.deltaTime;
            PlayerData playerData = PlayerController.PlayerData;

            m_movementBehaviour.HandleMovement(deltaTime, playerData, PlayerInputHandler.Instance.MovementInput);

            m_movementBehaviour.HandleRotation(deltaTime, playerData);
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
            float timeInState = Time.time - m_startTime;
            if(timeInState < PlayerController.WindowToChainJumps)
            {
                PlayerController.JumpCount++;
            }
            else { PlayerController.JumpCount = 1; }
            
            m_stateMachine.ChangeState(PlayerController.JumpState);
        }

    }

}