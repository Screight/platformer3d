using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Platformer3D
{
    public struct Animation
    {
        public string name;
        public int hash;

        public Animation(string p_name, int p_hash)
        {
            name = p_name;
            hash = p_hash;
        }

    }

    abstract public class AnimatorHandler : MonoBehaviour
    {
        protected Animator m_animator;
        int m_movementHash;

        protected Dictionary<ANIMATIONS, Animation> m_animations = new Dictionary<ANIMATIONS, Animation>();

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
            m_animator.CrossFade(m_animations[p_targetAnim].hash, p_transitionTime);
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

        public bool IsAnimationPlaying(ANIMATIONS p_animation)
        {
            try
            {
                return m_animations[p_animation].name == m_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            }
            catch (IndexOutOfRangeException e){
                return false;
            }
        }

        public int CurrentAnimationHash
        {
            get { return m_animations[m_currentAnimation].hash; }
        }

        #endregion
    }
}
