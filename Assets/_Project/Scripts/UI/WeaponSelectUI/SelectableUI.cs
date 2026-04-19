using TinySurvivors.InGame;
using TinySurvivors.SO.RewardSO;
using TinySurvivors.SO.WeaponSO;
using TinySurvivors.Unit.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TinySurvivors.UI.WeaponSelectUI
{
    public class SelectableUI : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemLevel;
        [SerializeField] private TextMeshProUGUI itemDescription;

        private WeaponData Data { get; set; }
        private void Awake()
        {
            button.onClick.AddListener(SelectItem);
        }
        
        /// <summary>
        /// 아이템 선택창을 초기화 하는 메서드
        /// </summary>
        public void Init(WeaponData data, PlayerWeaponHandler handler)
        {
            // TODO: 플레이어가 레벨업을 하게되면 선택해야하는 UI
            //       WeaponDatabase에 존재하는 무기중 랜덤으로 선택하게되어 초기화 해야함
            Data = data;
            
            itemIcon.sprite = data.weaponIcon;
            itemName.text = data.weaponName;

            int currentLevel = handler.GetCurrentWeaponLevel(data);
            
            if (handler.HasWeapon(data) && currentLevel >= data.maxLevel)
            {
                button.interactable = false;
                itemLevel.text = "MAX";
                itemDescription.text = "최대 레벨";
                return;
            }
            
            itemLevel.text = $"Lv {currentLevel + 1}";

            itemDescription.text =
                handler.HasWeapon(data)
                    ? "무기 레벨 +1"
                    : "새 무기 획득";
        }
        
        public void InitHealth(HealthRewardData reward)
        {
            Data = null;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                InGameManager.Instance.Player.PlayerStat.MaxHp
                    .AddBonus(reward.increaseMaxHp);

                FindObjectOfType<SelectUI>().CloseUI();
            });

            itemIcon.sprite = reward.rewardIcon;
            itemName.text = reward.rewardName;
            itemLevel.text = "";
            itemDescription.text = reward.rewardDescription;
        }
        
        /// <summary>
        /// 아이템을 선택하는 메서드
        /// </summary>
        private void SelectItem()
        {
            var handler = InGameManager.Instance.Player.WeaponHandler;

            if (handler.IsWeaponMaxLevel(Data))
                return;

            handler.AddWeapon(Data);
            FindObjectOfType<SelectUI>().CloseUI();
        }
    }
}