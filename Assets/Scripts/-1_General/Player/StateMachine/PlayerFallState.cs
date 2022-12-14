using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D.Player
{
    public class PlayerFallState : PlayerAirState
    {
        public PlayerFallState(PlayerController p_controller, StateMachine p_stateMachine, ANIMATIONS p_animation) : base(p_controller, p_stateMachine, p_animation)
        {
            m_name = "Fall State";
            m_internalName = "player_fall_state";
        }

        public override void Enter(bool p_changeToDefaultAnim)
        {
            base.Enter(p_changeToDefaultAnim);
            PlayerController.AnimatorHandler.PlayTargetAnimation(m_animation, 0.5f);
        }

    }
}
