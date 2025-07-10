using Server.Rooms;
using Server.Utiles;
using ServerCore;
using System;
using System.Diagnostics;
using System.Net;
using System.Timers;
using Timer = System.Timers.Timer;


namespace Server
{
    class Program
    {
        static Listener _listener = new Listener();
        public static RoomManager roomManager = RoomManager.Instance;
        public static Stopwatch timer;
        static void Main(string[] args)
        {
            // DNS (Domain Name System)
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 7777);
            timer = new();
            _listener.Init(endPoint, () => { return SessionManager.Instance.Generate(); });
            Console.WriteLine("Listening...");
            InitFlushTimer();
            //InitUpdateTimer();
            while (true) { }
            //FlushRoom();
        }
        //private static void InitUpdateTimer()
        //{
        //    Timer updateTimer = new Timer(30);
        //    updateTimer.Enabled = true;
        //    updateTimer.AutoReset = true;
        //}
        private static void InitFlushTimer()
        {
            Timer flushTimer = new Timer(15);
            timer.Restart();
            flushTimer.Elapsed += UpdateLoop;
            flushTimer.Enabled = true;
            flushTimer.AutoReset = true;
        }
        static long beforeTick;
        private static void UpdateLoop(object sender, ElapsedEventArgs e)
        {
            float deltaTime = (timer.ElapsedMilliseconds - beforeTick) / 1000f;
            //Console.WriteLine(deltaTime);
            Time.deltaTime = deltaTime;
            beforeTick = timer.ElapsedMilliseconds;
            roomManager.UpdateRooms();
            roomManager.FlushRooms();
        }
    }
}

