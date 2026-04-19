using TinySurvivors.InGame;
using TinySurvivors.ObjectPool;
using TinySurvivors.Unit.Player;
using UnityEngine;

namespace TinySurvivors.Unit.Monster.Drop
{
    public class ExpColliderHandler : MonoBehaviour
    {
        [SerializeField] private Exp exp;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out PlayerColliderHandler player)) return;
            if (!InGameManager.PlayerIsAlive()) return;
            
            exp.OnHit(player);
        }
    }
}