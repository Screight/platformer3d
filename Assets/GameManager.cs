using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer3D
{
    public class GameManager : Singleton<GameManager>
{
        [SerializeField] int m_coinsToWinGame = 2;
        int m_coinsCollected = 0;

        public void AddCoin()
        {
            m_coinsCollected++;
            CheckForWinCondition();
        }

        bool CheckForWinCondition()
        {
            if(m_coinsCollected >= m_coinsToWinGame)
            {
                Time.timeScale = 0;
                return true;
            }
            else { return false; }
        }

    }
}
