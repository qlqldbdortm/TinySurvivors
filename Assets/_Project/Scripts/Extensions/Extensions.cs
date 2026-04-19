using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace TinySurvivors.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// InputSystem에 이벤트를 구독하는 메서드
        /// </summary>
        public static void AddInputAction(this InputAction action, Action<InputAction.CallbackContext> callback,
            bool started, bool performed, bool canceled)
        {
            if (started) action.started += callback;
            if (performed) action.performed += callback;
            if (canceled) action.canceled += callback;
        }

        /// <summary>
        /// InputSystem에 넣어둔 이벤트를 구독 해제하는 메서드
        /// </summary>
        public static void RemoveInputAction(this InputAction action, Action<InputAction.CallbackContext> callback,
            bool started, bool performed, bool canceled)
        {
            if (started) action.started -= callback;
            if (performed) action.performed -= callback;
            if (canceled) action.canceled -= callback;
        }
        
        /// <summary>
        /// 아이템을 랜덤으로 선택하기 위해서 리스트를 무작위로 돌리는 메서드
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        public static void Shuffle<T>(this IList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int rand = UnityEngine.Random.Range(i, list.Count);
                (list[i], list[rand]) = (list[rand], list[i]);
            }
        }
    }
}