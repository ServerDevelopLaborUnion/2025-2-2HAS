using Server.Rooms;
using Server.Utiles;
using ServerCore;
using System;
using System.Numerics;

namespace Server.Objects
{
    internal class Player : ObjectBase, IHittable
    {
        public Player(ObjectManager manager) : base(manager)
        {
        }
        public string Name;

        public int Health { get; set; }
        public int ModelIndex { get; set; }
        public float Speed { get; set; }
        public Role Role { get; set; }
        public bool IsDead => Health <= 0;
        public Vector3 direction;

        public override IDataPacket CreatePacket()
        {
            return default;
        }

        public void Hit()
        {
        }

        public void Revive()
        {
        }
    }
}
