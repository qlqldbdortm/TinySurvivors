using System;
using System.Collections.Generic;
using TinySurvivors.Core;
using TinySurvivors.Interface;
using UnityEngine;

namespace TinySurvivors.ObjectPool
{
    public class PoolManager : Singleton<PoolManager>
    {
        private readonly Dictionary<MonoBehaviour, object> pools = new();
        
        /// <summary>
        /// Pool 생성
        /// </summary>
        public TinyPool<T> CreatePool<T>(T prefab, int capacity, Transform parent = null)
            where T : MonoBehaviour, IPoolable
        {
            var type = typeof(T);

            if (pools.TryGetValue(prefab, out var p))
            {
                return p as TinyPool<T>;
            }

            var pool = new TinyPool<T>(prefab, capacity, parent);
            pools.Add(prefab, pool);

            return pool;
        }
        
        /// <summary>
        /// Pool에서 오브젝트를 깨내옴
        /// </summary>
        public T Spawn<T>(T prefab) where T : MonoBehaviour, IPoolable
        {
            if (!pools.TryGetValue(prefab, out var poolObj))
            {
                return null;
            }

            return (poolObj as TinyPool<T>)!.Spawn();
        }

        /// <summary>
        /// Pool에 오브젝트를 집어 넣음
        /// </summary>
        public void Despawn<T>(T prefab, T obj) where T : MonoBehaviour, IPoolable
        {
            if (!pools.TryGetValue(prefab, out var poolObj))
            {
                return;
            }

            (poolObj as TinyPool<T>)!.Despawn(obj);
        }
    }
}