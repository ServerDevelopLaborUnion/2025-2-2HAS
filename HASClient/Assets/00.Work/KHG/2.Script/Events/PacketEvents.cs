using DewmoLib.Utiles;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace KHG.Events
{
    public class PacketEvents
    {
        public static readonly RoomListEvent RoomListEvent = new();
    }

    public class RoomListEvent : GameEvent
    {
        public List<RoomInfoPacket> infoPackets;
    }

    public class RoomEnterEvent : GameEvent
    {

    }
}
