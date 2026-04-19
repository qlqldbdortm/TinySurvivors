using System;
using TinySurvivors.Core;
using TinySurvivors.Unit.Player;
using UnityEngine;

namespace TinySurvivors.InGame
{
    public class PlayerSpawner : MonoBehaviour
    {
        public static event Action<Player> OnPlayerSpawned;
        
        private void Start()
        {
            var db = GameManager.Instance.Database[GameManager.Instance.SelectedPlayerIndex];

            var player = Instantiate(db.playerPrefab, Vector3.zero, Quaternion.identity);
            player.SetUnitData(db.playerData);

            InGameManager.Instance.RegisterPlayer(player as Player);
            OnPlayerSpawned?.Invoke(player as Player);
        }
    }
}