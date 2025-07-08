using Server.Objects;
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

        internal void SetUpRoom(C_CreateRoom packet)
        {
            throw new NotImplementedException();
        }
    }
}
