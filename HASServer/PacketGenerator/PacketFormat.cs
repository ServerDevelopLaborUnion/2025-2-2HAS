using System;
using System.Collections.Generic;
using System.Text;

namespace PacketGenerator
{
    class PacketFormat
    {
        // {0} 패킷 등록
        public static string managerFormat =
@"using ServerCore;
using System;
using System.Collections.Generic;

class PacketManager
{{
	#region Singleton
	static PacketManager _instance = new PacketManager();
	public static PacketManager Instance {{ get {{ return _instance; }} }}
	#endregion

	PacketManager()
	{{
		Register();
	}}

	Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>> _onRecv = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>>>();
	Dictionary<ushort, Action<PacketSession, IPacket>> _handler = new Dictionary<ushort, Action<PacketSession, IPacket>>();
		
	public void Register()
	{{
{0}
	}}

	public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer)
	{{
		ushort packetId = PacketUtility.ReadPacketID(buffer);

		Action<PacketSession, ArraySegment<byte>> action = null;
		if (_onRecv.TryGetValue(packetId, out action))
			action.Invoke(session, buffer);
	}}

	void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer) where T : IPacket, new()
	{{
		T pkt = new T();
		pkt.Deserialize(buffer);
		Action<PacketSession, IPacket> action = null;
		if (_handler.TryGetValue(pkt.Protocol, out action))
			action.Invoke(session, pkt);
	}}
}}";

        // {0} 패킷 이름
        public static string managerRegisterFormat =
@"		_onRecv.Add((ushort)PacketID.{0}, MakePacket<{0}>);
		_handler.Add((ushort)PacketID.{0}, PacketHandler.{0}Handler);";

        // {0} 패킷 이름/번호 목록
        // {1} 패킷 목록
        public static string fileFormat =
@"using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ServerCore;

public enum PacketID
{{
	{0}
}}
{1}
";

        // {0} 패킷 이름
        // {1} 패킷 번호
        public static string packetEnumFormat =
@"{0} = {1},";


        // {0} 패킷 이름
        // {1} 멤버 변수들
        // {2} 멤버 변수 Read
        // {3} 멤버 변수 Write
        public static string packetFormat =
@"
public class {0} : IPacket
{{
	{1}

	public ushort Protocol {{ get {{ return (ushort)PacketID.{0}; }} }}

	public void Deserialize(ArraySegment<byte> segment)
	{{
		ushort count = 0;

		count += sizeof(ushort);
		count += sizeof(ushort);
		{2}
	}}

	public ArraySegment<byte> Serialize()
	{{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		count += PacketUtility.AppendUshortData(this.Protocol, segment, count);
		{3}
		PacketUtility.AppendUshortData(count, segment, 0);
		return SendBufferHelper.Close(count);
	}}
}}
";
        public static string dataPacketFormat =
            @"
public struct {0} : IDataPacket
{{
	{1}

	public ushort Deserialize(ArraySegment<byte> segment, int offset)
	{{
		ushort count = (ushort)offset;
		{2}
		return (ushort)(count - offset);
	}}

	public ushort Serialize(ArraySegment<byte> segment, int offset)
	{{
		ushort count = (ushort)offset;
		{3}
		return (ushort)(count - offset);
	}}
}}
";

        // {0} 변수 형식
        // {1} 변수 이름
        public static string memberFormat =
@"public {0} {1};";


        // {0} 변수 형식
        // {1} 변수 이름
        public static string memberListFormat =
@"public List<{0}> {1};";
        /// <summary>
        ///{0} 변수 형식 <br/>
        ///{1} 변수 이름
		/// </summary>
        public static string readFormat =
@"count += PacketUtility.Read{0}Data(segment, count, out {1});";

        // {0} 변수 이름
        // {1} 변수 형식
        public static string readByteFormat =
@"this.{0} = ({1})segment.Array[segment.Offset + count];
count += sizeof({1});";

        // {0} 변수 이름
        public static string readStringFormat =
@"ushort {0}Len = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
count += sizeof(ushort);
this.{0} = Encoding.Unicode.GetString(s.Slice(count, {0}Len));
count += {0}Len;";

        // {0} 리스트 이름 [대문자]
        // {1} 리스트 이름 [소문자]
        public static string readListFormat =
@"this.{1}s.Clear();
ushort {1}Len = BitConverter.ToUInt16(s.Slice(count, s.Length - count));
count += sizeof(ushort);
for (int i = 0; i < {1}Len; i++)
{{
	{0} {1} = new {0}();
	{1}.Read(s, ref count);
	{1}s.Add({1});
}}";

        /// <summary>
        /// {0}변수 형식<br/>
        /// {1}변수 이름
        /// </summary>
        public static string writeFormat =
@"count += PacketUtility.Append{0}Data(this.{1}, segment, count);";


        // {0} 리스트 이름 [대문자]
        // {1} 리스트 이름 [소문자]
    }
}
