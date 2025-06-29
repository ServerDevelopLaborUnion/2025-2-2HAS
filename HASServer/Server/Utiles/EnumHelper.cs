using System;

namespace System
{
    internal static class EnumHelper
    {
        public static T StringToEnum<T>(string name) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), name);
        }
    }
}
