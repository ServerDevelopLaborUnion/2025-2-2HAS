using System.Collections.Generic;

namespace AgamaLibrary.Comperers
{
    public class EqualsComarerSbtractNull<T> : IEqualityComparer<T>
    {
        public static EqualsComarerSbtractNull<T> Default;

        public bool Equals(T x, T y)
        {
            return !(x is null || y is null) && x.Equals(y); // 둘중 하나가 null이면 false
        }

        public int GetHashCode(T obj)
            => obj.GetHashCode();
    }
}
