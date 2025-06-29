using System;

namespace Server.Rooms.States
{
    abstract class GameRoomState
    {
        protected GameRoom _room;
        public GameRoomState(GameRoom room)
        {
            _room = room;
        }
        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}
