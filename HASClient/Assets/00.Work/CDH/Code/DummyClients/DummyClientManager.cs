using DewmoLib.Dependencies;
using DewmoLib.ObjectPool.RunTime;
using DewmoLib.Utiles;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._00.Work.CDH.Code.DummyClients
{
    public class DummyClientManager : MonoBehaviour
    {
        [SerializeField] private PoolItemSO dummyClientItemSO;

        [Inject] private PoolManagerMono poolManager;
        private List<DummyClient> dummyClients;

        private void Awake()
        {
            dummyClients = new List<DummyClient>();
        }

        private void Start()
        {
            for(int i = 0; i < 4; ++i)
            {
                DummyClient newDummyClient = poolManager.Pop<DummyClient>(dummyClientItemSO);
                newDummyClient.Initialize(i);
                dummyClients.Add(newDummyClient);
            }
        }
    }
}

