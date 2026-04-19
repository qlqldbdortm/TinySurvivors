using TinySurvivors.Weapon;
using UnityEngine;

namespace TinySurvivors.Interface
{
    public interface IAttackPattern
    {
        public void Execute(WeaponInstance weapon, Transform origin);
    }
}