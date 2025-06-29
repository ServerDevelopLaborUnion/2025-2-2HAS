using DewmoLib.Utiles;

public partial class PacketHandler
{
    private EventChannelSO _packetChannel;
    public PacketHandler(EventChannelSO packetChannel)
    {
        _packetChannel = packetChannel;
    }
}
