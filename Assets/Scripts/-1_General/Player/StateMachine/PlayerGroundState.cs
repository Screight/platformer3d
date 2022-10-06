using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D.Player
{
    public class PlayerGroundState : GroundState
    {
        PlayerController m_controller;
        public PlayerGroundState(PlayerController p_controller, StateMachine p_stateMachine, string p_name, ANIMATIONS p_animation) : base(p_stateMachine, p_controller, p_name, p_animation)
        {
            m_controller = p_controller;
        }

        protected override void HandleTransitionToAir()
        {
            m_stateMachine.ChangeState(m_controller.PlayerFallState);
        }
    }

}