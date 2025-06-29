using AKH.Scripts.Packet;
using DewmoLib.Dependencies;
using DewmoLib.Utiles;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.EventSystem
{
    [Provide]
    public class PacketResponsePublisher : MonoBehaviour, IDependencyProvider
    {
        [SerializeField] private EventChannelSO packetChannel;
        private Dictionary<PacketID, Action<bool>> _events = new();
        private void Awake()
        {
            packetChannel.AddListener<PacketResponse>(HandleResponse);
        }

        private void HandleResponse(PacketResponse response)
        {
            if (_events.ContainsKey(response.packetId))
                _events[response.packetId]?.Invoke(response.success);
        }

        public void AddListener(PacketID id, Action<bool> handler)
        {
            if (_events.ContainsKey(id))
                _events[id] += handler;
            else
                _events[id] = handler;
        }
        public void RemoveListener(PacketID id, Action<bool> handler)
        {
            _events[id] -= handler;
        }
    }
}
