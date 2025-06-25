using DewmoLib.Network.Core;
using DewmoLib.Utiles;
using System;
using System.Collections.Generic;

namespace DewmoLib.Network.Packets
{
    public class PacketQueue
    {
        private object _lock = new object();
        private Queue<IPacket> _packets = new Queue<IPacket>();
        public PacketManager packetManager;
        public PacketQueue(PacketManager manager)
        {
            packetManager = manager;
        }

        public void Push(ArraySegment<byte> packet)
        {
            var pkt = packetManager.OnRecvPacket(packet);
            lock (_lock)
            {
                _packets.Enqueue(pkt);
            }
        }
        public void Clear()
        {
            _packets.Clear();
        }
        public void FlushPackets(PacketSession session)
        {
            while (true)
            {
                lock (_lock)
                {
                    if (_packets.Count <= 0)
                        break;

                    IPacket packet = _packets.Dequeue();
                    packetManager.HandlePacket(session, packet);
                }
            }
        }
    }
}
