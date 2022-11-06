using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    public class Controller : MonoBehaviour
    {
        protected CharacterController m_characterController;
        protected StateMachine m_stateMachine;
        protected AnimatorHandler m_animatorHandler;

        [SerializeField] Transform m_groundedTransform;
        [SerializeField] LayerMask m_groundedLayersToIgnore;

        Vector3 m_movement;
        bool m_isGrounded = false;
        bool m_isGroundedEnabled = true;

        protected virtual void Awake()
        {
            m_animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }

        protected virtual void Update()
        {
            CalculateIsGrounded();
            m_stateMachine.CurrentState.LogicUpdate();
            m_characterController.Move(m_movement);
            m_movement = Vector2.zero;
        }

        protected virtual void FixedUpdate()
        {
            m_stateMachine.CurrentState.PhysicsUpdate();
        }

        public void CalculateIsGrounded()
        {
            if (m_isGroundedEnabled)
            {
                m_isGrounded = Physics.Raycast(m_groundedTransform.position, -Vector2.up, 0.05f, m_groundedLayersToIgnore);
            }
            else { m_isGrounded = false; }
        }

        #region Getters and Setters

        public bool IsGrounded { get { return m_isGrounded; } }
        public bool IsGroundedEnabled { set { m_isGroundedEnabled = value; } }
        public CharacterController CharacterController { get { return m_characterController; } }
        public StateMachine StateMachine { get { return m_stateMachine; } }
        public AnimatorHandler AnimatorHandler { get { return m_animatorHandler; } }

        #region Movement
        public Vector3 Movement
        {
            get { return m_movement; }
            set { m_movement = value; }
        }

        public float MovementX
        {
            get { return m_movement.x; }
            set { m_movement.x = value; }
        }

        public float MovementY
        {
            get { return m_movement.y; }
            set { m_movement.y = value; }
        }

        public float MovementZ
        {
            get { return m_movement.z; }
            set { m_movement.z = value; }
        }
        #endregion

        #endregion

    }
}
