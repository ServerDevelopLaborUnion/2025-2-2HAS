using Server.Events;
using Server.Utiles;
using System;
using System.Collections.Generic;

namespace Server.Rooms.States
{
    class ReadyState : CanMoveState
    {
        private CountTimeSync _roundTime;
        public ReadyState(GameRoom room) : base(room)
        {
            _roundTime = new CountTimeSync(HandleTimerElapsed, HandleTimer, 100);
        }
        public override void Enter()
        {
            base.Enter();
            _room.Bus.AddListener<ClientChangeModelEvent>(HandleModelChanged);
            int seekerCount = _room.SessionCount / 5;
            List<Role> roles = new(_room.SessionCount) { Role.Hider };
            for(int i = 0; i < seekerCount; i++)
            {
                int randomVal = Random.Shared.Next(_room.SessionCount);
                if (roles[randomVal]==Role.Seeker)
                {
                    i--;
                    continue;
                }
                roles[randomVal] = Role.Seeker;
            }
        }
        public override void Exit()
        {
            base.Exit();
            _room.Bus.RemoveListener<ClientChangeModelEvent>(HandleModelChanged);
        }
        public override void Update()
        {
            base.Update();
            _roundTime.UpdateDeltaTime();
        }
        private void HandleTimerElapsed(double obj)
        {
        }

        private void HandleTimer()
        {
            _room.ChangeState(RoomState.InGame);
        }



        private void HandleModelChanged(ClientChangeModelEvent @event)
        {
            //모델 바꾸는 로직
        }
    }
}
