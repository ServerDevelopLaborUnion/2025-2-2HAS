using DewmoLib.Network.Core;
using DewmoLib.Utiles;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace DewmoLib.Network.Packets
{

    public abstract class PacketManager
    {
        Dictionary<ushort, Func<ArraySegment<byte>, IPacket>> _onRecv = new();
        Dictionary<ushort, Action<PacketSession, IPacket>> _handler = new Dictionary<ushort, Action<PacketSession, IPacket>>();

        public abstract void Register();

        protected void RegisterHandler<T>(ushort id, Action<PacketSession, IPacket> handler) where T : IPacket, new()
        {
            _onRecv.Add((ushort)id, PacketUtility.CreatePacket<T>);
            _handler.Add((ushort)id, handler);
        }

        public IPacket OnRecvPacket(ArraySegment<byte> buffer)
        {
            ushort packetId = PacketUtility.ReadPacketID(buffer);
            Func<ArraySegment<byte>, IPacket> func = null;
            if (_onRecv.TryGetValue(packetId, out func))
                return func.Invoke(buffer);
            return default;
        }
        public void HandlePacket(PacketSession session, IPacket packet)
        {
            Debug.Log(_handler);
            Debug.Log(packet.Protocol);
            if (_handler.ContainsKey(packet.Protocol))
                _handler[packet.Protocol].Invoke(session, packet);
            else
            {
                Debug.Log("Fail: " + packet.Protocol);
                throw new NullReferenceException();
            }
        }
    }
}