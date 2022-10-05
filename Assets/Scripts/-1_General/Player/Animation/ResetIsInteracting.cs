using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetIsInteracting : StateMachineBehaviour
{
    int m_isInteractingHash;
    private void OnEnable()
    {
        m_isInteractingHash = Animator.StringToHash("isInteracting");
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(m_isInteractingHash, false);
    }

}
