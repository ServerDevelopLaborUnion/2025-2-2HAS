using System;
using System.Net;
using UnityEngine;
using DewmoLib.Network.Packets;
using DewmoLib.Network.Core;

namespace AKH.Network
{
    public class ServerSession : PacketSession
    {
        private PacketQueue _packetQueue;
        public ServerSession(PacketQueue packetQueue)
        {
            _packetQueue = packetQueue;
        }

        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected : {endPoint}");
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnDisconnected : {endPoint}");
        }

        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            Debug.Log("Recv");
            _packetQueue.Push(buffer);
        }

        public override void OnSend(int numOfBytes)
        {
            Console.WriteLine("SEND");
            //Console.WriteLine($"Transferred bytes: {numOfBytes}");
        }
    }
}
