using ServerCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Server.Rooms
{
    class RoomManager : Singleton<RoomManager>
    {

        private ConcurrentDictionary<int, Room> _rooms = new();
        private int _roomIdGenerator = 0;
        public void UpdateRooms()
        {
            foreach (var room in _rooms.Values)
                room.Push(() => room.UpdateRoom());
        }
        public void FlushRooms()
        {
            foreach (var room in _rooms.Values)
                room.Push(() => room.Flush());
        }
        public void RemoveRoom(int roomId)
        {
            _rooms.TryRemove(roomId, out Room room);
        }
        public Room GetRoomById(int roomId)
        {
            return _rooms.GetValueOrDefault(roomId);
        }
        public int GenerateRoom(C_CreateRoom packet,string hostName)
        {
            int id = Interlocked.Increment(ref _roomIdGenerator);
            Console.WriteLine($"Generate Room: {id}");
            GameRoom room = new(Instance, id, packet.roomName);
            room.Push(() => room.SetUpRoom(packet,hostName));
            _rooms.TryAdd(id, room);
            return id;
        }
        public List<RoomInfoPacket> GetRoomInfos()
        {
            List<RoomInfoPacket> list = new List<RoomInfoPacket>();
            foreach (var room in _rooms)
            {
                list.Add(new RoomInfoPacket()
                {
                    roomName = room.Value.RoomName,
                    roomId = room.Key,
                    maxCount = room.Value.MaxSessionCount,
                    currentCount = room.Value.SessionCount,
                    hostName = room.Value.HostName
                });
            }
            return list;
        }
    }
}
