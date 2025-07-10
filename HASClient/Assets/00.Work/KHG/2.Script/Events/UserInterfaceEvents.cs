using DewmoLib.Utiles;
using UnityEngine;

namespace KHG.Events
{
    public class UserInterfaceEvents
    {
        public static readonly WarnUiEvent WarnUiEvent = new();
    }

    public class WarnUiEvent : GameEvent
    {
        public string Title = "����";
        public string Message = "Unexpected Error";
    }
}