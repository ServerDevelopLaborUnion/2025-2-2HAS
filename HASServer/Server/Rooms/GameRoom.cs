using Server.Objects;
using Server.Utiles;
using System;

namespace Server.Rooms
{
    internal class GameRoom : Room
    {
        public bool CanAddPlayer = true;
        public GameRoom(RoomManager manager, int roomId, string name) : base(manager, roomId, name)
        {
        }

        public override void ObjectDead(ObjectBase obj)
        {
        }

        public override void UpdateRoom()
        {
        }

        public void FirstEnter(ClientSession clientSession)
        {
            S_RoomEnterFirst first = new();
            clientSession.Send(first.Serialize());
        }
    }
}
