using UnityEngine;

namespace TinySurvivors.SO.RewardSO
{
    [CreateAssetMenu(fileName = "RewardData", menuName = "ScriptableObjects/Reward/01.Health", order = 1)]
    public class HealthRewardData : BaseRewardData
    {
        public float increaseMaxHp;
    }
}