using Server.Events;
using Server.Objects;
using Server.Utiles;
using System;

namespace Server.Rooms.States
{
    class LobbyState : GameRoomState
    {
        public LobbyState(GameRoom room) : base(room)
        {
        }
        public override void Enter()
        {
            base.Enter();
            _room.Bus.AddListener<ClientMoveEvent>(HandleChange);
        }

        private void HandleChange(ClientMoveEvent @event)
        {
            var player = _room.ObjectManager.GetObject<Player>(@event.index);
            player.position = @event.position;
            //_room.Broadcast();
        }
    }
}
