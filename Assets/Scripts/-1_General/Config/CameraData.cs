using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    [CreateAssetMenu(fileName = "newCameraData", menuName = "Game/Data/Camera Data")]

    public class CameraData : ScriptableObject
    {
        [Header("Camera")]
        public float yawSpeedCamera = 2.0f;
        public float pitchSpeedCamera = 2.0f;
        public float maximumPivot = 35;
        public float minimumPivot = -35;

        public float followSpeed = 2.0f;
    }
}
