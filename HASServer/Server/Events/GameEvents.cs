using Server.Utiles;
using System.Numerics;

namespace Server.Events
{
    public class ClientMoveEvent : GameEvent
    {
        public int index;
        public Vector3 position;
        public Vector3 direction;
        public float speed;

        public override void ResetItem()
        {
            index = -1;
            position = default;
            direction = default;
            speed = 0;
        }
    }
    public class ClientChangeModelEvent : GameEvent
    {
        public int index;
        public int modelIndex;

        public override void ResetItem()
        {
            index = -1;
            modelIndex = -1;
        }
    }
    public class ClientRotateEvent : GameEvent
    {
        public int index;
        public Quaternion rotation;

        public override void ResetItem()
        {
            index = -1;
            rotation = default;
        }
    }
    public class GameStartEvent : GameEvent
    {
        public override void ResetItem()
        {
        }
    }
}
