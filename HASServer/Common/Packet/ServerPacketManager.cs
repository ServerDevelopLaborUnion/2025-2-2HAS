using ServerCore;
using System;
using System.Collections.Generic;

class PacketManager
{
	#region Singleton
	static PacketManager _instance = new PacketManager();
	public static PacketManager Instance { get { return _instance; } }
	#endregion

	PacketManager()
	{
		Register();
	}

	Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>> _onRecv = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>>();
	Dictionary<ushort, Action<PacketSession, IPacket>> _handler = new Dictionary<ushort, Action<PacketSession, IPacket>>();
		
	public void Register()
	{
		_onRecv.Add((ushort)PacketID.C_RoomEnter, MakePacket<C_RoomEnter>);
		_handler.Add((ushort)PacketID.C_RoomEnter, PacketHandler.C_RoomEnterHandler);
		_onRecv.Add((ushort)PacketID.C_GameStart, MakePacket<C_GameStart>);
		_handler.Add((ushort)PacketID.C_GameStart, PacketHandler.C_GameStartHandler);
		_onRecv.Add((ushort)PacketID.C_SetName, MakePacket<C_SetName>);
		_handler.Add((ushort)PacketID.C_SetName, PacketHandler.C_SetNameHandler);
		_onRecv.Add((ushort)PacketID.C_RoomExit, MakePacket<C_RoomExit>);
		_handler.Add((ushort)PacketID.C_RoomExit, PacketHandler.C_RoomExitHandler);
		_onRecv.Add((ushort)PacketID.C_CreateRoom, MakePacket<C_CreateRoom>);
		_handler.Add((ushort)PacketID.C_CreateRoom, PacketHandler.C_CreateRoomHandler);
		_onRecv.Add((ushort)PacketID.C_RoomList, MakePacket<C_RoomList>);
		_handler.Add((ushort)PacketID.C_RoomList, PacketHandler.C_RoomListHandler);
		_onRecv.Add((ushort)PacketID.C_Chat, MakePacket<C_Chat>);
		_handler.Add((ushort)PacketID.C_Chat, PacketHandler.C_ChatHandler);
		_onRecv.Add((ushort)PacketID.C_Move, MakePacket<C_Move>);
		_handler.Add((ushort)PacketID.C_Move, PacketHandler.C_MoveHandler);
		_onRecv.Add((ushort)PacketID.C_Rotate, MakePacket<C_Rotate>);
		_handler.Add((ushort)PacketID.C_Rotate, PacketHandler.C_RotateHandler);
		_onRecv.Add((ushort)PacketID.C_ChangeModel, MakePacket<C_ChangeModel>);
		_handler.Add((ushort)PacketID.C_ChangeModel, PacketHandler.C_ChangeModelHandler);

	}

	public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer)
	{
		ushort packetId = PacketUtility.ReadPacketID(buffer);

		Action<PacketSession, ArraySegment<byte>> action = null;
		if (_onRecv.TryGetValue(packetId, out action))
			action.Invoke(session, buffer);
	}

	void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer) where T : IPacket, new()
	{
		T pkt = new T();
		pkt.Deserialize(buffer);
		Action<PacketSession, IPacket> action = null;
		if (_handler.TryGetValue(pkt.Protocol, out action))
			action.Invoke(session, pkt);
	}
}