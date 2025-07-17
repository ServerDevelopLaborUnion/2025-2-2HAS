using System.Collections.Generic;
using System.Diagnostics;
using System;
using Server.Pool;

namespace Server.Utiles
{
    public abstract class GameEvent : IPoolable
    {
        public abstract void ResetItem();
    }
    public class EventBus
    {
        private Dictionary<Type, Action<GameEvent>> _events = new Dictionary<Type, Action<GameEvent>>();
        private Dictionary<Delegate, Action<GameEvent>> _lookUpTable = new Dictionary<Delegate, Action<GameEvent>>();

        public void AddListener<T>(Action<T> handler) where T : GameEvent
        {
            if (_lookUpTable.ContainsKey(handler) == true)
            {
                Console.WriteLine($"{typeof(T)} is already registered");
                return;
            }

            Action<GameEvent> castHandler = evt => handler.Invoke(evt as T);
            _lookUpTable[handler] = castHandler;

            Type evtType = typeof(T);
            if (_events.ContainsKey(evtType))
                _events[evtType] += castHandler;
            else
                _events[evtType] = castHandler;
        }

        public void RemoveListener<T>(Action<T> handler) where T : GameEvent
        {
            Type evtType = typeof(T);
            if (_lookUpTable.TryGetValue(handler, out Action<GameEvent> castHandler))
            {
                if (_events.TryGetValue(evtType, out Action<GameEvent> internalHandler))
                {
                    internalHandler -= castHandler;
                    if (internalHandler == null)
                        _events.Remove(evtType);
                    else
                        _events[evtType] = internalHandler;
                }
                _lookUpTable.Remove(handler); //여기가 castHandler로 적혀있어서 리스너가 빠지질 않았어.
            }
        }

        public void InvokeEvent(GameEvent evt)
        {
            if (_events.TryGetValue(evt.GetType(), out Action<GameEvent> castHandler))
            {
                castHandler?.Invoke(evt);
            }
        }

        public void Clear()
        {
            _events.Clear();
            _lookUpTable.Clear();
        }
    }
}
