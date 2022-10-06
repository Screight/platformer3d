using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    public class StateMachine
    {
        State m_currentState;

        public void Initialize(State p_startingState)
        {
            m_currentState = p_startingState;
            m_currentState.Enter();
        }

        public void ChangeState(State p_newState)
        {
            m_currentState.Exit();
            m_currentState = p_newState;
            m_currentState.Enter();
        }

        public State CurrentState
        {
            get { return m_currentState; }
            set { m_currentState = value; }
        }

    }

}