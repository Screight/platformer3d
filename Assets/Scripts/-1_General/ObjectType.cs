using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    public class ObjectType : MonoBehaviour
    {
        [SerializeField] List<OBJECT_TYPE> m_types;

        public bool IsType(OBJECT_TYPE p_type)
        {
            foreach (OBJECT_TYPE type in m_types) { if (type == p_type) { return true; } }
            return false;
        }

    }
}
