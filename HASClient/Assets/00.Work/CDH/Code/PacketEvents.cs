using _00.Work.CDH.Code.ChatFolder;
using Assets._00.Work.CDH.Code.ChatFolder;
using Assets._00.Work.CDH.Code.DummyClients;

namespace _00.Work.CDH.Code
{
    public class PacketEvents
    {
        public static readonly ChatRecvEventHandler ChatEvent = new();
        public static readonly MoveEventHandler MoveEvent = new();
        public static readonly RotateEventHandler RotateEventHandler = new();
    }
}