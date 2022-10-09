using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D.Player
{
    public class PlayerIdleState : PlayerGroundState
    {
        public PlayerIdleState(PlayerController p_controller, StateMachine p_stateMachine, ANIMATIONS p_animation) : base(p_controller, p_stateMachine, "Idle State", p_animation)
        {
        }

        public override void Enter(bool p_changeToDefaultAnim)
        {
            base.Enter(p_changeToDefaultAnim);
        }

        public override void LogicUpdate()
        {

            if (PlayerController.AnimatorHandler.Movement != 0)
            {
                PlayerController.AnimatorHandler.Movement = 0;
            }

            base.LogicUpdate();
            if (PlayerInputHandler.Instance.MovementInput.sqrMagnitude > 0.01f)
            {
                m_stateMachine.ChangeState(PlayerController.LocomotionState);
                return;
            }
        }

    }
}
