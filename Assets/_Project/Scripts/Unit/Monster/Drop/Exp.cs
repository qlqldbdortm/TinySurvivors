using TinySurvivors.InGame;
using TinySurvivors.Interface;
using TinySurvivors.ObjectPool;
using TinySurvivors.Unit.Player;
using UnityEngine;

namespace TinySurvivors.Unit.Monster.Drop
{
    public class Exp : MonoBehaviour, IPoolable
    {
        /// <summary>
        /// 플레이어가 Exp와 닿았을 때 획득하는 경험치 량<br/>
        /// </summary>
        public float Value { get; set; }
        public Exp Prefab { get; private set; }


        [SerializeField] private float speed = 3f;
        private bool isForced;
        private Transform followTarget;

        public void Init()
        {
            enabled = false;
        }

        public void Release()
        {
            enabled = false;
        }

        public void SetPrefab(Exp prefab)
        {
            Prefab = prefab;
        }
        
        public void Magnet(Transform target)
        {
            enabled = true;
            isForced = true;
            followTarget = target;
        }

        public void OnHit(PlayerColliderHandler player)
        {
            player.BasePlayer.PlayerExp.GetExp(Value);
            DropManager.Instance.RemoveExpList(this);
            PoolManager.Instance.Despawn(Prefab, this);
        }
        
        private void Update()
        {
            if (!isForced || followTarget is null) return;

            float dist = Vector3.Distance(transform.position, followTarget.position);
            float currentSpeed = speed + dist * 5f;
            
            transform.position = Vector3.MoveTowards(transform.position, followTarget.position, currentSpeed * Time.deltaTime);
        }
    }
}