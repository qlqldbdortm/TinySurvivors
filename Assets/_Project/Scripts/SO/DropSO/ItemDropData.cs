using System;
using TinySurvivors.Enum;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TinySurvivors.SO.ExpDropSO
{
    [CreateAssetMenu(fileName = "ItemDropData", menuName = "ScriptableObjects/Drop/02.ItemDropData", order = 2)]
    public class ItemDropData : ScriptableObject
    {
        [Serializable]
        public struct ItemTable
        {
            [Tooltip("아이템 종류")] 
            public ItemType itemType;
            
            [Tooltip("아이템 드롭 확률")] [Range(0, 500)] 
            public int weight;
        }
        
        public ItemTable[] tables;
        
        public ItemType GetRandom()
        {
            int total = 0;
            foreach (var e in tables)
            {
                total += e.weight;
            }

            int rand = Random.Range(0, total);

            int acc = 0;
            foreach (var e in tables)
            {
                acc += e.weight;
                if (rand < acc) return e.itemType;
            }

            return tables[0].itemType;
        }
    }
}