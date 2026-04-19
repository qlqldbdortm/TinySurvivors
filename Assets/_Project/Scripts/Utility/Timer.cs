using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TinySurvivors.Utility
{
    public class Timer
    {
        public float NowTime => time;
        public event Action<float> OnTimeChanged;
        
        private float time;
        private CancellationTokenSource token;
        
        /// <summary>
        /// 타이머를 시작하는 메서드
        /// </summary>
        public void Start()
        {
            if (token != null) return;
            _ = TimerAsync();
        }

        /// <summary>
        /// 타이머를 멈추는 메서드<br/>
        /// 0초로 초기화
        /// </summary>
        public void Stop()
        {
            Pause();
            Reset();
        }

        /// <summary>
        /// 타이머를 일시정지하는 메서드
        /// </summary>
        public void Pause()
        {   
            token?.Cancel();
            token = null;
        }

        /// <summary>
        /// 타이머를 초기화 하는 메서드
        /// </summary>
        public void Reset()
        {
            time = 0f;
        }

        /// <summary>
        /// 타이머를 증가시키는 UniTask-async 메서드
        /// </summary>
        private async UniTask TimerAsync()
        {
            token = new CancellationTokenSource();
            while (true)
            {
                await UniTask.Yield(token.Token);
                time += Time.deltaTime;
                OnTimeChanged?.Invoke(time);
            }
        }
    }
}