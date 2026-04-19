using TinySurvivors.Interface;
using UnityEngine;

namespace TinySurvivors.Tile
{
    public class Tile : MonoBehaviour, IPoolable
    {
        public Tile Prefab { get; private set; }
        
        public void Init()
        {
            
        }

        public void Release()
        {
            
        }

        public void SetPrefab(Tile prefab)
        {
            Prefab = prefab;
        }
    }
}