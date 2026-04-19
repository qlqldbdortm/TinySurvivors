using System;
using TinySurvivors.InGame;
using TinySurvivors.Utility;
using TMPro;
using UnityEngine;

namespace TinySurvivors.UI.HUD
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;

        private Timer Timer;
        
        private void Awake()
        {
            timerText = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            if (InGameManager.Instance?.Timer != null)
            {
                Bind(InGameManager.Instance.Timer);
            }
        }

        private void OnDisable()
        {
            Unbind();
        }

        private void Bind(Timer timer)
        {
            Timer = timer;
            Timer.OnTimeChanged += OnTimeChanged;
        }

        private void Unbind()
        {
            if (Timer != null)
            {
                Timer.OnTimeChanged -= OnTimeChanged;
                Timer = null;
            }
        }

        public void Refresh()
        {
            timerText.text = "00 : 00";
        }

        private void OnTimeChanged(float time)
        {
            int minutes = (int)(time / 60);
            int seconds = (int)(time % 60);
            timerText.text = $"{minutes:00} : {seconds:00}";
        }
    }
}