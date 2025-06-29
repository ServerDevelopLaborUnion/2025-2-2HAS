using ServerCore;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DummyClient
{
	

	class Program
	{
		static void Main(string[] args)
		{
			// DNS (Domain Name System)
			IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7777);

			Connector connector = new Connector();

			connector.Connect(endPoint, 
				() => { return SessionManager.Instance.Generate(); },
				8);

			while (true)
			{
			}
		}
	}
}
