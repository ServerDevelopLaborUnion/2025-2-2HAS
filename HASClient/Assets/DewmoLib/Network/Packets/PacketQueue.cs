using DewmoLib.Network.Core;
using System;
using System.Collections.Concurrent;

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
                if (_packets.Count <= 0)
                    break;

                _packets.TryDequeue(out var packet);
                packetManager.HandlePacket(session, packet);
            }
        }
    }
}
