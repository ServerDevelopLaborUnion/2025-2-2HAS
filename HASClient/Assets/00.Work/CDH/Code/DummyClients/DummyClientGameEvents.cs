using DewmoLib.Utiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._00.Work.CDH.Code.DummyClients
{
    public class DummyClientMoveEventHandler : GameEvent
    {
        public int index;
        public float velocity;
        public VectorPacket position;
        public VectorPacket direction;
    }
    public class DummyClientRotationEventHandler : GameEvent
    {
        public int index;
        public QuaternionPacket rotation;
    }
}
