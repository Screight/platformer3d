using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    public class PlayerAnimatorHandler : AnimatorHandler
    {
        PlayerInputHandler m_inputHandler;

        int m_movementHash;

        private void OnEnable()
        {
            Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();

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

        #endregion
    }
}
