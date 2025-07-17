using DewmoLib.Utiles;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._00.Work.CDH.Code.DummyClients
{
    public class DummyClientManager : MonoBehaviour
    {
        [SerializeField] private EventChannelSO packetChannel;
        [SerializeField] private DummyClient dummyClientPrefab;

        private List<DummyClient> dummyClients;

        private void Awake()
        {
            
        }

        private void Start()
        {
            packetChannel.AddListener<DummyClientMoveEventHandler>(DummyClientMoveEventHandler);
            packetChannel.AddListener<DummyClientRotationEventHandler>(DummyClientRotationEventHandler);
        }

        private void DummyClientRotationEventHandler(DummyClientRotationEventHandler handler)
        {
        }

        private void DummyClientMoveEventHandler(DummyClientMoveEventHandler handler)
        {
        }
    }
}

