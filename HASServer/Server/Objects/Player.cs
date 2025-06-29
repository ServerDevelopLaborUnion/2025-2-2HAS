using Server.Rooms;
using Server.Utiles;
using ServerCore;
using System;
using System.Numerics;

namespace Server.Objects
{
    internal class Player : ObjectBase, IHittable
    {
        public Player(Room room) : base(room)
        {
        }
        public string Name;

        public int Health { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsDead => throw new NotImplementedException();

        public override IDataPacket CreatePacket()
        {
            throw new NotImplementedException();
        }

        public void Hit()
        {
        }

        public void Revive()
        {
        }
    }
}
