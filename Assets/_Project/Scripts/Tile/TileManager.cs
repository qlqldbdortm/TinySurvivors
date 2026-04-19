using System.Collections.Generic;
using TinySurvivors.InGame;
using TinySurvivors.ObjectPool;
using TinySurvivors.Unit.Player;
using UnityEngine;

namespace TinySurvivors.Tile
{
    public class TileManager : MonoBehaviour
    {
        public Tile[] mapPrefabs;
        public int mapSize = 20;

        private Dictionary<Vector2Int, Tile> maps = new();

        private Vector2Int currentPlayerChunk;
        private Transform Player { get; set; }
        
        private void Start()
        {
            PlayerSpawner.OnPlayerSpawned += Init;
            
            foreach (var mapPrefab in mapPrefabs)
            {
                PoolManager.Instance.CreatePool(mapPrefab,10, new GameObject("Tilemap").transform);
            }
        }

        private void OnDisable()
        {
            PlayerSpawner.OnPlayerSpawned -= Init;
        }

        private void Update()
        {
            if (Player is null) return;

            Vector2Int playerChunk = GetChunk(Player.position);
            if (playerChunk != currentChunk)
            {
                currentChunk = playerChunk;
                UpdateMaps();
            }
        }

        private Vector2Int currentChunk;

        private void Init(Player player)
        {
            Player = player.transform;

            Player.position = Vector3.zero;
            
            Vector2[] offsets = {
                new (-mapSize / 2f, -mapSize / 2f), 
                new (-mapSize / 2f, mapSize / 2f),  
                new (mapSize / 2f, -mapSize / 2f), 
                new (mapSize / 2f, mapSize / 2f)   
            };

            foreach (var offset in offsets)
            {
                Vector2Int chunkPos = new Vector2Int(
                    Mathf.FloorToInt((offset.x / mapSize)),
                    Mathf.FloorToInt((offset.y / mapSize))
                );
                
                CreateMapAtChunk(chunkPos);
            }
            
            currentChunk = GetChunk(Player.position);
        }

        private Vector2Int GetChunk(Vector3 pos)
        {
            int chunkX = Mathf.FloorToInt((pos.x + mapSize / 2f) / mapSize);
            int chunkY = Mathf.FloorToInt((pos.y + mapSize / 2f) / mapSize);
            return new Vector2Int(chunkX, chunkY);
        }

        private void UpdateMaps()
        {
            Vector2Int[] offsets =
            {
                new (-1, -1), new (0, -1),
                new (-1, 0), new (0, 0)
            };

            foreach (var offset in offsets)
            {
                Vector2Int chunkPos = currentChunk + offset;
                if (!maps.ContainsKey(chunkPos))
                {
                    CreateMapAtChunk(chunkPos);
                }
            }

            List<Vector2Int> removeList = new List<Vector2Int>();
            
            foreach (var map in maps)
            {
                if (Mathf.Abs(map.Key.x - currentChunk.x) > 1 || Mathf.Abs(map.Key.y - currentChunk.y) > 1)
                {
                    removeList.Add(map.Key);
                }
            }

            foreach (var key in removeList)
            {
                PoolManager.Instance.Despawn(maps[key].Prefab, maps[key]);
                maps.Remove(key);
            }
        }

        private void CreateMapAtChunk(Vector2Int chunkPos)
        {
            if (maps.ContainsKey(chunkPos)) return;
            
            var prefab = mapPrefabs[Random.Range(0, mapPrefabs.Length)];
            Vector3 worldPos = new Vector3(
                chunkPos.x * mapSize + mapSize / 2f,
                chunkPos.y * mapSize + mapSize / 2f,
                0
            );

            var map = PoolManager.Instance.Spawn(prefab);
            map.SetPrefab(prefab);
            
            map.transform.position = worldPos;
            map.transform.rotation = Quaternion.identity;
            
            maps.Add(chunkPos, map);
        }
    }
}