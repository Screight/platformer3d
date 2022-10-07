using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    public class StateMachine
    {
        State m_currentState;

        public void Initialize(State p_startingState, bool p_changeToDefaultAnim)
        {
            m_currentState = p_startingState;
            m_currentState.Enter(p_changeToDefaultAnim);
        }

        public void ChangeState(State p_newState, bool p_changeToDefaultAnim = true)
        {
            m_currentState.Exit();
            m_currentState = p_newState;
            m_currentState.Enter(p_changeToDefaultAnim);
        }

        public State CurrentState
        {
            get { return m_currentState; }
            set { m_currentState = value; }
        }

    }

}