using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    public class PlayerAnimatorHandler : MonoBehaviour
    {
        PlayerInputHandler m_inputHandler;

        Animator m_animator;
        int m_movementHash;

        Dictionary<ANIMATIONS, int> m_animationsHashes = new Dictionary<ANIMATIONS, int>();

        int m_isInteractingHash;

        ANIMATIONS m_currentAnimation;

        private void OnEnable()
        {
            Initialize();
        }

        public void Initialize()
        {
            m_animator = GetComponent<Animator>();
            m_movementHash = Animator.StringToHash("Movement");

            m_animationsHashes.Add(ANIMATIONS.LOCOMOTION, Animator.StringToHash("Locomotion"));
            m_animationsHashes.Add(ANIMATIONS.JUMP, Animator.StringToHash("Jump_1"));
            m_animationsHashes.Add(ANIMATIONS.JUMP_2, Animator.StringToHash("Jump_2"));
            m_animationsHashes.Add(ANIMATIONS.JUMP_3, Animator.StringToHash("Jump_3"));
            m_animationsHashes.Add(ANIMATIONS.EMPTY, Animator.StringToHash("Empty"));
            m_animationsHashes.Add(ANIMATIONS.LAND, Animator.StringToHash("Landing"));
            m_animationsHashes.Add(ANIMATIONS.FALLING, Animator.StringToHash("FallLoop"));
            m_animationsHashes.Add(ANIMATIONS.FALL_TO_LANDIING, Animator.StringToHash("FallToLanding"));

            m_inputHandler = GetComponentInParent<PlayerInputHandler>();

        }

        public void UpdateAnimatorValues(float p_movementValue, float walkToRunAxisTransition)
        {
            #region Movement
            float v = 0;
            if(p_movementValue == 0) { v = 0; }
            else if (Mathf.Abs(p_movementValue) < walkToRunAxisTransition) { v = 0.5f; }
            else { v = 1; }
            #endregion

            float m_deltaTime = Time.deltaTime;
            m_animator.SetFloat(m_movementHash, v, 0.1f, m_deltaTime);
        }

        public void PlayTargetAnimation(ANIMATIONS p_targetAnim)
        {
            m_currentAnimation = p_targetAnim;
            m_animator.CrossFade(m_animationsHashes[p_targetAnim], 0.2f);
        }

        public void PlayTargetAnimation(int p_targetAnimHash)
        {
            m_animator.CrossFade(p_targetAnimHash, 0.2f);
        }

        #region Accesors
        public float Movement
        {
            get { return m_animator.GetFloat(m_movementHash); }
            set { m_animator.SetFloat(m_movementHash, value, 0.2f, Time.deltaTime); }
        }

        public void SetMovementType(float p_value)
        {
            m_animator.SetFloat(m_movementHash, p_value, 0.0f, Time.deltaTime);
        }

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
