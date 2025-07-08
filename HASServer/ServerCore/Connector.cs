using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace ServerCore
{
    /*
	 * 서버에서 쓰는걸 ListenSocket
	 * 클라는 Socket
	 * Connect, Listen -> 식당문을 열고 손님을 기다리는중, Bind, Accept
	 *서버
	 * bind: 내가 어느 손님을 받을건지, 내 건물 위치가 어딘지 입력 >> 어디서든 받을건지 아니면 내 로컬만 받을건지 설정, IPEndPoint 입력
	 *  Listen -> 식당문을 열고 손님을 기다리는중
	 *  Accept -> 식당 문 앞에서 다시한번 거르기 우리가 원하는 받는거
	 *  
	 *  클라작업
	 *  Connect -> 식당에 가는거
	 *  
	 *  서버작업 멀티스레드
	 *  Listen을 처리하는 스레드가 따로있고
	 *  Recv,Send 해주는 스레드 따로 나누고
	 *  Connect 하는 스레드 나눔
	 *  
	 *  Recv를 처리하는 스레드를 하나씩 백그라운드에 실행시켜둠
	 *  Recv는 그냥 켜두기하면 하면 Recv가 왓을떄 콜백만 실행시키면 됨.
	 *  
	 *  Send는 어느 스레드에서 Send를 해줄지 모름 얘는 Queue 방식으로 처리를 하는데
	 *  만약에 3개의 스레드에서 Send를 한방에 실행시킨다.
	 *  Send는 하나의 스레드가 작업을 완료하면 그 다음 스레드가 Send하고 또 그다음 스레드가 Send함
	 *  Send를 3개를 동시에 해버리면 낭비지.
	 *  먼저 들어온 스레드가 Send를 그냥 해버려 그러면 Send를 하는 그 텀이 있을거아녀
	 *  그 텀 사이에 나머지 스레드 2개가 대기 리스트에 보낼 패킷을 적재해놔
	 *  먼저 들어온 스레드가 Queue를 체크하고 그게 비었으면 Send를 처리해.
	 */
    public class Connector
    {
        Func<Session> _sessionFactory;

        public void Connect(IPEndPoint endPoint, Func<Session> sessionFactory, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                // 휴대폰 설정
                Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                _sessionFactory = sessionFactory;

                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.Completed += OnConnectCompleted;
                args.RemoteEndPoint = endPoint;
                args.UserToken = socket;

                RegisterConnect(args);
            }
        }

        void RegisterConnect(SocketAsyncEventArgs args)
        {
            Socket socket = args.UserToken as Socket;
            if (socket == null)
                return;

            bool pending = socket.ConnectAsync(args);
            if (pending == false)
                OnConnectCompleted(null, args);
        }

        void OnConnectCompleted(object sender, SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                Session session = _sessionFactory.Invoke();
                session.Start(args.ConnectSocket);
                session.OnConnected(args.RemoteEndPoint);
            }
            else
            {
                Console.WriteLine($"OnConnectCompleted Fail: {args.SocketError}");
            }
        }
    }
}
