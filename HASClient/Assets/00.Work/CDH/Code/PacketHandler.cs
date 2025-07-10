using _00.Work.CDH.Code;
using Assets._00.Work.CDH.Code.ChatFolder;
using ServerCore;
using System.Diagnostics;
using UnityEngine.iOS;

public partial class PacketHandler
{
    public void S_ChatHandler(PacketSession session, IPacket packet)
    {
        S_Chat chat = (S_Chat)packet;

        ChatRecvEventHandler chatEventHandler = ChatPacketEvents.ChatEvent;
        chatEventHandler.pName = chat.pName;
        chatEventHandler.message = chat.text;

        _packetChannel.InvokeEvent(chatEventHandler);
    }
}
