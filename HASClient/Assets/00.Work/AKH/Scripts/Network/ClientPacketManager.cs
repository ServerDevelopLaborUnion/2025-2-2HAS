using DewmoLib.Network.Packets;
using DewmoLib.Utiles;
using UnityEngine;

namespace AKH.Network
{
    public class ClientPacketManager : PacketManager
    {
        private PacketHandler _packetHandler;
        public ClientPacketManager(EventChannelSO packetChannel) : base(packetChannel)
        {
            _packetHandler = new(packetChannel);
        }

        public override void Register()
        {
            RegisterHandler<S_Chat>((ushort)PacketID.S_Chat, _packetHandler.S_ChatHandler);
        }
    }
}
