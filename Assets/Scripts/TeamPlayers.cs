using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts
{
    [Serializable]
    public class TeamPlayers
    {
        public PlayerControl Player1;
        public PlayerControl Player2;
        public Color Color;
        private int _turnCount;
        private List<PlayerControl> _activPlayers = new List<PlayerControl>();

        public void Init(GameObject root, PlayerControl controlPrefab, Transform spawnPosition1, Transform spawnPosition2)
        {
            if (Player1 != null)
            {
                Object.Destroy(Player1);
            }

            Player1 = Object.Instantiate(controlPrefab);
            Player1.transform.position = spawnPosition1.position;
            Player1.transform.parent = root.transform;
            Player1.Body.color = Color;
            Player1.PlayerHealth.OnDie += (sender, args) => { _activPlayers.Remove(Player1); };

            if (Player2 != null)
            {
                Object.Destroy(Player2);
            }

            Player2 = Object.Instantiate(controlPrefab);
            Player2.transform.position = spawnPosition2.position;
            Player2.transform.parent = root.transform;
            Player2.Body.color = Color;
            Player2.PlayerHealth.OnDie += (sender, args) => { _activPlayers.Remove(Player2); };

            _activPlayers.Add(Player1);
            _activPlayers.Add(Player2);
            _turnCount = 0;
        }

        public PlayerControl GetActivPlayer()
        {
            _turnCount++;
            return _activPlayers[_turnCount%_activPlayers.Count];
        }
    }
}