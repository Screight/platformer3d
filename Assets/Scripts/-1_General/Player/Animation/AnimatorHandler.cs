using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectD
{
    public enum ANIMATIONS { LOCOMOTION,ROLLING, BACKSTEP, LAND, FALLING, EMPTY, LAST_NO_USE }
    public class AnimatorHandler : MonoBehaviour
    {
        InputHandler m_inputHandler;
        PlayerLocomotion m_playerLocomotion;

        Animator m_animator;
        int m_vertical;
        int m_horizontal;
        [SerializeField] bool m_canRotate;

        Dictionary<ANIMATIONS, int> m_animationsHashes = new Dictionary<ANIMATIONS, int>();

        int m_isInteractingHash;

        ANIMATIONS m_currentAnimation;

        private void Awake()
        {
            Initialize();
        }
        public void Initialize()
        {
            m_animator = GetComponent<Animator>();
            m_vertical = Animator.StringToHash("Vertical");
            m_horizontal = Animator.StringToHash("Horizontal");
            m_isInteractingHash = Animator.StringToHash("isInteracting");

            m_animationsHashes.Add(ANIMATIONS.ROLLING, Animator.StringToHash("Rolling"));
            m_animationsHashes.Add(ANIMATIONS.BACKSTEP, Animator.StringToHash("Backstep"));
            m_animationsHashes.Add(ANIMATIONS.LOCOMOTION, Animator.StringToHash("Locomotion"));
            m_animationsHashes.Add(ANIMATIONS.EMPTY, Animator.StringToHash("Empty"));
            m_animationsHashes.Add(ANIMATIONS.LAND, Animator.StringToHash("Landing"));
            m_animationsHashes.Add(ANIMATIONS.FALLING, Animator.StringToHash("Falling"));

            m_inputHandler = GetComponentInParent<InputHandler>();
            m_playerLocomotion = GetComponentInParent<PlayerLocomotion>();

        }

        public void UpdateAnimatorValues(float p_verticalMovement, float p_horizontalMovement, bool p_isSprinting)
        {
            #region Vertical
            float v = 0;
            if (p_verticalMovement > 0 && p_verticalMovement < 0.55f) { v = 0.5f; }
            else if (p_verticalMovement > 0.55f) { v = 1; }
            else if (p_verticalMovement < 0 && p_verticalMovement > -0.55f) { v = -0.5f; }
            else if (p_verticalMovement < -0.55f) { v = -1; }
            #endregion

            #region Horizontal
            float h = 0;
            if (p_horizontalMovement > 0 && p_horizontalMovement < 0.55f) { h = 0.5f; }
            else if (p_horizontalMovement > 0.55f) { h = 1; }
            else if (p_horizontalMovement < 0 && p_horizontalMovement > -0.55f) { h = -0.5f; }
            else if (p_horizontalMovement < -0.55f) { h = -1; }
            #endregion

            if (p_isSprinting)
            {
                v = 2;
                h = p_horizontalMovement;
            }

            float m_deltaTime = Time.deltaTime;
            m_animator.SetFloat(m_vertical, v, 0.1f, m_deltaTime);
            m_animator.SetFloat(m_horizontal, h, 0.1f, m_deltaTime);
        }

        public void PlayTargetAnimation(ANIMATIONS p_targetAnim, bool p_isInteracting)
        {
            m_animator.applyRootMotion = p_isInteracting;
            m_animator.SetBool(m_isInteractingHash, p_isInteracting);

            m_currentAnimation = p_targetAnim;
            m_animator.CrossFade(m_animationsHashes[p_targetAnim], 0.2f);
        }

        public void PlayTargetAnimation(int p_targetAnimHash, bool p_isInteracting)
        {
            m_animator.applyRootMotion = p_isInteracting;
            m_animator.SetBool(m_isInteractingHash, p_isInteracting);
            m_animator.CrossFade(p_targetAnimHash, 0.2f);
        }

        private void OnAnimatorMove()
        {
            /*if (IsInteracting == false) { return; }

            float delta = Time.deltaTime;

            m_playerLocomotion.Rigidbody.drag = 0;
            Vector3 deltaPosition = m_animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            m_playerLocomotion.Rigidbody.velocity = velocity;*/
        }

        #region Accesors
        public bool CanRotate
        {
            get { return m_canRotate; }
            set { m_canRotate = value; }
        }

        public bool IsInteracting
        {
            get { return m_animator.GetBool(m_isInteractingHash); }
            set { m_animator.SetBool(m_isInteractingHash, value); }
        }
        public float Horizontal
        {
            get { return m_animator.GetFloat(m_horizontal); }
            set { m_animator.SetFloat(m_horizontal, value, 0.1f, Time.deltaTime); }
        }
        public float Vertical
        {
            get { return m_animator.GetFloat(m_vertical); }
            set { m_animator.SetFloat(m_vertical, value, 0.1f, Time.deltaTime); }
        }

        public void SetVertical(float p_value)
        {
            m_animator.SetFloat(m_vertical, p_value, 0.0f, Time.deltaTime);
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
