﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ServerCore;
using System.Threading;

namespace DummyClient
{
    class ServerSession : PacketSession
    {
        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected : {endPoint}");
            C_SetName c_SetName = new();
            c_SetName.name = "qwd";
            Send(c_SetName.Serialize());
            C_RoomEnter roomEnter = new() { roomId = 1 };
            Send(roomEnter.Serialize());
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnDisconnected : {endPoint}");
        }

        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            PacketManager.Instance.OnRecvPacket(this, buffer);
        }

        public override void OnSend(int numOfBytes)
        {
            Console.WriteLine("SEND");
            //Console.WriteLine($"Transferred bytes: {numOfBytes}");
        }
    }
}
