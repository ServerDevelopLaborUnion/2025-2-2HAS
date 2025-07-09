using Assets._00.Work.CDH.Code.ChatFolder;
using ServerCore;

public partial class PacketHandler
{
    public void S_ChatHandler(PacketSession session, IPacket packet)
    {
        S_Chat chat = packet as S_Chat;
        if (chat == null)
            return;

        ChatRecvEventHandler chatEventHandler = new()
        {
            pName = chat.pName,
            message = chat.text
        };
        _packetChannel.InvokeEvent(chatEventHandler);
    }
}
