using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    public class Controller : MonoBehaviour
    {
        protected CharacterController m_characterController;
        protected StateMachine m_stateMachine;

        Vector3 m_movement;

        protected virtual void Update()
        {
            m_characterController.Move(m_movement);
        }

        #region Getters and Setters

        public CharacterController CharacterController
        {
            get { return m_characterController; }
        }

        public StateMachine StateMachine
        {
            get { return m_stateMachine; }
            private set { }
        }

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
