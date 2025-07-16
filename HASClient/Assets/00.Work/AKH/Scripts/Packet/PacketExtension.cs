using UnityEngine;

namespace Assets._00.Work.AKH.Scripts.Packet
{
    public static class PacketExtension
    {
        public static VectorPacket ToPacket(this Vector3 vector)
            => new VectorPacket() { x = vector.x, y = vector.y, z = vector.z };
        public static Vector3 ToVector3(this VectorPacket packet)
            => new Vector3() { x = packet.x, y = packet.y, z = packet.z };
        public static Quaternion ToQuaternion(this QuaternionPacket packet)
            => new Quaternion(packet.x, packet.y, packet.z, packet.w);
        public static QuaternionPacket ToPacket(this Quaternion quaterion)
            => new QuaternionPacket() { z = quaterion.z, w = quaterion.w, x = quaterion.x, y = quaterion.y };
    }
}
