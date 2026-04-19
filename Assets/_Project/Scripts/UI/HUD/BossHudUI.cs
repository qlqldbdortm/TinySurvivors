using TinySurvivors.Unit.Monster.Boss;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TinySurvivors.UI.HUD
{
    public class BossHudUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI bossNameText;
        [SerializeField] private Slider bossHpBar;

        public void BossSpawn(BossMonster boss)
        {
            gameObject.SetActive(true);
            bossNameText.text = boss.GetName();
            bossHpBar.maxValue = boss.UnitHp.MaxHp;
            bossHpBar.value = boss.UnitHp.CurrentHp;
        }

        public void SetHp(float value)
        {
            bossHpBar.value = value;
        }
        
        public void BossDespawn()
        {
            gameObject.SetActive(false);
        }
    }
}