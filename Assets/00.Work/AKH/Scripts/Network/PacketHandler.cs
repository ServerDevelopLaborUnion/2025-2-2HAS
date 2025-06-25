using DewmoLib.Utiles;

namespace DewmoLib.Network.Packets
{
    public partial class PacketHandler
    {
        private EventChannelSO _packetChannel;
        public PacketHandler(EventChannelSO packetChannel)
        {
            _packetChannel = packetChannel;
        }

    }
}
