using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    public class Controller : MonoBehaviour
    {
        protected CharacterController m_characterController;

        #region Getters and Setters

        public CharacterController CharacterController
        {
            get { return m_characterController; }
        }

        #endregion

    }
}
