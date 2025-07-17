using Server.Objects;
using ServerCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Server.Rooms
{
    internal abstract class Room : IJobQueue
    {
        protected ObjectManager _objectManager = new();
        public ObjectManager ObjectManager => _objectManager;
        protected RoomManager _roomManager;
        public EventBus Bus { get; private set; }

        public Room(RoomManager manager, int roomId, string name)
        {
            _roomManager = manager;
            RoomId = roomId;
            RoomName = name;
        }
        protected Dictionary<int, ClientSession> _sessions = new Dictionary<int, ClientSession>();
        private JobQueue _jobQueue = new JobQueue();
        private ConcurrentQueue<ArraySegment<byte>> _pendingList = new();
        public string RoomName { get; private set; }
        public int HostIndex { get; private set; }
        public int RoomId { get; private set; } = 0;
        public int MaxSessionCount { get; protected set; }
        public int SessionCount => _sessions.Count;
        public Dictionary<int, ClientSession> Sessions => _sessions;

        public void Push(Action job)
        {
            _jobQueue.Push(job);
        }
        public void Flush()
        {
            int n = 0;
            try
            {
                // N ^ 2
                if (_pendingList.Count == 0)
                    return;
                //Console.WriteLine($"SessionCount : {_sessions.Values.Count}");
                foreach (ClientSession s in _sessions.Values)
                {
                    n++;
                    s.Send(_pendingList.ToList());
                }
                //Console.WriteLine("Clear");
                _pendingList.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Complete: {n}");
                Console.WriteLine(ex);
            }
        }

        public void Broadcast(IPacket packet)
        {
            _pendingList.Enqueue(packet.Serialize());
        }
        public ClientSession GetSession(int key)
        {
            return _sessions[key];
        }
        public virtual void Enter(ClientSession session)
        {
            _sessions.Add(session.SessionId, session);
            session.Room = this;
        }
        public virtual void Leave(ClientSession session)
        {
            _sessions.Remove(session.SessionId);
            _objectManager.RemoveObject(session.PlayerId);
            if (SessionCount == 0)
            {
                AllPlayerExit();
            }
            else
            {
                Broadcast(new S_RoomExit() { Index = session.PlayerId });
            }
        }

        public virtual void AllPlayerExit()
        {
            _roomManager.RemoveRoom(RoomId);
        }
        public void ReviveAllPlayer()
        {
            foreach (var session in Sessions)
            {
                _objectManager.GetObject<Player>(session.Value.PlayerId).Revive();
            }
        }
        public abstract void ObjectDead(ObjectBase obj);
        public abstract void UpdateRoom();
        public virtual void SetUpRoom(C_CreateRoom packet, int hostIndex)
        {
            HostIndex = hostIndex;
            RoomName = packet.roomName;
            MaxSessionCount = packet.maxCount;
        }
    }
}
