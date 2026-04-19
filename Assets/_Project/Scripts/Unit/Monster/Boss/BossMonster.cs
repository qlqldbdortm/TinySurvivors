using System;
using TinySurvivors.Enum;
using TinySurvivors.InGame;
using TinySurvivors.UI;
using UnityEngine;

namespace TinySurvivors.Unit.Monster.Boss
{
    public class BossMonster : Monster
    {
        public override bool IsBoss => true;
                
        public float[] patternCooldown = { 3f, 5f };

        [HideInInspector] public float patternTimer;
        
        /// <summary>
        /// 현재 어떤 공격을 하는지에 대한 정보 (대시, 투척)
        /// </summary>
        [HideInInspector] public int currentPattern;
        
        /// <summary>
        /// 투사체를 던지는 방식에 대한 정보 (원형, 부채꼴)
        /// </summary>
        public BossBulletPattern CurrentBulletPattern { get; private set; }

        public void SetBulletPattern(BossBulletPattern pattern)
        {
            CurrentBulletPattern = pattern;
        }

        public override void Init()
        {
            base.Init();
            InGameUIManager.Instance.BossSpawn(this);
            UnitHp.OnDamaged += InGameUIManager.Instance.BossHudUI.SetHp;
        }

        public override void Release()
        {
            base.Release();
            InGameUIManager.Instance.BossDespawn();
            UnitHp.OnDamaged -= InGameUIManager.Instance.BossHudUI.SetHp;
        }

        public string GetName()
        {
            return baseUnitData.baseName;
        }
    }
}