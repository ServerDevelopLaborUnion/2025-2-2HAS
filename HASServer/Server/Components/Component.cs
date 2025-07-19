using Server.Rooms;

namespace Server.Components
{
    internal abstract class Component
    {
        protected Component(Room room)
        {
            
        }
        public abstract void Enable();
        public abstract void Disable();
    }
}
