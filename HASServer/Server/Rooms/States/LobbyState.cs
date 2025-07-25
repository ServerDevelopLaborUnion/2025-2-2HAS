using Server.Events;
using Server.Objects;
using Server.Utiles;
using System;
using System.Numerics;

namespace Server.Rooms.States
{
    class LobbyState : CanMoveState
    {
        public LobbyState(GameRoom room) : base(room)
        {
        }
        public override void Enter()
        {
            base.Enter();
            _room.Bus.AddListener<GameStartEvent>(HandleGameStartReq);
            var players = _room.ObjectManager.GetObjects<Player>();
            S_ResetGame reset = new();
            reset.playerinits = new();
            foreach(var player in players)
            {
                player.Role = Role.None;
                player.Health = 100;
                player.position = Vector3.Zero;
                reset.playerinits.Add((PlayerInitPacket)player.CreatePacket());
            }
            _room.Broadcast(reset);
        }
        public override void Exit()
        {
            _room.Bus.RemoveListener<GameStartEvent>(HandleGameStartReq);
            base.Exit();
        }

        private void HandleGameStartReq(GameStartEvent @event)
        {
            _room.ChangeState(RoomState.Ready);
        }
    }
}
