
using _00.Work.CDH.Code.Chat;
using ServerCore;
using DewmoLib.Utiles;
using VHierarchy.Libs;

public partial class PacketHandler
{
    public void S_ChatHandler(PacketSession session, IPacket packet)
    {
        S_Chat chat = packet as S_Chat;
        if (chat == null)
            return;
        
        ChatEventHandler chatEventHandler = new()
        {
            pName = chat.pName,
            message = chat.text
        };
        _packetChannel.InvokeEvent(chatEventHandler);
    }
}
