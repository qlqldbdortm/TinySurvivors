using System.Collections.Generic;
using TinySurvivors.Core;
using TinySurvivors.ObjectPool;
using TinySurvivors.UI;
using TinySurvivors.Unit.Monster;
using TinySurvivors.Unit.Monster.Boss;
using UnityEngine;

namespace TinySurvivors.InGame
{
    public class Spawner : Singleton<Spawner>
    {
        [SerializeField] private List<SpawnPhase> phases;
        [SerializeField] private float spawnRadius = 10f;

        private float elapsedTime;
        private float spawnTimer;
        private SpawnPhase currentPhase;

        protected override void Awake()
        {
            base.Awake();
            InitPools();
        }

        private void Update()
        {
            elapsedTime += Time.deltaTime;
            currentPhase = GetCurrentPhase();
            if (currentPhase == null) return;

            SpawnBoss();

            spawnTimer += Time.deltaTime;
            if (spawnTimer >= currentPhase.spawnDelay)
            {
                spawnTimer = 0f;
                SpawnMonster();
            }
        }

        private void InitPools()
        {
            HashSet<Monster> prefabs = new();

            foreach (var phase in phases)
            {
                foreach (var m in phase.monsters)
                {
                    prefabs.Add(m.prefab);
                }

                if (phase.spawnBoss && phase.bossPrefab != null)
                {
                    prefabs.Add(phase.bossPrefab);
                }
            }

            foreach (var prefab in prefabs)
            {
                if (prefab is not BossMonster)
                {
                    PoolManager.Instance.CreatePool(prefab, 200, new GameObject($"{prefab.name}").transform);
                    continue;
                }

                PoolManager.Instance.CreatePool(prefab, 1, new GameObject($"{prefab.name}").transform);
            }
        }

        private SpawnPhase GetCurrentPhase()
        {
            foreach (var phase in phases)
            {
                if (elapsedTime >= phase.startTime && elapsedTime < phase.endTime)
                {
                    return phase;
                }
            }

            return null;
        }

        private void SpawnMonster()
        {
            if (currentPhase.monsters == null || currentPhase.monsters.Count == 0) return;

            Monster prefab = GetRandomMonster(currentPhase.monsters);
            Monster monster = PoolManager.Instance.Spawn(prefab);
            monster.SetPrefab(prefab);
            monster.transform.position = GetSpawnPosition();
        }

        private void SpawnBoss()
        {
            if (!currentPhase.spawnBoss || currentPhase.bossSpawned) return;

            Monster boss = PoolManager.Instance.Spawn(currentPhase.bossPrefab);
            boss.SetPrefab(currentPhase.bossPrefab);
            boss.transform.position = GetSpawnPosition();

            currentPhase.bossSpawned = true;

            boss.MonsterAnimationEventHandler.OnBossDeath += OnBossDeath;
        }

        private void OnBossDeath()
        {
            if (!currentPhase.lastStage) return;
            InGameUIManager.Instance.Clear();
            InGameManager.Instance.GameClear();
        }

        private Monster GetRandomMonster(List<MonsterSpawn> list)
        {
            int total = 0;

            foreach (var monsterSpawn in list)
            {
                total += monsterSpawn.rate;
            }

            int rand = Random.Range(0, total);
            int cur = 0;

            foreach (var monsterSpawn in list)
            {
                cur += monsterSpawn.rate;
                if (rand < cur)
                {
                    return monsterSpawn.prefab;
                }
            }

            return list[0].prefab;
        }

        private Vector3 GetSpawnPosition()
        {
            Vector2 dir = Random.insideUnitCircle.normalized;
            return InGameManager.Instance.Player.transform.position + (Vector3)(dir * spawnRadius);
        }

# if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
# endif
    }
}