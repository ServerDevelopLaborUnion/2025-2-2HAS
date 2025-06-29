using DewmoLib.Utiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKH.Scripts.Packet
{
    public static class PacketEvevnts
    {
        public static readonly PacketResponse PacketResponse = new();
    }
    public class PacketResponse : GameEvent
    {
        public PacketID packetId;
        public bool success;
    }
}
