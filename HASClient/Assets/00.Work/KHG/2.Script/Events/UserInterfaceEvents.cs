using DewmoLib.Utiles;
using UnityEngine;

namespace KHG.Events
{
    public class UserInterfaceEvents
    {
        public static readonly WarnUiEvent WarnUiEvent = new();
        public static readonly ServerConnectEvent ServerConnectEvent = new();
    }

    public class WarnUiEvent : GameEvent
    {
        public string Title = "¿À·ù";
        public string Message = "Unexpected Error";
    }
    public class ServerConnectEvent : GameEvent
    {
        public bool result;
    }
}