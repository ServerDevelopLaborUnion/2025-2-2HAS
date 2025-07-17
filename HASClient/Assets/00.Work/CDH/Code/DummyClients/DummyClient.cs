using UnityEngine;
using Assets._00.Work.YHB.Scripts.Entities;
using System.Diagnostics.Tracing;
using DewmoLib.Utiles;
using System;

namespace Assets._00.Work.CDH.Code.DummyClients
{
    public class DummyClient : Entity
    {
        [SerializeField] private EventChannelSO packetChannel;

        public Action<DummyClientMoveEventHandler> OnMoveEvent;
        public Action<DummyClientRotationEventHandler> OnRotationEvent;

        public int Id { get; set; }

        private void Start()
        {
            packetChannel.AddListener<DummyClientMoveEventHandler>(HandleDummyClientMoveEvent);
            packetChannel.AddListener<DummyClientRotationEventHandler>(HandleDummyClientRotationEvent);
        }

        private void HandleDummyClientMoveEvent(DummyClientMoveEventHandler evt)
        {
            OnMoveEvent?.Invoke(evt);
        }

        private void HandleDummyClientRotationEvent(DummyClientRotationEventHandler evt)
        {
            OnRotationEvent?.Invoke(evt);
        }

    }
}
