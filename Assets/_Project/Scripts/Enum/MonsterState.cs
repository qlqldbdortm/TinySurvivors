namespace TinySurvivors.Enum
{
    public enum MonsterState
    {
        None = 0,
        /// <summary>
        /// 몬스터가 태어난 직후 상태
        /// </summary>
        Idle = 1,
        /// <summary>
        /// 몬스터가 플레이어를 추격하는 상태
        /// </summary>
        Chase = 2,
        /// <summary>
        /// 몬스터가 플레이어를 공격하는 상태 (원거리)
        /// </summary>
        Attack = 3,
        /// <summary>
        /// 몬스터가 플레이어에게 공격받은 상태
        /// </summary>
        Damaged = 4,
        /// <summary>
        /// 몬스터가 플레이어에게 죽은 상태
        /// </summary>
        Death = 5,
        
        BossPattern = 6,
        
        BossDash = 7,
        
        BossChase = 8,
        
        BossBullet = 9,
    }
}