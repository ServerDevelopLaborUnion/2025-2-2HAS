using DewmoLib.Network.Packets;
using DewmoLib.Utiles;
using UnityEngine;

namespace AKH.Network
{
    public class ClientPacketManager : PacketManager
    {
        private PacketHandler _packetHandler;
        public ClientPacketManager(EventChannelSO packetChannel)
        {
            _packetHandler = new(packetChannel);
            Register();
        }

        public override void Register()
        {
            RegisterHandler<S_Chat>((ushort)PacketID.S_Chat, _packetHandler.S_ChatHandler);
            RegisterHandler<S_RoomList>((ushort)PacketID.S_RoomList, _packetHandler.S_RoomListHandler);
        }
    }
}
