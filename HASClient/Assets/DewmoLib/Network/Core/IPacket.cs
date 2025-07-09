using System;

namespace ServerCore
{
    public interface IPacket
    {
        ushort Protocol { get; }
        ArraySegment<byte> Serialize();
        void Deserialize(ArraySegment<byte> segment);
    }
    public interface IDataPacket
    {
        ushort Serialize(ArraySegment<byte> segment, int offset);
        ushort Deserialize(ArraySegment<byte> segment, int offset);
    }
}
