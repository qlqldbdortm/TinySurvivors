using UnityEngine;

namespace TinySurvivors.SO.RewardSO
{
    public abstract class BaseRewardData : ScriptableObject
    {
        public Sprite rewardIcon;
        public string rewardName;
        public string rewardDescription;
    }
}