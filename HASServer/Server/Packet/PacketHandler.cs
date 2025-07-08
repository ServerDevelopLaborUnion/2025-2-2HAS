using Server;
using Server.Objects;
using Server.Rooms;
using Server.Utiles;
using ServerCore;
using System;

class PacketHandler
{
    private static RoomManager _roomManager = RoomManager.Instance;
    internal static void C_CreateRoomHandler(PacketSession session, IPacket packet)
    {
        var createRoom = packet as C_CreateRoom;
        var clientSession = session as ClientSession;
        int roomId = _roomManager.GenerateRoom(createRoom);
        EnterRoomProcess(roomId, clientSession, (PacketID)createRoom.Protocol);
    }

    internal static void C_RoomEnterHandler(PacketSession session, IPacket packet)
    {
        var enterPacket = packet as C_RoomEnter;
        var clientSession = session as ClientSession;
        if (clientSession.Room != null || string.IsNullOrEmpty(clientSession.Name))
            return;
        EnterRoomProcess(enterPacket.roomId, clientSession, (PacketID)enterPacket.Protocol);
    }

    private static void EnterRoomProcess(int roomId, ClientSession clientSession, PacketID caller)
    {
        var room = _roomManager.GetRoomById(roomId) as GameRoom;
        if (room == default)
            return;
        Console.WriteLine("EnterRoom");
        room.Push(() =>
        {
            if (room.CanAddPlayer)
            {
                Player newPlayer = new Player(room)
                {
                    Health = 100,
                    Name = clientSession.Name,
                };
                clientSession.PlayerId = newPlayer.index;
                room.Enter(clientSession);
                //room.FirstEnterProcess(clientSession);
                LocationInfoPacket location = new() { index = newPlayer.index };
                PlayerNamePacket playerName = new() { index = newPlayer.index, nickName = newPlayer.Name};
                room.Broadcast(new S_RoomEnter() { newPlayer = location, playerName = playerName });
                SendPacketResponse(clientSession, caller, true);
            }
            else
            {
                SendPacketResponse(clientSession, caller, false);
            }
        });
    }

    private static void SendPacketResponse(ClientSession session, PacketID caller, bool v)
    {
        S_PacketResponse response = new S_PacketResponse();
        response.responsePacket = (ushort)caller;
        response.success = v;
        session.Send(response.Serialize());
    }

    internal static void C_RoomExitHandler(PacketSession session, IPacket packet)
    {
        var clientSession = session as ClientSession;
        var room = clientSession.Room;
        room.Push(() => room.Leave(clientSession));
        Console.WriteLine($"Leave Room: {clientSession.SessionId}");
    }

    internal static void C_RoomListHandler(PacketSession session, IPacket packet)
    {
        var clientSession = session as ClientSession;
        var list = _roomManager.GetRoomInfos();
        S_RoomList roomList = new S_RoomList();
        roomList.roomInfos = list;
        clientSession.Send(roomList.Serialize());
    }

    internal static void C_UpdateLocationHandler(PacketSession session, IPacket packet)
    {
        var clientSession = session as ClientSession;
        var playerPacket = packet as C_UpdateLocation;
        Room room = clientSession.Room;
        if (room == null)
            return;
        room.Push(() =>
        {
            Player player = room.GetObject<Player>(clientSession.PlayerId);
            if (player.IsDead)
                return;
            //player.HandlePacket(playerPacket);
        });
    }

    internal static void C_SetNameHandler(PacketSession session, IPacket packet)
    {
        var clientSession = session as ClientSession;
        var setName = packet as C_SetName;
        bool success = !string.IsNullOrEmpty(setName.name) || (setName.name.Length < 6 && setName.name.Length > 2);
        if (success)
            clientSession.Name = setName.name;
        //SendPacketResponse(clientSession, PacketID.C_SetName, success);
    }

    internal static void C_ChatHandler(PacketSession session, IPacket packet)
    {
        ClientSession clientSession = session as ClientSession;
        var cchat = packet as C_Chat;
        S_Chat chat = new();
        chat.text = cchat.text;
        //chat.pName = clientSession.Name;
        chat.pName = "ASD";
        Console.WriteLine(chat.text);
        SessionManager.Instance.Broadcast(chat);
    }
}