using Server.Objects;
using Server.Rooms.States;
using Server.Utiles;
using System;
using System.Threading;

namespace Server.Rooms
{
    internal class GameRoom : Room
    {
        public bool CanAddPlayer = true;
        private RoomStateMachine _stateMachine;
        public GameRoom(RoomManager manager, int roomId, string name) : base(manager, roomId, name)
        {
            Console.WriteLine($"ID:{Thread.CurrentThread.ManagedThreadId}");
            _stateMachine = new(this);
            ChangeState(RoomState.Lobby);
        }

        public override void ObjectDead(ObjectBase obj)
        {
        }

        public override void UpdateRoom()
        {
            _stateMachine.UpdateRoom();
        }

        public void FirstEnter(ClientSession clientSession)
        {
            S_RoomEnterFirst first = new();
            clientSession.Send(first.Serialize());
        }
        public void ChangeState(RoomState newState) => _stateMachine.ChangeState(newState);
    }
}
