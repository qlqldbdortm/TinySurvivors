using System;

namespace TinySurvivors.Unit
{
    [Serializable]
    public class UnitStat
    {
        public float BaseValue { get; private set; }
        public float Value => BaseValue + bonus;

        private float bonus;
        public event Action<float, float> OnValueChanged;
        public UnitStat(float baseValue)
        {
            BaseValue = baseValue;
        }

        public void AddBonus(float value)
        {
            float oldValue = Value;
            bonus += value;
            OnValueChanged?.Invoke(oldValue, Value);
        }

        public void RemoveBonus(float value)
        {
            float oldValue = Value;
            bonus -= value;
            OnValueChanged?.Invoke(oldValue, Value);
        }
    }
}