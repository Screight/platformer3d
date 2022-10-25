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

            string name = "Locomotion";
            Animation animation = new Animation(name, Animator.StringToHash(name));
            m_animations.Add(ANIMATIONS.LOCOMOTION, animation);

            name = "Jump_1";
            animation = new Animation(name, Animator.StringToHash(name));
            m_animations.Add(ANIMATIONS.JUMP, animation);

            name = "Jump_2";
            animation = new Animation(name, Animator.StringToHash(name));
            m_animations.Add(ANIMATIONS.JUMP_2, animation);

            name = "Jump_3";
            animation = new Animation(name, Animator.StringToHash(name));
            m_animations.Add(ANIMATIONS.JUMP_3, animation);

            name = "Empty";
            animation = new Animation(name, Animator.StringToHash(name));
            m_animations.Add(ANIMATIONS.EMPTY, animation);

            name = "Landing";
            animation = new Animation(name, Animator.StringToHash(name));
            m_animations.Add(ANIMATIONS.LAND, animation);

            name = "FallLoop";
            animation = new Animation(name, Animator.StringToHash(name));
            m_animations.Add(ANIMATIONS.FALLING, animation);

            name = "FallToLanding";
            animation = new Animation(name, Animator.StringToHash(name));
            m_animations.Add(ANIMATIONS.FALL_TO_LANDIING, animation);

            name = "RunPreparation";
            animation = new Animation(name, Animator.StringToHash(name));
            m_animations.Add(ANIMATIONS.RUN_PREPARATION, animation);

            name = "Idle";
            animation = new Animation(name, Animator.StringToHash(name));
            m_animations.Add(ANIMATIONS.IDLE, animation);

            name = "RunToStop";
            animation = new Animation(name, Animator.StringToHash(name));
            m_animations.Add(ANIMATIONS.RUN_TO_STOP, animation);

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
