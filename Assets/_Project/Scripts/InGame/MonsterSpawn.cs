using System;
using TinySurvivors.Unit.Monster;
using UnityEngine;

namespace TinySurvivors.InGame
{
    [Serializable]
    public class MonsterSpawn
    {
        [Tooltip("소환할 몬스터 프리팹")]
        public Monster prefab;
        
        [Tooltip("해당 몬스터의 소환 확률")] 
        [Range(0, 100)]
        public int rate;
    }
}