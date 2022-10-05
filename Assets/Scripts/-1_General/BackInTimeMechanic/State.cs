using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectD.BackInTime
{
    public class State
    {
        Vector3 m_speed;
        Quaternion m_rotation;
        Vector3 m_scale;

        float m_vertical;
        int m_animationHash;

        public State(Target p_target)
        {
            m_rotation = p_target.transform.rotation;
            m_scale = p_target.transform.localScale;
            m_speed = p_target.rigidbody.velocity;
            m_animationHash = p_target.animtorHandler.CurrentAnimationHash;
            m_vertical = p_target.animtorHandler.Vertical;
        }

        public void ApplyStateTo(Target p_target)
        {
            p_target.rigidbody.velocity = -m_speed;
            p_target.transform.rotation = m_rotation;
            p_target.transform.localScale = m_scale;

            if(m_animationHash != p_target.animtorHandler.CurrentAnimationHash)
            {
                p_target.animtorHandler.PlayTargetAnimation(m_animationHash, true);
            }

            p_target.animtorHandler.SetVertical(m_vertical);

        }

    }
}
