using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    public class Mushroom : OnControllerColliderHitEvent
    {
        [SerializeField] float m_jumpSpeed = 5.0f;
        public override void HandleInteraction(ControllerColliderHit p_hit)
        {
            Player.PlayerController playerController = GameManager.Instance.PlayerController;
            string stateID = playerController.CurrentState.Identifier;

            if (stateID != GameManager.Instance.GetIdentifier(typeof(Player.PlayerJumpState)) && stateID != GameManager.Instance.GetIdentifier(typeof(Player.PlayerFallState))) { return; }

            float angleBetween = Vector3.Angle(p_hit.normal, p_hit.controller.transform.up);

            if(angleBetween >= -90 && angleBetween <= 90)
            {

                if(playerController.CurrentState.Identifier == GameManager.Instance.GetIdentifier(playerController.JumpState))
                {
                    playerController.JumpState.VerticalSpeed = m_jumpSpeed;
                }
                else
                {
                    playerController.StateMachine.ChangeState(playerController.JumpState);
                    playerController.JumpState.JumpSpeed = m_jumpSpeed;
                }
            }

        }
    }
}