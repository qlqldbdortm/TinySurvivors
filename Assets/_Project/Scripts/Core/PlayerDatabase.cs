using System;
using TinySurvivors.SO.UnitSO.PlayerSO;

namespace TinySurvivors.Core
{
    [Serializable]
    public class PlayerDatabase
    {
        public PlayerData playerData;
        public Unit.Unit playerPrefab;

        /// <summary>
        /// 유저가 로비에서 플레이어의 직업을 선택할 때 PlayerData를 세팅한 상태로 만들어주는 메서드
        /// </summary>
        public void Setup()
        {
            playerPrefab.SetUnitData(playerData);
        }
    }
}