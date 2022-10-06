using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    [CreateAssetMenu(fileName = "newPlayerData", menuName = "Game/Data/Player Data/ Base Data")]

    public class PlayerData : ScriptableObject
    {
        [Header("Move State")]
        public float movementSpeed = 4.0f;
        public float sprintSpeed = 7.0f;
        public float rotationSpeed = 10.0f;
        public float fallSpeed = 100.0f;

        [Tooltip("The layers to ignore when checking for ground")]
        public LayerMask m_groundMask;
    }
}
