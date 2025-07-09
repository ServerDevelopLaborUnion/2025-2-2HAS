using DewmoLib.Network.Core;

public partial class PacketHandler
{
    public void S_NameHandler(PacketSession session, IPacket packet)
    {
        C_SetName name = packet as C_SetName;
        if (name.name.Length < 2)
            return;

        PlayerNameEvent evt = PlayerInfoEvents.PlayerNameEvent;
        evt.Name = name.name;
        _packetChannel.InvokeEvent(evt);
    }
}
