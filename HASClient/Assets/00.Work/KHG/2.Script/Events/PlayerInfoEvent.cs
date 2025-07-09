using DewmoLib.Utiles;

public class PlayerInfoEvents
{
    public static readonly PlayerNameEvent PlayerNameEvent = new();
}
public class PlayerNameEvent : GameEvent
{
    public string Name;
}