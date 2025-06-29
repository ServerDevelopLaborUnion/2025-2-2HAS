using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore
{
    public class PacketUtility
    {
        public static IPacket CreatePacket<T>(ArraySegment<byte> buffer) where T : IPacket, new()
        {
            T val = new T();
            val.Deserialize(buffer);
            return val;
        }

        public static ushort ReadPacketID(ArraySegment<byte> buffer)
        {
            return BitConverter.ToUInt16(buffer.Array, buffer.Offset + 2);
        }

        public static ushort ReadListData<T>(ArraySegment<byte> buffer, int offset, out List<T> result) where T : IDataPacket, new()
        {
            ushort num = 0;
            ushort num2 = BitConverter.ToUInt16(buffer.Array, buffer.Offset + offset);
            num += 2;
            result = new List<T>();
            for (int i = 0; i < num2; i++)
            {
                num += ReadDataPacketData<T>(buffer, offset + num, out var result2);
                result.Add(result2);
            }

            return num;
        }

        public static ushort ReadDataPacketData<T>(ArraySegment<byte> buffer, int offset, out T result) where T : IDataPacket, new()
        {
            ushort num = 0;
            result = new T();
            return (ushort)(num + result.Deserialize(buffer, offset));
        }

        public static ushort ReadIntData(ArraySegment<byte> buffer, int offset, out int result)
        {
            result = BitConverter.ToInt32(buffer.Array, buffer.Offset + offset);
            return 4;
        }
        public static ushort ReadLongData(ArraySegment<byte> buffer, int offset, out long result)
        {
            result = BitConverter.ToInt64(buffer.Array, buffer.Offset + offset);
            return 8;
        }
        public static ushort ReadDoubleData(ArraySegment<byte> buffer, int offset, out double result)
        {
            result = BitConverter.ToDouble(buffer.Array, buffer.Offset + offset);
            return 8;
        }
        public static ushort ReadUshortData(ArraySegment<byte> buffer, int offset, out ushort result)
        {
            result = BitConverter.ToUInt16(buffer.Array, buffer.Offset + offset);
            return 2;
        }

        public static ushort ReadShortData(ArraySegment<byte> buffer, int offset, out short result)
        {
            result = BitConverter.ToInt16(buffer.Array, buffer.Offset + offset);
            return 2;
        }

        public static ushort ReadFloatData(ArraySegment<byte> buffer, int offset, out float result)
        {
            result = BitConverter.ToSingle(buffer.Array, buffer.Offset + offset);
            return 4;
        }
        public static ushort ReadBoolData(ArraySegment<byte> buffer, int offset, out bool result)
        {
            result = BitConverter.ToBoolean(buffer.Array, buffer.Offset + offset);
            return 1;
        }

        public static ushort ReadStringData(ArraySegment<byte> buffer, int offset, out string result)
        {
            ushort length = BitConverter.ToUInt16(buffer.Array, buffer.Offset + offset);
            result = Encoding.Unicode.GetString(buffer.Array, buffer.Offset + offset + 2, length);
            return (ushort)(2 + length);
        }
        public static ushort AppendListData<T>(List<T> data, ArraySegment<byte> buffer, int offset) where T : IDataPacket, new()
        {
            ushort num = 0;
            num += AppendUshortData((ushort)data.Count, buffer, offset);
            for (int i = 0; i < data.Count; i++)
            {
                num += AppendDataPacketData(data[i], buffer, offset + num);
            }

            return num;
        }

        public static ushort AppendDataPacketData<T>(T data, ArraySegment<byte> buffer, int offset) where T : IDataPacket, new()
        {
            ushort num = 0;
            return (ushort)(num + data.Serialize(buffer, offset));
        }

        public static ushort AppendIntData(int data, ArraySegment<byte> buffer, int offset)
        {
            ushort num = 4;
            Buffer.BlockCopy(BitConverter.GetBytes(data), 0, buffer.Array, buffer.Offset + offset, num);
            return num;
        }

        public static ushort AppendUshortData(ushort data, ArraySegment<byte> buffer, int offset)
        {
            ushort num = 2;
            Buffer.BlockCopy(BitConverter.GetBytes(data), 0, buffer.Array, buffer.Offset + offset, num);
            return num;
        }
        public static ushort AppendBoolData(bool data, ArraySegment<byte> buffer, int offset)
        {
            ushort num = 1;
            Buffer.BlockCopy(BitConverter.GetBytes(data), 0, buffer.Array, buffer.Offset + offset, num);
            return num;
        }

        public static ushort AppendShortData(short data, ArraySegment<byte> buffer, int offset)
        {
            ushort num = 2;
            Buffer.BlockCopy(BitConverter.GetBytes(data), 0, buffer.Array, buffer.Offset + offset, num);
            return num;
        }

        public static ushort AppendFloatData(float data, ArraySegment<byte> buffer, int offset)
        {
            ushort num = 4;
            Buffer.BlockCopy(BitConverter.GetBytes(data), 0, buffer.Array, buffer.Offset + offset, num);
            return num;
        }
        public static ushort AppendLongData(long data, ArraySegment<byte> buffer, int offset)
        {
            ushort num = 8;
            Buffer.BlockCopy(BitConverter.GetBytes(data), 0, buffer.Array, buffer.Offset + offset, num);
            return num;
        }
        public static ushort AppendDoubleData(double data, ArraySegment<byte> buffer, int offset)
        {
            ushort num = 8;
            Buffer.BlockCopy(BitConverter.GetBytes(data), 0, buffer.Array, buffer.Offset + offset, num);
            return num;
        }
        public static ushort AppendStringData(string data, ArraySegment<byte> buffer, int offset)
        {
            var stringBytes = Encoding.Unicode.GetBytes(data);
            ushort length = (ushort)stringBytes.Length;

            Buffer.BlockCopy(BitConverter.GetBytes(length), 0, buffer.Array, buffer.Offset + offset, 2);
            Buffer.BlockCopy(stringBytes, 0, buffer.Array, buffer.Offset + offset + 2, length);

            return (ushort)(2 + length);
        }

    }
}
