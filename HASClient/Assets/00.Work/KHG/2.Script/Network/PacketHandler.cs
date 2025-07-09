using KHG.Events;
using ServerCore;

public partial class PacketHandler
{
    public void S_RoomListHandler(PacketSession session, IPacket packet)
    {
        S_RoomList s_RoomList = packet as S_RoomList;
        if (s_RoomList == null) return;

        RoomListEvent roomListEvent = PacketEvents.RoomListEvent;
        roomListEvent.infoPackets = s_RoomList.roomInfos;
        _packetChannel.InvokeEvent(roomListEvent);
    }
}
