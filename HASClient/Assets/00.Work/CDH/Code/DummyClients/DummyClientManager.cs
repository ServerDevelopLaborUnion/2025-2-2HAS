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

        private void Awake()
        {
            dummyClients = new List<DummyClient>();
        }

        private void Start()
        {
            for(int i = 0; i < 4; ++i)
            {
                DummyClient newDummyClient = Instantiate(dummyClientPrefab, transform);
                dummyClients.Add(newDummyClient);
            }
        }
    }
}

