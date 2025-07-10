using DewmoLib.Utiles;

namespace KHG.Events
{
    public class PlayerInfoEvents
    {
        public static readonly PlayerNameEvent PlayerNameEvent = new();
    }
    public class PlayerNameEvent : GameEvent
    {
        public string Name;
    }
}