using TinySurvivors.SO.UnitSO;
using UnityEngine;

namespace TinySurvivors.Unit
{
    public abstract class Unit : MonoBehaviour
    {
        /// <summary>
        /// 유닛이 생성될 때 가지고 있는 SO 데이터 <br/>
        /// </summary>
        [SerializeField] protected UnitData baseUnitData;
        protected bool isInitialized;
        public UnitHp UnitHp { get; set; }

        protected virtual void Awake()
        {
        }

        public virtual void SetUnitData(UnitData data)
        {
            if (isInitialized)
                return;

            isInitialized = true;
            baseUnitData = data;
        }

        public virtual void ResetUnit()
        {
            isInitialized = false;
            baseUnitData = null;
        }
    }
}