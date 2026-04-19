using TinySurvivors.ObjectPool;
using UnityEngine;

namespace TinySurvivors.Test
{
    public class TestPool : MonoBehaviour
    {
        [SerializeField] private TestPoolObject prefab;
        [SerializeField] private int capacity = 5;

        private TinyPool<TestPoolObject> pool;

        private void Start()
        {
            pool = new TinyPool<TestPoolObject>(prefab, capacity, transform);
            
            var a = pool.Spawn();
            var b = pool.Spawn();
            var c = pool.Spawn();

            Debug.Log("3개 Spawn 완료");
            
            pool.Despawn(a);
            pool.Despawn(b);

            Debug.Log("2개 Despawn 완료");

            var d = pool.Spawn();
            Debug.Log($"재사용된 오브젝트: {d.name}");
        }
    }
}