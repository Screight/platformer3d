using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectD
{
    public class PlayerState
    {
        protected PlayerController m_player;
        protected PlayerStateMachine m_stateMachine;
        protected PlayerData m_playerData;
        protected float m_startTime;

        protected ANIMATIONS m_animation;
        protected string m_name;

        protected bool m_isActive;

        public PlayerState(PlayerController p_player, PlayerStateMachine p_stateMachine, PlayerData p_playerData, string p_name, ANIMATIONS p_animation)
        {
            m_player = p_player;
            m_stateMachine = p_stateMachine;
            m_playerData = p_playerData;
            m_name = p_name;
            m_animation = p_animation;
        }

        public virtual void Enter()
        {
            Debug.Log("Entered " + m_name + " state.");
            DoChecks();
            // start animation
            m_startTime = Time.time;
            m_isActive = true;
        }
        public virtual void Exit()
        {
            m_isActive = false;
        }
        public virtual void LogicUpdate() { }
        public virtual void PhysicsUpdate()
        {
            DoChecks();
        }
        public virtual void DoChecks() { }

    }

}