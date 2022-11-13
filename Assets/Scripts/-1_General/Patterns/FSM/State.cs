using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    public class State
    {
        protected Controller m_controller;
        protected StateMachine m_stateMachine;
        protected float m_startTime;

        protected ANIMATIONS m_animation;
        protected string m_name;

        protected bool m_isActive;
        protected string m_internalName;

        public State(StateMachine p_stateMachine, Controller p_controller, string p_name, ANIMATIONS p_animation)
        {
            m_stateMachine = p_stateMachine;
            m_controller = p_controller;
            m_name = p_name;
            m_animation = p_animation;
        }

        public virtual void Enter(bool p_changeToDefaultAnim = true)
        {
            Debug.Log("Entered " + m_name + " state.");
            DoChecks();
            // start animation
            m_startTime = Time.time;
            m_isActive = true;

            if (p_changeToDefaultAnim)
            {
                m_controller.AnimatorHandler.PlayTargetAnimation(m_animation);
            }

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

        public override string ToString() { return m_name; }
        public string Identifier { get { return m_internalName; } }

    }

}