using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D.Player
{
    public class PlayerJump : PlayerAirState
    {
        #region Jump
        float m_gravity_1;
        float m_initialSpeed_1;
        float m_gravity_2;
        float m_initialSpeed_2;
        float m_gravity_3;
        float m_initialSpeed_3;
        #endregion

        protected PlayerJump(PlayerController p_controller, StateMachine p_stateMachine, ANIMATIONS p_animation) : base(p_controller, p_stateMachine, p_animation)
        {
            m_name = "Jump State";

            PlayerData playerData = PlayerController.PlayerData;

            m_gravity_1 = playerData.gravity_1;
            m_initialSpeed_1 = playerData.initialSpeed_1;
            m_gravity_2 = playerData.gravity_2;
            m_initialSpeed_2 = playerData.initialSpeed_3;
            m_gravity_3 = playerData.gravity_3;
            m_initialSpeed_3 = playerData.initialSpeed_3;
        }

    }
}
