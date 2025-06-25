using DewmoLib.Network.Packets;
using DewmoLib.Utiles;
using UnityEngine;

namespace AKH.Network
{
    public class ClientPacketManager : PacketManager
    {
        public ClientPacketManager(EventChannelSO packetChannel) : base(packetChannel)
        {
        }

        public override void Register()
        {
            Debug.Log("asd");
        }
    }
}
