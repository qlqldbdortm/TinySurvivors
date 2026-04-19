using UnityEngine;
using UnityEngine.UI;

namespace TinySurvivors.UI.PlayerUI
{
    public class PlayerHpUI : MonoBehaviour
    {
        [SerializeField] private Slider hpBar;
        private Vector3 originScale;
        
        private void Awake()
        {
            originScale = transform.localScale; 
        }
        
        void LateUpdate()
        {
            Vector3 parentScale = transform.parent.lossyScale;

            transform.localScale = new Vector3(
                Mathf.Abs(originScale.x) * Mathf.Sign(parentScale.x),
                originScale.y,
                originScale.z
            );
        }
        
        /// <summary>
        /// HP Bar를 초기화 하는 메서드
        /// </summary>
        public void Init(float maxHp)
        {
            hpBar.maxValue = maxHp;
            hpBar.value = maxHp;
        }
        
        /// <summary>
        /// HP Bar의 값을 변경하는 메서드
        /// </summary>
        public void SetHpBar(float value)
        {
            hpBar.value = value;
        }

        public void SetMaxHpBar(float oldValue, float newValue)
        {
            hpBar.maxValue = newValue;
        }
    }
}