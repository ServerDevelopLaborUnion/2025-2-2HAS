using System;
using System.Collections.Generic;

namespace AgamaLibrary.Comperers
{
    // old
    /*public class GreaterComparer<T> : IComparer<T>
    {
        private readonly Func<T, int> _keySelector; // 비교할 값 저장
        
        public GreaterComparer(Func<T, int> keySelector) // 매개변수 T인 int가져옴
        {
            _keySelector = keySelector ?? throw new ArgumentNullException(nameof(keySelector)); // null이면 오류출력, 아니면 저장
        }

        public int Compare(T x, T y)
        {
            if (x is null || y is null)
                throw new ArgumentNullException("입력된 인자중 하나가 null입니다."); // 마찬가지

            return _keySelector(x).CompareTo(_keySelector(y));
        }
    }*/

    public class GreaterComparer<T> : IComparer<T> where T : notnull, IComparable<T>
    {
        public static GreaterComparer<T> Default;

        public int Compare(T x, T y)
        {
            return x.CompareTo(y);
        }
    }
}
