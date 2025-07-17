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

        public Action<MoveEventHandler> OnMoveEvent;
        public Action<RotateEventHandler> OnRotationEvent;

        public int Id { get; private set; }

        [field : SerializeField] public PoolItemSO PoolItem { get; private set; }

        public GameObject GameObject => gameObject;

        private void Start()
        {
            packetChannel.AddListener<MoveEventHandler>(HandleDummyClientMoveEvent);
            packetChannel.AddListener<RotateEventHandler>(HandleDummyClientRotationEvent);
        }

        private void OnDestroy()
        {
            packetChannel.RemoveListener<MoveEventHandler>(HandleDummyClientMoveEvent);
            packetChannel.RemoveListener<RotateEventHandler>(HandleDummyClientRotationEvent);
        }

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

        private void HandleDummyClientMoveEvent(MoveEventHandler evt)
        {
            print(evt.index);
            print(Id);

            if(evt.index != Id)
                return;

            OnMoveEvent?.Invoke(evt);
        }

        private void HandleDummyClientRotationEvent(RotateEventHandler evt)
        {
            print(evt.index);
            print(Id);

            if (evt.index != Id)
                return;

            OnRotationEvent?.Invoke(evt);
        }
    }
}
