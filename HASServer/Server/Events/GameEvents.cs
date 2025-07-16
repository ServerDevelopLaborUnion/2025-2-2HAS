using Server.Utiles;

namespace Server.Events
{
    public class ClientMoveEvent : GameEvent
    {
        public int index;
        public VectorPacket position;
        public VectorPacket direction;
        public float velocity;
    }
}
