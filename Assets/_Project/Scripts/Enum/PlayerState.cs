namespace TinySurvivors.Enum
{
    public enum PlayerState
    {
        None = 0,
        /// <summary>
        /// 플레이어가 태어난 상태, 플레이어가 가만히 서있는 상태
        /// </summary>
        Idle = 1,
        /// <summary>
        /// 플레이어가 이동하는 상태
        /// </summary>
        Move = 2,
        /// <summary>
        /// 플레이어가 몬스터에게 피격당한 상태
        /// </summary>
        Damage = 3,
        /// <summary>
        /// 플레이어가 몬스터에게 죽은 상태
        /// </summary>
        Death = 4,
    }
}