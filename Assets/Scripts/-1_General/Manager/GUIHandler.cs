using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    public class GUIHandler : Singleton<GUIHandler>
    {
        TMPro.TextMeshProUGUI m_coinText;

        protected override void Awake()
        {
            base.Awake();
            m_coinText = transform.GetChild(0).Find("Text").GetComponent<TMPro.TextMeshProUGUI>();
        }

        public void SetCoinTextTo(int p_value) { m_coinText.text = p_value.ToString(); }

    }
}
