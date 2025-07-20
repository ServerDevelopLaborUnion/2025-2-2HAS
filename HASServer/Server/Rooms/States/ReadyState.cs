using Server.Events;
using System;

namespace Server.Rooms.States
{
    class ReadyState : GameRoomState
    {
        public ReadyState(GameRoom room) : base(room)
        {
        }
        public override void Enter()
        {
            base.Enter();
            _room.Bus.AddListener<ClientChangeModelEvent>(HandleModelChanged);
        }

        private void HandleModelChanged(ClientChangeModelEvent @event)
        {

        }
    }
}
