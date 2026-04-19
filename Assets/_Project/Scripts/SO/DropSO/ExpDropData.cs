using System;
using TinySurvivors.Enum;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.SO.ExpDropSO
{
    [CreateAssetMenu(fileName = "ExpDropData", menuName = "ScriptableObjects/Drop/01.ExpDropData", order = 1)]
    public class ExpDropData : ScriptableObject
    {
        [Serializable]
        public struct ExpTable
        {
            [Tooltip("경험치 종류")] 
            public ExpType expType;
            
            [Tooltip("경험치 드롭 확률")] [Range(0, 100)] 
            public int weight;
        }
        
        public ExpTable[] tables;
        
        public ExpType GetRandom()
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
                if (rand < acc) return e.expType;
            }

            return tables[0].expType;
        }
    }
}