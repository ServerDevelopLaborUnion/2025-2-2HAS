using Server.Rooms;

namespace Server.Components
{
    internal abstract class Component
    {
        protected Component(Room room)
        {

        }
        public abstract void Enable();//여기서 직접 이벤트 구독이 아니라 Strategy 패턴처럼 하면 어떨까
        public abstract void Disable();
    }
}
