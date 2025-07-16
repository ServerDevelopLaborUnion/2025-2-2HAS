using _00.Work.CDH.Code;
using _00.Work.CDH.Code.ChatFolder;
using Assets._00.Work.CDH.Code.ChatFolder;
using Assets._00.Work.CDH.Code.DummyClients;
using ServerCore;
using System.Diagnostics;
using UnityEngine.iOS;
using UnityEngine.Rendering;

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

    public void S_DummyClientHandler(PacketSession session, IPacket packet)
    {
        S_Move move = (S_Move)packet;

        DummyClientMoveEventHandler dummyClientMoveEventHandler = PacketEvents.dummyClientMoveEvent;
        dummyClientMoveEventHandler.index = move.index;
        dummyClientMoveEventHandler.velocity = move.velocity;
        dummyClientMoveEventHandler.position = move.position;
        dummyClientMoveEventHandler.direction = move.direction;

        _packetChannel.InvokeEvent(dummyClientMoveEventHandler);
    }
}
