using DummyClient;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;

class PacketHandler
{
    internal static void S_EnterRoomFirstHandler(PacketSession session, IPacket packet)
    {
        var players = packet as S_EnterRoomFirst;
        Console.WriteLine($"myIndex : {players.myIndex}");
    }

    internal static void S_PacketResponseHandler(PacketSession session, IPacket packet)
    {
    }

    internal static void S_RoomEnterHandler(PacketSession session, IPacket packet)
    {
        var roomEnter = packet as S_RoomEnter;
        //Console.WriteLine($"newPlayer: {roomEnter.newPlayer.index}");
    }

    internal static void S_RoomExitHandler(PacketSession session, IPacket packet)
    {
    }

    internal static void S_RoomListHandler(PacketSession session, IPacket packet)
    {
        var listPacket = packet as S_RoomList;
        foreach(var item in listPacket.roomInfos)
        {
            Console.WriteLine($"{item.roomId}: {item.currentCount} / {item.maxCount}");
        }
    }

    internal static void S_SyncTimerHandler(PacketSession session, IPacket packet)
    {
    }

    internal static void S_TeamInfosHandler(PacketSession session, IPacket packet)
    {
        Console.WriteLine("ASD");
    }

    internal static void S_TestTextHandler(PacketSession session, IPacket packet)
    {
        var test = packet as S_TestText;
    }

    internal static void S_UpdateInfosHandler(PacketSession session, IPacket packet)
    {
        var p = packet as S_UpdateInfos;
        foreach(var item in p.playerInfos)
        {
        }
    }

    internal static void S_UpdateLocationsHandler(PacketSession session, IPacket packet)
    {
    }

    internal static void S_UpdateRoomStateHandler(PacketSession session, IPacket packet)
    {
    }

    internal static void S_UpdateSnapshotHandler(PacketSession session, IPacket packet)
    {
    }
}
