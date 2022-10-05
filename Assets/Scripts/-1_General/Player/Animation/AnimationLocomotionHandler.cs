using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectD
{
    public class AnimationLocomotionHandler : StateMachineBehaviour
    {
        int m_verticalHash;
        int m_isInteractingHash;
        PlayerLocomotion m_playerLocomotion;

        private void OnEnable()
        {
            m_isInteractingHash = Animator.StringToHash("isInteracting");
            m_verticalHash = Animator.StringToHash("Vertical");
            m_playerLocomotion = FindObjectOfType<PlayerLocomotion>();
        }

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_playerLocomotion.SprintFlag = false;
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
        {
        }
        
    }
}
