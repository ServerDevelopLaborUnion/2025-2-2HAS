using Server.Utiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Server.Rooms.States
{
    internal class RoomStateMachine
    {
        private Dictionary<RoomState, GameRoomState> _states;
        public GameRoomState CurrentState { get; private set; }
        public RoomState CurrentStateEnum { get; private set; }
        public RoomStateMachine(GameRoom room)
        {
            _states = new Dictionary<RoomState, GameRoomState>();
            Assembly fsmAssembly = Assembly.GetAssembly(typeof(GameRoomState));
            List<Type> types = fsmAssembly.GetTypes()
                .Where(type => !type.IsAbstract && type.IsSubclassOf(typeof(GameRoomState)))
                .ToList();
            types.ForEach(type => _states.Add(
                EnumHelper.StringToEnum<RoomState>(type.Name.Replace("State", ""))
                , Activator.CreateInstance(type, room) as GameRoomState));
        }
        public void ChangeState(RoomState type)
        {
            if (_states.TryGetValue(type, out GameRoomState state))
            {
                CurrentState?.Exit();
                CurrentStateEnum = type;
                CurrentState = state;
                state.Enter();
            }
            else
            {
                Console.WriteLine($"{type}State does not exist");
                throw new System.Exception();
            }
        }
        public void UpdateRoom()
            => CurrentState.Update();
    }
}
