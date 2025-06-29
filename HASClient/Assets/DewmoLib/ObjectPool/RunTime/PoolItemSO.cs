using UnityEngine;

namespace DewmoLib.ObjectPool.RunTime
{
    [CreateAssetMenu(fileName = "PoolItemSO", menuName = "SO/Pool/Item", order = 0)]
    public class PoolItemSO : ScriptableObject
    {
        public string poolingName;
        public GameObject prefab;
        public int initCount;
    }
}