using Server.Utiles;
using System.Numerics;

namespace Server.Events
{
    public class ClientChangeEvent : GameEvent
    {
        public int index;
        public int modelIndex;
        public Vector3 position;
        public Vector3 direction;
        public Quaternion rotation;
        public float speed;

        public override void ResetItem()
        {
            index = -1;
            modelIndex = -1;
            position = default;
            direction = default;
            rotation = default;
            speed = 0;
        }
        public ClientChangeEvent Init(int index,int modelIndex,Vector3 pos,Vector3 dir,Quaternion rot,float vel)
        {
            this.index = index;
            this.modelIndex = modelIndex;
            position = pos;
            direction = dir;
            rotation = rot;
            speed = vel;
            return this;
        }
    }
}
