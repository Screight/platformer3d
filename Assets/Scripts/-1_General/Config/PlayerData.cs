using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    [CreateAssetMenu(fileName = "newPlayerData", menuName = "Game/Data/Player Data/ Base Data")]

    public class PlayerData : ScriptableObject
    {
        [Header("Move State")]
        public float walkSpeed = 2.0f;
        public float runSpeed = 4.0f;
        public float sprintSpeed = 7.0f;
        public float rotationSpeed = 10.0f;
        public float fallSpeed = 100.0f;
        public float acceleration = 1.0f;

        [Header("Jump State")]

        public float gravity_1 = -10f;
        public float initialSpeed_1 = 8f;
        public float gravity_2 = -10f;
        public float initialSpeed_2 = 10f;
        public float gravity_3 = -10f;
        public float initialSpeed_3 = 12;

        [Range(0,1)]
        public float walToRunAxisTransition = 0.4f;
    }
}
