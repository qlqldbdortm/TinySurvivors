using System;
using System.Collections.Generic;
using TinySurvivors.Core;
using TinySurvivors.ObjectPool;
using TinySurvivors.Projectile;
using TinySurvivors.Unit.Monster;
using UnityEngine;

namespace TinySurvivors.InGame
{
    public class MonsterManager : Singleton<MonsterManager>
    {
        private readonly HashSet<Monster> enemies = new();
        
        [SerializeField] private BaseProjectile[] createPoolObject;


        private void Start()
        {
            foreach (var projectile in createPoolObject)
            {
                PoolManager.Instance.CreatePool(projectile, 200, new GameObject(projectile.name).transform);
            }
        }


        /// <summary>
        /// 몬스터가 Pool에서 꺼내질 때 등록
        /// </summary>
        /// <param name="monster"></param>
        public void Register(Monster monster)
        {
            enemies.Add(monster);
        }

        /// <summary>
        /// 몬스터가 Pool에 들어갈 때 해제
        /// </summary>
        /// <param name="monster"></param>
        public void Unregister(Monster monster)
        {
            enemies.Remove(monster);
        }
        
        /// <summary>
        /// Player와 가장 가까운 몬스터의 위치를 찾아서 반환하는 메서드
        /// </summary>
        /// <param name="origin">플레이어</param>
        /// <param name="range">범위</param>
        public Transform FindClosest(Transform origin, float range)
        {
            Transform closest = null;
            float minDist = range * range;

            foreach (var e in enemies)
            {
                if (!e.gameObject.activeInHierarchy)
                    continue;

                float dist = ((Vector2)(origin.transform.position - e.transform.position)).sqrMagnitude;
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = e.transform;
                }
            }

            return closest;
        }
        
        public IEnumerable<Monster> Enemies => enemies;
    }
}