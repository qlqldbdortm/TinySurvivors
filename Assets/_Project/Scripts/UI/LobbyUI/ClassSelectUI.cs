using TinySurvivors.Core;
using UnityEngine;

namespace TinySurvivors.UI.LobbyUI
{
    public class ClassSelectUI : MonoBehaviour
    {
        [Header("UI 요소")]
        [Tooltip("직업을 선택하는 UI 프리펩")]
        [SerializeField] private ClassSelectableUI prefab;
        
        [Tooltip("프리펩이 생성될 위치")]
        [SerializeField] private GameObject selectPanel;
        
        private void Awake()
        {
            int index = 0;
            foreach (var playable in GameManager.Instance.Database)
            {
                var selectable = Instantiate(prefab, selectPanel.transform);
                selectable.Init(playable, index++);

            }
        }

        public void OpenUI()
        {
            gameObject.SetActive(true);
        }
    }
}