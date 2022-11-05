using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    public abstract class Collectable : MonoBehaviour
    {

        private void OnTriggerEnter(Collider p_collider)
        {
            if (p_collider.tag != "Player") { return; }
            HandleInteraction();
        }

        private void OnTriggerExit(Collider p_collider)
        {
            if (p_collider.tag != "Player") { return; }
            HandleInteraction();
        }

        protected abstract void HandleInteraction();

    }
}
