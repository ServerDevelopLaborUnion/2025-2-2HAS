using System.Numerics;

namespace Server.Utiles
{
    internal static class VectorExtension
    {
        public static Vector3 ToVector3(this VectorPacket packet)
            => new Vector3(x: packet.x, y:packet.y,z:packet.z);
        public static VectorPacket ToPacket(this Vector3 vector)
            => new VectorPacket() { x = vector.X, y = vector.Y, z = vector.Z };
    }
    internal static class QuaternionExtension
    {
        public static Quaternion ToQuaternion(this QuaternionPacket packet)
            => new Quaternion(x: packet.x, y: packet.y, z: packet.z, w: packet.w);
        public static QuaternionPacket ToPacket(this Quaternion quater)
            => new QuaternionPacket() { x = quater.X, y = quater.Y, z = quater.Z, w = quater.W };
    }
}
