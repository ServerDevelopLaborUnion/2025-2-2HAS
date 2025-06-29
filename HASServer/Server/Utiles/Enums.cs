namespace Server.Utiles
{
    public enum ObjectType
    {
        None,
        BombArea,
        PlantArea,
        Door,
        BreakableWall,
        Player
    }
    public enum RoomState
    {
        Lobby,
        Prepare,
        InGame,
        Between,
        Bomb,
        GameEnd
    }
}
