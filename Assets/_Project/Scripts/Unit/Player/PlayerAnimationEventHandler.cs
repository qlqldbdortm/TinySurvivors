using System;
using TinySurvivors.InGame;
using TinySurvivors.UI;
using UnityEngine;

namespace TinySurvivors.Unit.Player
{
    public class PlayerAnimationEventHandler : UnitAnimationEventHandler
    {
        public event Action OnPlayerDeath;
        public override void OnAttackStart()
        {
            
        }

        public override void OnShootProj()
        {
            
        }

        public override void OnAttackEnd()
        {

        }

        public override void OnDeath()
        {
            OnPlayerDeath?.Invoke();
        }
    }
}