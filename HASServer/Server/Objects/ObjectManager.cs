using Server.Utiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Server.Objects
{
    internal class ObjectManager
    {
        private Dictionary<int, ObjectBase> _objects = new();
        private int _objectIdGenerator = 0;

        public T GetObject<T>(int id) where T : ObjectBase
        {
            return _objects.GetValueOrDefault(id) as T;
        }
        public IEnumerable<T> GetObjects<T>() where T : ObjectBase
        {
            return _objects.Values.OfType<T>();
        }
        public void AddObject(ObjectBase obj)
        {
            _objects.Add(++_objectIdGenerator, obj);
            Console.WriteLine($"add:{_objectIdGenerator}");
            obj.index = _objectIdGenerator;
        }
        public void RemoveObject(int index)
        {
            _objects.Remove(index);
            //Broadcast(new S_RemoveObject() { index = index });
        }
    }
}
