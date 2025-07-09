using ServerCore;
using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace DewmoLib.Network.Packets
{
    public class PacketQueue
    {
        private ConcurrentQueue<IPacket> _packets = new();
        public PacketManager packetManager;
        public PacketQueue(PacketManager manager)
        {
            packetManager = manager;
        }

        public void Push(ArraySegment<byte> packet)
        {
            var pkt = packetManager.OnRecvPacket(packet);
            _packets.Enqueue(pkt);
        }
        public void Clear()
        {
            _packets.Clear();
        }
        public void FlushPackets(PacketSession session)
        {
            while (true)
            {
                if (_packets.TryDequeue(out var packet))
                {
                    Debug.Log(packet.Protocol);
                    packetManager.HandlePacket(session, packet);
                }
                else
                    break;
            }
        }
    }
}
