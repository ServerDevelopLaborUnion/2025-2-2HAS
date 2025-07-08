using Server.Rooms;
using Server.Utiles;
using ServerCore;
using System.Numerics;

namespace Server.Objects
{
    internal abstract class ObjectBase
    {
        protected Room _myRoom;
        public ObjectBase(Room room)
        {
            _myRoom = room;
            room.AddObject(this);
        }
        public ObjectType ObjectType { get; protected set; } = ObjectType.None;
        public int index { get; set; }

        public Vector3 position;
        public Quaternion rotation;

        public abstract IDataPacket CreatePacket();
    }
}
