using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
