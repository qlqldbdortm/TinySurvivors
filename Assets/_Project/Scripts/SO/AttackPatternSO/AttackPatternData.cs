using TinySurvivors.Interface;
using TinySurvivors.Weapon;
using UnityEngine;

namespace TinySurvivors.SO.AttackPatternSO
{
    public abstract class AttackPatternData : ScriptableObject, IAttackPattern
    {
        public abstract void Execute(WeaponInstance weapon, Transform origin);
    }
}