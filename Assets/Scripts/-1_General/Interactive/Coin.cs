using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    public class Coin : Collectable
    {
        [SerializeField] float m_speed = 100.0f;

        private void Update()
        {
            transform.Rotate(Vector3.up * m_speed * Time.deltaTime);
        }

        protected override void HandleInteraction()
        {
            GameManager.Instance.AddCoin();
            Destroy(this.gameObject);
        }
    }

}