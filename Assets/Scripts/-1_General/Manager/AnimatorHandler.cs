using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    abstract public class AnimatorHandler : MonoBehaviour
    {
        protected Animator m_animator;
        int m_movementHash;

        protected Dictionary<ANIMATIONS, int> m_animationsHashes = new Dictionary<ANIMATIONS, int>();

        protected ANIMATIONS m_currentAnimation;

        private void OnEnable()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            m_animator = GetComponent<Animator>();
        }

        public void PlayTargetAnimation(ANIMATIONS p_targetAnim, float p_transitionTime = 0.2f)
        {
            m_currentAnimation = p_targetAnim;
            m_animator.CrossFade(m_animationsHashes[p_targetAnim], p_transitionTime);
        }

        public void PlayTargetAnimation(int p_targetAnimHash, float p_transitionTime = 0.2f)
        {
            m_animator.CrossFade(p_targetAnimHash, p_transitionTime);
        }

        #region Accesors
        public ANIMATIONS CurrentAnimation
        {
            get { return m_currentAnimation; }
        }

        public int CurrentAnimationHash
        {
            get { return m_animationsHashes[m_currentAnimation]; }
        }

        #endregion
    }
}
