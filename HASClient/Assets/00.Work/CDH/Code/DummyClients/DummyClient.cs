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

        public Action<MoveEventHandler> OnMoveEvent;
        public Action<RotateEventHandler> OnRotationEvent;

        public int Id { get; set; }

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

        private void HandleDummyClientMoveEvent(MoveEventHandler evt)
        {
            if(evt.index != Id)
                return;

            OnMoveEvent?.Invoke(evt);
        }

        private void HandleDummyClientRotationEvent(RotateEventHandler evt)
        {
            if (evt.index != Id)
                return;

            OnRotationEvent?.Invoke(evt);
        }

    }
}
