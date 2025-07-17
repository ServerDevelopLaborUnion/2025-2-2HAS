using DewmoLib.Utiles;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._00.Work.CDH.Code.DummyClients
{
    public class DummyClientManager : MonoBehaviour
    {
        [SerializeField] private EventChannelSO packetChannel;
        [SerializeField] private DummyClient dummyClientPrefab;

        private List<DummyClient> dummyClients;

        public Action<DummyClientRotationEventHandler> OnRotationEvent;
        public Action<DummyClientMoveEventHandler> OnMoveEvent;

        private void Awake()
        {
            
        }

        private void Start()
        {
        }
    }
}

