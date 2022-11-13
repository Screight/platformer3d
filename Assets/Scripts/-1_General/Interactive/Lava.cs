using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    public class Lava : Collectable
    {
        protected override void HandleInteraction()
        {
            GameManager.Instance.Health--;
        }
    }
}
