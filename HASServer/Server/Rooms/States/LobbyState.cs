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
            _room.Bus.AddListener<ClientMoveEvent>(HandleMove);
            _room.Bus.AddListener<ClientRotateEvent>(HandleRotate);
        }

        public override void Exit()
        {
            base.Exit();
            _room.Bus.RemoveListener<ClientMoveEvent>(HandleMove);
            _room.Bus.RemoveListener<ClientRotateEvent>(HandleRotate);
        }
        private void HandleRotate(ClientRotateEvent @event)
        {
            var player = _room.ObjectManager.GetObject<Player>(@event.index);
            player.rotation = @event.rotation;
            S_Rotate rotate = new()
            {
                index = player.index,
                rotation = player.rotation.ToPacket()
            };
            _room.Broadcast(rotate);
        }

        private void HandleMove(ClientMoveEvent @event)
        {
            var player = _room.ObjectManager.GetObject<Player>(@event.index);
            player.position = @event.position;
            player.direction = @event.direction;
            player.Speed = @event.speed;
            S_Move move = new()
            {
                direction = player.direction.ToPacket(),
                index = player.index,
                position = player.position.ToPacket(),
                speed = player.Speed
            };
            _room.Broadcast(move);
        }
    }
}
