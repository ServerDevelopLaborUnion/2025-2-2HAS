using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ServerCore;

public enum PacketID
{
	C_RoomEnter = 1,
	C_GameStart = 2,
	C_SetName = 3,
	S_RoomEnter = 4,
	S_RoomEnterFirst = 5,
	C_RoomExit = 6,
	S_RoomExit = 7,
	C_CreateRoom = 8,
	S_RoomList = 9,
	S_PacketResponse = 10,
	C_RoomList = 11,
	S_Chat = 12,
	C_Chat = 13,
	C_Move = 14,
	C_Rotate = 15,
	C_ChangeModel = 16,
	S_Rotate = 17,
	S_Move = 18,
	S_SyncTimer = 19,
	S_UpdateRoomState = 20,
	
}

public struct VectorPacket : IDataPacket
{
	public float x;
	public float y;
	public float z;

	public ushort Deserialize(ArraySegment<byte> segment, int offset)
	{
		ushort count = (ushort)offset;
		count += PacketUtility.ReadFloatData(segment, count, out x);
		count += PacketUtility.ReadFloatData(segment, count, out y);
		count += PacketUtility.ReadFloatData(segment, count, out z);
		return (ushort)(count - offset);
	}

	public ushort Serialize(ArraySegment<byte> segment, int offset)
	{
		ushort count = (ushort)offset;
		count += PacketUtility.AppendFloatData(this.x, segment, count);
		count += PacketUtility.AppendFloatData(this.y, segment, count);
		count += PacketUtility.AppendFloatData(this.z, segment, count);
		return (ushort)(count - offset);
	}
}

public struct QuaternionPacket : IDataPacket
{
	public float x;
	public float y;
	public float z;
	public float w;

	public ushort Deserialize(ArraySegment<byte> segment, int offset)
	{
		ushort count = (ushort)offset;
		count += PacketUtility.ReadFloatData(segment, count, out x);
		count += PacketUtility.ReadFloatData(segment, count, out y);
		count += PacketUtility.ReadFloatData(segment, count, out z);
		count += PacketUtility.ReadFloatData(segment, count, out w);
		return (ushort)(count - offset);
	}

	public ushort Serialize(ArraySegment<byte> segment, int offset)
	{
		ushort count = (ushort)offset;
		count += PacketUtility.AppendFloatData(this.x, segment, count);
		count += PacketUtility.AppendFloatData(this.y, segment, count);
		count += PacketUtility.AppendFloatData(this.z, segment, count);
		count += PacketUtility.AppendFloatData(this.w, segment, count);
		return (ushort)(count - offset);
	}
}

public struct RoomInfoPacket : IDataPacket
{
	public int roomId;
	public int maxCount;
	public int currentCount;
	public string roomName;
	public string hostName;

	public ushort Deserialize(ArraySegment<byte> segment, int offset)
	{
		ushort count = (ushort)offset;
		count += PacketUtility.ReadIntData(segment, count, out roomId);
		count += PacketUtility.ReadIntData(segment, count, out maxCount);
		count += PacketUtility.ReadIntData(segment, count, out currentCount);
		count += PacketUtility.ReadStringData(segment, count, out roomName);
		count += PacketUtility.ReadStringData(segment, count, out hostName);
		return (ushort)(count - offset);
	}

	public ushort Serialize(ArraySegment<byte> segment, int offset)
	{
		ushort count = (ushort)offset;
		count += PacketUtility.AppendIntData(this.roomId, segment, count);
		count += PacketUtility.AppendIntData(this.maxCount, segment, count);
		count += PacketUtility.AppendIntData(this.currentCount, segment, count);
		count += PacketUtility.AppendStringData(this.roomName, segment, count);
		count += PacketUtility.AppendStringData(this.hostName, segment, count);
		return (ushort)(count - offset);
	}
}

public struct PlayerNamePacket : IDataPacket
{
	public string nickName;
	public int index;

	public ushort Deserialize(ArraySegment<byte> segment, int offset)
	{
		ushort count = (ushort)offset;
		count += PacketUtility.ReadStringData(segment, count, out nickName);
		count += PacketUtility.ReadIntData(segment, count, out index);
		return (ushort)(count - offset);
	}

	public ushort Serialize(ArraySegment<byte> segment, int offset)
	{
		ushort count = (ushort)offset;
		count += PacketUtility.AppendStringData(this.nickName, segment, count);
		count += PacketUtility.AppendIntData(this.index, segment, count);
		return (ushort)(count - offset);
	}
}

public struct PlayerInitPacket : IDataPacket
{
	public int index;
	public int modelIndex;
	public string name;
	public VectorPacket position;
	public QuaternionPacket rotation;

	public ushort Deserialize(ArraySegment<byte> segment, int offset)
	{
		ushort count = (ushort)offset;
		count += PacketUtility.ReadIntData(segment, count, out index);
		count += PacketUtility.ReadIntData(segment, count, out modelIndex);
		count += PacketUtility.ReadStringData(segment, count, out name);
		count += PacketUtility.ReadDataPacketData(segment, count, out position);
		count += PacketUtility.ReadDataPacketData(segment, count, out rotation);
		return (ushort)(count - offset);
	}

	public ushort Serialize(ArraySegment<byte> segment, int offset)
	{
		ushort count = (ushort)offset;
		count += PacketUtility.AppendIntData(this.index, segment, count);
		count += PacketUtility.AppendIntData(this.modelIndex, segment, count);
		count += PacketUtility.AppendStringData(this.name, segment, count);
		count += PacketUtility.AppendDataPacketData(this.position, segment, count);
		count += PacketUtility.AppendDataPacketData(this.rotation, segment, count);
		return (ushort)(count - offset);
	}
}

public struct C_RoomEnter : IPacket
{
	public int roomId;

	public ushort Protocol { get { return (ushort)PacketID.C_RoomEnter; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		count += PacketUtility.ReadIntData(segment, count, out roomId);
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		count += PacketUtility.AppendIntData(this.roomId, segment, count);
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

public struct C_GameStart : IPacket
{
	

	public ushort Protocol { get { return (ushort)PacketID.C_GameStart; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

public struct C_SetName : IPacket
{
	public string name;

	public ushort Protocol { get { return (ushort)PacketID.C_SetName; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		count += PacketUtility.ReadStringData(segment, count, out name);
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		count += PacketUtility.AppendStringData(this.name, segment, count);
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

public struct S_RoomEnter : IPacket
{
	public PlayerInitPacket newPlayer;

	public ushort Protocol { get { return (ushort)PacketID.S_RoomEnter; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		count += PacketUtility.ReadDataPacketData(segment, count, out newPlayer);
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		count += PacketUtility.AppendDataPacketData(this.newPlayer, segment, count);
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

public struct S_RoomEnterFirst : IPacket
{
	public int myIndex;
	public List<PlayerInitPacket> inits;

	public ushort Protocol { get { return (ushort)PacketID.S_RoomEnterFirst; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		count += PacketUtility.ReadIntData(segment, count, out myIndex);
		count += PacketUtility.ReadListData(segment, count, out inits);
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		count += PacketUtility.AppendIntData(this.myIndex, segment, count);
		count += PacketUtility.AppendListData(this.inits, segment, count);
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

public struct C_RoomExit : IPacket
{
	

	public ushort Protocol { get { return (ushort)PacketID.C_RoomExit; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

public struct S_RoomExit : IPacket
{
	public int Index;

	public ushort Protocol { get { return (ushort)PacketID.S_RoomExit; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		count += PacketUtility.ReadIntData(segment, count, out Index);
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		count += PacketUtility.AppendIntData(this.Index, segment, count);
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

public struct C_CreateRoom : IPacket
{
	public string roomName;
	public int maxCount;

	public ushort Protocol { get { return (ushort)PacketID.C_CreateRoom; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		count += PacketUtility.ReadStringData(segment, count, out roomName);
		count += PacketUtility.ReadIntData(segment, count, out maxCount);
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		count += PacketUtility.AppendStringData(this.roomName, segment, count);
		count += PacketUtility.AppendIntData(this.maxCount, segment, count);
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

public struct S_RoomList : IPacket
{
	public List<RoomInfoPacket> roomInfos;

	public ushort Protocol { get { return (ushort)PacketID.S_RoomList; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		count += PacketUtility.ReadListData(segment, count, out roomInfos);
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		count += PacketUtility.AppendListData(this.roomInfos, segment, count);
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

public struct S_PacketResponse : IPacket
{
	public ushort responsePacket;
	public bool success;

	public ushort Protocol { get { return (ushort)PacketID.S_PacketResponse; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		count += PacketUtility.ReadUshortData(segment, count, out responsePacket);
		count += PacketUtility.ReadBoolData(segment, count, out success);
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		count += PacketUtility.AppendUshortData(this.responsePacket, segment, count);
		count += PacketUtility.AppendBoolData(this.success, segment, count);
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

public struct C_RoomList : IPacket
{
	

	public ushort Protocol { get { return (ushort)PacketID.C_RoomList; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

public struct S_Chat : IPacket
{
	public string pName;
	public string text;

	public ushort Protocol { get { return (ushort)PacketID.S_Chat; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		count += PacketUtility.ReadStringData(segment, count, out pName);
		count += PacketUtility.ReadStringData(segment, count, out text);
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		count += PacketUtility.AppendStringData(this.pName, segment, count);
		count += PacketUtility.AppendStringData(this.text, segment, count);
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

public struct C_Chat : IPacket
{
	public string text;

	public ushort Protocol { get { return (ushort)PacketID.C_Chat; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		count += PacketUtility.ReadStringData(segment, count, out text);
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		count += PacketUtility.AppendStringData(this.text, segment, count);
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

public struct C_Move : IPacket
{
	public VectorPacket position;
	public VectorPacket direction;
	public float speed;

	public ushort Protocol { get { return (ushort)PacketID.C_Move; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		count += PacketUtility.ReadDataPacketData(segment, count, out position);
		count += PacketUtility.ReadDataPacketData(segment, count, out direction);
		count += PacketUtility.ReadFloatData(segment, count, out speed);
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		count += PacketUtility.AppendDataPacketData(this.position, segment, count);
		count += PacketUtility.AppendDataPacketData(this.direction, segment, count);
		count += PacketUtility.AppendFloatData(this.speed, segment, count);
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

public struct C_Rotate : IPacket
{
	public QuaternionPacket rotation;

	public ushort Protocol { get { return (ushort)PacketID.C_Rotate; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		count += PacketUtility.ReadDataPacketData(segment, count, out rotation);
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		count += PacketUtility.AppendDataPacketData(this.rotation, segment, count);
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

public struct C_ChangeModel : IPacket
{
	public int modelIndex;

	public ushort Protocol { get { return (ushort)PacketID.C_ChangeModel; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		count += PacketUtility.ReadIntData(segment, count, out modelIndex);
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		count += PacketUtility.AppendIntData(this.modelIndex, segment, count);
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

public struct S_Rotate : IPacket
{
	public int index;
	public QuaternionPacket rotation;

	public ushort Protocol { get { return (ushort)PacketID.S_Rotate; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		count += PacketUtility.ReadIntData(segment, count, out index);
		count += PacketUtility.ReadDataPacketData(segment, count, out rotation);
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		count += PacketUtility.AppendIntData(this.index, segment, count);
		count += PacketUtility.AppendDataPacketData(this.rotation, segment, count);
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

public struct S_Move : IPacket
{
	public int index;
	public float speed;
	public VectorPacket position;
	public VectorPacket direction;

	public ushort Protocol { get { return (ushort)PacketID.S_Move; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		count += PacketUtility.ReadIntData(segment, count, out index);
		count += PacketUtility.ReadFloatData(segment, count, out speed);
		count += PacketUtility.ReadDataPacketData(segment, count, out position);
		count += PacketUtility.ReadDataPacketData(segment, count, out direction);
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		count += PacketUtility.AppendIntData(this.index, segment, count);
		count += PacketUtility.AppendFloatData(this.speed, segment, count);
		count += PacketUtility.AppendDataPacketData(this.position, segment, count);
		count += PacketUtility.AppendDataPacketData(this.direction, segment, count);
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

public struct S_SyncTimer : IPacket
{
	public float time;

	public ushort Protocol { get { return (ushort)PacketID.S_SyncTimer; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		count += PacketUtility.ReadFloatData(segment, count, out time);
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		count += PacketUtility.AppendFloatData(this.time, segment, count);
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

public struct S_UpdateRoomState : IPacket
{
	public ushort state;

	public ushort Protocol { get { return (ushort)PacketID.S_UpdateRoomState; } }

	public void Deserialize(ArraySegment<byte> segment)
	{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		count += PacketUtility.ReadUshortData(segment, count, out state);
	}

	public ArraySegment<byte> Serialize()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		count += PacketUtility.AppendUshortData(this.state, segment, count);
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}
}

