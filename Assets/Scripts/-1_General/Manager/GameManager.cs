using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer3D.Player;
using System;

namespace Platformer3D
{
    public class GameManager : Singleton<GameManager>
{
        [SerializeField] int m_coinsToWinGame = 10;
        int m_coinsCollected = 0;

        Player.PlayerController m_playerController;

        Dictionary<System.Type, string> m_stateIdentifiers;

        private void Start()
        {
            m_stateIdentifiers = new Dictionary<Type, string>();

            m_stateIdentifiers.Add(typeof(PlayerIdleState), "player_idle_state");
            m_stateIdentifiers.Add(typeof(PlayerLocomotionState), "player_locomotion_state");
            m_stateIdentifiers.Add(typeof(PlayerJumpState), "player_jump_state");
            m_stateIdentifiers.Add(typeof(PlayerFallState), "player_fall_state");
        }

        public void AddCoin()
        {
            m_coinsCollected++;
            GUIHandler.Instance.SetCoinTextTo(m_coinsCollected);
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

        public Player.PlayerController PlayerController { get { return m_playerController; } }
        public bool SetPlayerController(Player.PlayerController p_playerController)
        {
            if(m_playerController != null) { return false; }
            else
            {
                m_playerController = p_playerController;
                return true;
            }
        }

        public string GetIdentifier(State p_state) {
            Type type = p_state.GetType();
            bool isNull = !m_stateIdentifiers.ContainsKey(type);

            if (isNull) { return "NOT FOUND"; }

            return m_stateIdentifiers[type];
        }

        public string GetIdentifier(Type p_type)
        {
            bool isNull = !m_stateIdentifiers.ContainsKey(p_type);

            if (isNull) { return "NOT FOUND"; }

            return m_stateIdentifiers[p_type];
        }

    }
}
