using TinySurvivors.Interface;
using UnityEngine;

namespace TinySurvivors.Test
{
    public class TestPoolObject : MonoBehaviour, IPoolable
    {
        public int initCount;
        public int releaseCount;

        public void Init()
        {
            initCount++;
            Debug.Log($"Init 호출됨 : {name} ({initCount})");
        }

        public void Release()
        {
            releaseCount++;
            Debug.Log($"Release 호출됨 : {name} ({releaseCount})");
        }
    }
}