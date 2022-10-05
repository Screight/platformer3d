using UnityEngine;
using UnityEngine.Events;
using System;

namespace ProjectD
{
    public class AnimationRollingHandler : StateMachineBehaviour
    {
        PlayerController m_controller;
        UnityEvent m_rollAnimationFinishEvent;
        bool m_isInitialized = false;

        private void OnEnable()
        {
            m_rollAnimationFinishEvent = new UnityEvent();
        }

        private void OnDisable()
        {
            m_rollAnimationFinishEvent.RemoveAllListeners();
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            try
            {
                m_rollAnimationFinishEvent.Invoke();
            }
            catch(NullReferenceException e)
            {
                Debug.LogError(e.ToString());
                Debug.LogError(m_controller.ToString() + " Roll State NOT found.");
            }
            
        }

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (m_isInitialized) { return; }
            m_controller = animator.transform.GetComponentInParent<PlayerController>();
            m_rollAnimationFinishEvent.AddListener(m_controller.RollState.HandleRollTransition);
            
        }
    }
}
