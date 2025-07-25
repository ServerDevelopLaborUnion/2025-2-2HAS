using _00.Work.CDH.Code;
using Assets._00.Work.CDH.Code.ChatFolder;
using Assets._00.Work.CDH.Code.DummyClients;
using ServerCore;

public partial class PacketHandler
{
    public void S_ChatHandler(PacketSession session, IPacket packet)
    {
        S_Chat chat = (S_Chat)packet;

        ChatRecvEventHandler chatEventHandler = PacketEvents.ChatEvent;
        chatEventHandler.pName = chat.pName;
        chatEventHandler.message = chat.text;

        _packetChannel.InvokeEvent(chatEventHandler);
    }
}
