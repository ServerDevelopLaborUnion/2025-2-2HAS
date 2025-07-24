using Server.Events;
using Server.Objects;
using Server.Utiles;
using System;

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
