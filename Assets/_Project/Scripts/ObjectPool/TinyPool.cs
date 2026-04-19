using System.Collections.Generic;
using TinySurvivors.Interface;
using UnityEngine;

namespace TinySurvivors.ObjectPool
{
    public class TinyPool<T> where T : MonoBehaviour, IPoolable
    {
        private readonly Queue<T> pool;
        private readonly T prefab;
        private readonly Transform parentTransform;
        private readonly HashSet<T> activeObjects = new();
        
        public TinyPool(T prefab, int capacity, Transform parent = null)
        {
            this.prefab = prefab;
            parentTransform = parent;
            pool = new Queue<T>(capacity);

            for (int i = 0; i < capacity; i++)
            {
                T newObj = CreateNewObject();
                PrewarmDespawn(newObj);
            }
        }
        /// <summary>
        /// 플에서 오브젝트를 1개 꺼내와 사용
        /// </summary>
        /// <return>풀에서 꺼내온 오브젝트</return>
        public T Spawn()
        {
            // 풀에 오브젝트가 있을 경우 풀에서 꺼내 사용하고 없을 경우 새로 생성해서 사용
            var getObj = pool.Count > 0 ? pool.Dequeue() : CreateNewObject();
            getObj.gameObject.SetActive(true);
            getObj.Init();
            
            activeObjects.Add(getObj);
            
            return getObj;
        }

        /// <summary>
        /// 사용이 끝난 오브젝트를 풀에 반환
        /// </summary>
        /// <param name="obj">풀에 되돌려 놓을 오브젝트</param>
        public void Despawn(T obj)
        {
            if (!activeObjects.Remove(obj)) return;
            
            obj.Release();
            obj.gameObject.SetActive(false);
            
            if (parentTransform is not null && obj.transform.parent != parentTransform)
            {
                obj.transform.parent = parentTransform;
            }
            
            pool.Enqueue(obj);
        }
        
        private void PrewarmDespawn(T obj)
        {
            obj.Release();
            obj.gameObject.SetActive(false);

            if (parentTransform is not null)
            {
                obj.transform.parent = parentTransform;
            }

            pool.Enqueue(obj);
        }
        
        /// <summary>
        /// 새로운 오브젝트를 생성하는 메서드
        /// </summary>
        private T CreateNewObject()
        {
            T newObj = Object.Instantiate(prefab, parentTransform);
            newObj.gameObject.SetActive(false);
            
            return newObj;
        }
    }
}