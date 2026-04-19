using System;
using System.Collections.Generic;
using _Project.Scripts.SO.ExpDropSO;
using TinySurvivors.Core;
using TinySurvivors.Enum;
using TinySurvivors.Item;
using TinySurvivors.ObjectPool;
using TinySurvivors.SO.ExpDropSO;
using TinySurvivors.Unit.Monster.Drop;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TinySurvivors.InGame
{
    public class DropManager : Singleton<DropManager>
    {
        [SerializeField] private Exp smallExp;
        [SerializeField] private Exp largeExp;
        
        [SerializeField] private BaseItem healItem;
        [SerializeField] private BaseItem magnetItem;
        [SerializeField] private BaseItem bombItem;
        
        [SerializeField] private ExpDropData expDropData;
        [SerializeField] private ItemDropData itemDropData; 
        
        private readonly List<Exp> activeExps = new();
        
        public IEnumerable<Exp> ActiveExps => activeExps;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            PoolManager.Instance.CreatePool(smallExp,500, new GameObject("Small Exp").transform);
            PoolManager.Instance.CreatePool(largeExp,500, new GameObject("Large Exp").transform);
            PoolManager.Instance.CreatePool(healItem,20, new GameObject("Heal Item").transform);
            PoolManager.Instance.CreatePool(magnetItem,20, new GameObject("Magnet Item").transform);
            PoolManager.Instance.CreatePool(bombItem,20, new GameObject("Bomb Item").transform);
        }
        /// <summary>
        /// 몬스터가 죽었을 때 dropTable에 따라서 경험치를 떨어뜨리도록 하는 메서드
        /// </summary>
        /// <param name="pos">죽은 몬스터의 위치</param>
        public void DropExp(Vector3 pos)
        {
            var type = expDropData.GetRandom();
            var prefab = type == ExpType.Small ? smallExp : largeExp;

            var exp = PoolManager.Instance.Spawn(prefab);
            exp.SetPrefab(prefab);
            exp.Value = type == ExpType.Small ? 2f : 5f;
            exp.transform.position = pos;
            
            activeExps.Add(exp);
        }

        public void BossDropExp(Vector3 pos, int count, float radius)
        {
            for (int i = 0; i < count; i++)
            {
                float angle = (360f / count) * i;
                float rad = angle * Mathf.Deg2Rad;

                Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * (Mathf.Sqrt(Random.value) * radius);

                DropExp(pos + offset);
            }
        }
        
        public void RemoveExpList(Exp exp)
        {
            activeExps.Remove(exp);
        }

        public void DropItem(Vector3 pos)
        {
            var type = itemDropData.GetRandom();
            var prefab = type switch 
            {
                ItemType.None => null,
                ItemType.Heal => healItem,
                ItemType.Magnet => magnetItem,
                ItemType.Bomb => bombItem,
                _ => null,
            };
            
            if (prefab is null) return;
            
            var item = PoolManager.Instance.Spawn(prefab);
            item.SetPrefab(prefab);
            item.transform.position = pos;
        }
    }
}