using System;

namespace ServerCore
{
    public class Singleton<T> where T : new()
    {
        private static Lazy<T> _instance = new Lazy<T>(() => new T());

        public static T Instance => _instance.Value;
    }
}
