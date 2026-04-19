using System;
using UnityEngine;

namespace TinySurvivors.Unit
{
    public class UnitHp
    {
        public float CurrentHp { get; private set; }
        public float MaxHp => baseStat.MaxHp.Value;
        
        private BaseStat baseStat;
        private bool isInvincible;
        
        /// <summary>
        /// 플레이어가 몬스터에게 피격당했을 때 발생하는 이벤트
        /// </summary>
        public event Action<float> OnDamaged;
        
        /// <summary>
        /// 플레이어가 체력을 회복했을 때 발생하는 이벤트
        /// </summary>
        public event Action<float> OnHealed;
        
        /// <summary>
        /// 플레이어의 최대 체력의 변동이 있을 때 발생하는 이벤트 <br/>
        /// ex) 레벨업을 하여 획득한 아이템을 통한 최대 체력이 증가한 경우
        /// </summary>
        public event Action<float, float> OnMaxHpChanged;
        public event Action OnDeath;

        public UnitHp(BaseStat stat)
        {
            baseStat = stat;
            CurrentHp = baseStat.MaxHp.Value;
            
            baseStat.MaxHp.OnValueChanged += OnMaxHpValueChanged;
        }

        public void TakeDamage(float damage)
        {
            if (isInvincible) return;
            
            CurrentHp = Mathf.Max(CurrentHp - damage, 0);
            OnDamaged?.Invoke(CurrentHp);
            
            if (CurrentHp <= 0)
            {
                OnDeath?.Invoke();
            }
        }

        public void Heal(float heal)
        {
            if (CurrentHp <= 0) return;
            
            CurrentHp = Mathf.Min(CurrentHp + heal, MaxHp);
            
            OnHealed?.Invoke(CurrentHp);
        }

        private void OnMaxHpValueChanged(float oldMax, float newMax)
        {
            float delta = newMax - oldMax;
            
            CurrentHp = Mathf.Min(CurrentHp + delta, newMax);
            
            OnMaxHpChanged?.Invoke(oldMax, newMax);
            OnHealed?.Invoke(CurrentHp);
        }
        
        public void SetInvincible(bool invincible)
        {
            isInvincible = invincible;
        }

        public void Refresh()
        {
            CurrentHp = MaxHp;
        }
    }
}