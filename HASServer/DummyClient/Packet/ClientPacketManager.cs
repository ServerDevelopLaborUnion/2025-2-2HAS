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
		_onRecv.Add((ushort)PacketID.S_RoomEnter, MakePacket<S_RoomEnter>);
		_handler.Add((ushort)PacketID.S_RoomEnter, PacketHandler.S_RoomEnterHandler);
		_onRecv.Add((ushort)PacketID.S_RoomExit, MakePacket<S_RoomExit>);
		_handler.Add((ushort)PacketID.S_RoomExit, PacketHandler.S_RoomExitHandler);
		_onRecv.Add((ushort)PacketID.S_RoomList, MakePacket<S_RoomList>);
		_handler.Add((ushort)PacketID.S_RoomList, PacketHandler.S_RoomListHandler);
		_onRecv.Add((ushort)PacketID.S_PacketResponse, MakePacket<S_PacketResponse>);
		_handler.Add((ushort)PacketID.S_PacketResponse, PacketHandler.S_PacketResponseHandler);
		_onRecv.Add((ushort)PacketID.S_Chat, MakePacket<S_Chat>);
		_handler.Add((ushort)PacketID.S_Chat, PacketHandler.S_ChatHandler);
		_onRecv.Add((ushort)PacketID.S_SyncTimer, MakePacket<S_SyncTimer>);
		_handler.Add((ushort)PacketID.S_SyncTimer, PacketHandler.S_SyncTimerHandler);
		_onRecv.Add((ushort)PacketID.S_UpdateRoomState, MakePacket<S_UpdateRoomState>);
		_handler.Add((ushort)PacketID.S_UpdateRoomState, PacketHandler.S_UpdateRoomStateHandler);

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