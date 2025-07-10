using Server.Utiles;
using System;

namespace Server.Rooms.States
{
    class LobbyState : GameRoomState
    {
        private CountTimeSync nextState;
        public LobbyState(GameRoom room) : base(room)
        {
            nextState = new(HandleElapsed, HandleCompleteCount, 100);
        }

        private void HandleElapsed(double interval)
        {

        }

        private void HandleCompleteCount()
        {
        }
    }
}
