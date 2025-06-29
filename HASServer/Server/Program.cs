using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Server.Rooms;
using Server.Utiles;
using ServerCore;
using Timer = System.Timers.Timer;

namespace Server
{
    class Program
    {
        static Listener _listener = new Listener();
        public static RoomManager roomManager = RoomManager.Instance;
        private static Stopwatch _stopWatch;

        static void Main(string[] args)
        {
            // DNS (Domain Name System)
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 7777);

            _listener.Init(endPoint, () => { return SessionManager.Instance.Generate(); });
            Console.WriteLine("Listening...");
            _stopWatch = new();
            GameLoop();
        }
        private static void GameLoop()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            float lastTime = 0f;

            while (true)
            {
                float currentTime = (float)stopwatch.Elapsed.TotalSeconds;
                Time.deltaTime = currentTime - lastTime;
                Time.time = currentTime;
                lastTime = currentTime;
                roomManager.UpdateRooms();
                roomManager.FlushRooms();

                Thread.Sleep(10); // 프레임 제한
            }
        }
    }
}
