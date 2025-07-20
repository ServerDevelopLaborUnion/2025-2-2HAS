using KHG.Events;
using ServerCore;
using System;

public partial class PacketHandler
{
    public void S_RoomListHandler(PacketSession session, IPacket packet)
    {
        S_RoomList s_RoomList = (S_RoomList)packet;

        RoomListEvent roomListEvent = PacketEvents.RoomListEvent;
        roomListEvent.infoPackets = s_RoomList.roomInfos;
        _packetChannel.InvokeEvent(roomListEvent);
    }

    public void S_RoomEnterHandler(PacketSession session, IPacket packet)// 전체 확인용
    {

    }
}
