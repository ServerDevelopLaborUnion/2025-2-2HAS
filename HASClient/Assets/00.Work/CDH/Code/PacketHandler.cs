using _00.Work.CDH.Code;
using _00.Work.CDH.Code.ChatFolder;
using DewmoLib.Network.Core;

public partial class PacketHandler
{
    public void S_ChatHandler(PacketSession session, IPacket packet)
    {
        S_Chat chat = packet as S_Chat;
        if (chat == null)
            return;

        ChatRecvEventHandler evt = ChatPacketEvents.ChatEvent;
        evt.pName = chat.pName;
        evt.message = chat.text;

        _packetChannel.InvokeEvent(evt);
    }
}
