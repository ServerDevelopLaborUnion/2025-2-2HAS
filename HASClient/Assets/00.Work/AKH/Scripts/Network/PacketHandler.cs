using AKH.Scripts.Packet;
using DewmoLib.Utiles;
using ServerCore;
using System;

public partial class PacketHandler
{
    private EventChannelSO _packetChannel;
    public PacketHandler(EventChannelSO packetChannel)
    {
        _packetChannel = packetChannel;
    }

    internal void S_PacketResponseHandler(PacketSession session, IPacket packet)
    {
        var response = (S_PacketResponse)packet;
        var evt = PacketEvents.PacketResponse;
        evt.success = response.success;
        evt.packetId = (PacketID)response.responsePacket;
        _packetChannel.InvokeEvent(evt);
    }
}
