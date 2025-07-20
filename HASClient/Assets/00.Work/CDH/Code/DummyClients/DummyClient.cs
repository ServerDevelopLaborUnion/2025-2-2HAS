using UnityEngine;
using Assets._00.Work.YHB.Scripts.Entities;
using System.Diagnostics.Tracing;
using DewmoLib.Utiles;
using System;
using DewmoLib.ObjectPool.RunTime;

namespace Assets._00.Work.CDH.Code.DummyClients
{
    public class DummyClient : Entity, IPoolable
    {
        [SerializeField] private EventChannelSO packetChannel;

        private Pool myPool;

        public Action<Vector2> OnMoveEvent;
        public Action<Quaternion> OnRotationEvent;

        public int Id { get; private set; }

        [field : SerializeField] public PoolItemSO PoolItem { get; private set; }

        public GameObject GameObject => gameObject;


        public void SetUpPool(Pool pool)
        {
            myPool = pool;
        }

        public void ResetItem()
        {
        }

        public void Initialize(int id)
        {
            Id = id;
        }

        public void HandleDummyClientMove(Vector2 move)
        {
            OnMoveEvent?.Invoke(move);
        }

        public void HandleDummyClientRotation(Quaternion rotation)
        {
            OnRotationEvent?.Invoke(rotation);
        }
    }
}
