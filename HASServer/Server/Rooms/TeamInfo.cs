namespace Server.Rooms
{
    internal class TeamInfo
    {
        //처음 시작할때 플레이어ㅓ 수
        public int BaseCount { get; set; }
        //현재 플레이어 수
        public int CurrentCount { get; set; }
        //이긴 수
        public int WinCount { get; set; }
        public void ResetCurrentCount()
        {
            CurrentCount = BaseCount;
        }
        public void Win()
        {
            WinCount++;
        }
    }
}
