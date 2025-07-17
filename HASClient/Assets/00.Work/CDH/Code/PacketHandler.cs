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

    public void S_MoveHandler(PacketSession session, IPacket packet)
    {
        S_Move move = (S_Move)packet;

        DummyClientMoveEventHandler dummyClientMoveEventHandler = PacketEvents.dummyClientMoveEvent;
        dummyClientMoveEventHandler.index = move.index;
        dummyClientMoveEventHandler.velocity = move.velocity;
        dummyClientMoveEventHandler.position = move.position;
        dummyClientMoveEventHandler.direction = move.direction;

        _packetChannel.InvokeEvent(dummyClientMoveEventHandler);
    }
    public void S_RotateHandler(PacketSession session, IPacket packet)
    {
        S_Rotate move = (S_Rotate)packet;

        DummyClientRotationEventHandler dummyClientRotationEventHandler = PacketEvents.dummyClientRotationEvent;
        dummyClientRotationEventHandler.index = move.index;
        dummyClientRotationEventHandler.rotation = move.rotation;

        _packetChannel.InvokeEvent(dummyClientRotationEventHandler);
    }
}
