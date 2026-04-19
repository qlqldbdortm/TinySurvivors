using System;
using System.Collections.Generic;
using TinySurvivors.Unit.Monster;
using UnityEngine;

namespace TinySurvivors.InGame
{
    [Serializable]
    public class SpawnPhase
    {
        [Tooltip("페이즈가 시작되는 시간")] 
        public float startTime;
        [Tooltip("페이즈가 끝나는 시간")] 
        public float endTime;
        [Tooltip("몬스터가 소환되는 딜레이")] 
        public float spawnDelay;

        [Tooltip("해당 스테이지가 마지막인지 확인")] 
        public bool lastStage;
        [Tooltip("해당 페이즈에서 소환되는 몬스터 종류")] 
        public List<MonsterSpawn> monsters;
        [Tooltip("해당 스테이지에서 보스가 소환되는지 확인")]
        public bool spawnBoss;
        [Tooltip("보스를 소환할 때 사용하는 보스 프리펩")] 
        public Monster bossPrefab;
        
        [Tooltip("보스가 소환됬는지 확인하는 용도")] [NonSerialized] 
        public bool bossSpawned;
    }
}