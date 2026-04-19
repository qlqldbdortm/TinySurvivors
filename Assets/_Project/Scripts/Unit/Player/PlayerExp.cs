using System;
using UnityEngine;

namespace TinySurvivors.Unit.Player
{
    public class PlayerExp : MonoBehaviour
    {
        [SerializeField] private float baseExp = 10f;
        [SerializeField] private float growth = 1.45f;

        public int Level { get; private set; } = 1;
        public float CurrentExp { get; private set; }
        public float MaxExp => baseExp * Mathf.Pow(Level, growth);
        
        /// <summary>
        /// 경험치 획득 시 UI 표시 이벤트
        /// </summary>
        public event Action<float> OnValueChanged;
        
        /// <summary>
        /// 레벨업 시 발생하는 이벤트
        /// </summary>
        public event Action OnLevelUp;
        

        public void GetExp(float value)
        {
            CurrentExp += value;

            while (CurrentExp >= MaxExp)
            {
                CurrentExp -= MaxExp;
                LevelUp();
            }
            
            OnValueChanged?.Invoke(CurrentExp);
        }

        private void LevelUp()
        {
            Level++;
            OnLevelUp?.Invoke(); // TODO: 레벨업 시 무기 선택 UI가 나오도록
        }
    }
}