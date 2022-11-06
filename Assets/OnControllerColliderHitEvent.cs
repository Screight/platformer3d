using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Platformer3D
{
    public abstract class OnControllerColliderHitEvent : MonoBehaviour
    {
        public abstract void HandleInteraction(ControllerColliderHit p_hit);

    }
}
