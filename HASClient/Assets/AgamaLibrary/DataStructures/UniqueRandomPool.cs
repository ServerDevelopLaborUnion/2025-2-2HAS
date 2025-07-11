using System;
using System.Collections.Generic;
using System.Linq;

namespace AgamaLibrary.DataStructures
{
    /// <summary>
    /// 범위를 지정하고 그 범위 안에서 값을 중복없이 가져옵니다.
    /// </summary>
    public class UniqueRandomPool<T>
    {
        public event Action OnReset;

        private List<T> _range;
        private List<T> _values;

        private Random _random;

        private readonly bool _allowDuplicate;

        /// <summary>
        /// 초기화 시 기준이 될 범위의 수
        /// </summary>
        public int RangeCount
            => _range.Count;

        /// <summary>
        /// 초기회 되기 까지 남은 값들의 수
        /// </summary>
        public int ValueCount
            => _values.Count;

        public IEnumerable<T> Values
            => _values;

        public IEnumerable<T> Range
            => _range;

        public UniqueRandomPool(IEnumerable<T> range = null, bool allowDuplicate = false, Random random = null)
        {
            _range = new List<T>();
            _values = new List<T>();

            _random = random ?? new Random();

            _allowDuplicate = allowDuplicate;

            if (range != null)
            {
                _range.AddRange(range);
                _values.AddRange(range);
            }
        }

        /// <summary>
        /// 범위를 초기화하고 값을 설정합니다.
        /// </summary>
        /// <param name="withValue">값을 같이 초기화 시킬지 여부입니다. 단, Reset을 하지않고 설정합니다.</param>
        public void SetRange(T range, bool withValue = true)
        {
            ClearRange();
            _range.Add(range);

            if (withValue)
            {
                _values.Clear();
                _values.Add(range);
            }
        }

        /// <summary>
        /// 범위를 초기화하고 값을 설정합니다.
        /// </summary>
        /// <param name="withValue">값을 같이 초기화 시킬지 여부입니다. 단, Reset을 하지않고 설정합니다.</param>
        public void SetRange(IEnumerable<T> range, bool withValue = true)
        {
            if (range == null)
                return;

            ClearRange();
            if (_allowDuplicate)
                _range.AddRange(range);
            else
            {
                _range.AddRange(range);
                _range = _range.Distinct().ToList();
            }

            if (withValue)
            {
                _values.Clear();
                _values.AddRange(_range);
            }
        }

        /// <summary>
        /// 값(<typeparamref name="T"/>)을 범위에 추가합니다.
        /// </summary>
        /// <param name="value">추가할 값입니다.</param>
        /// <param name="withValue">값을 같이 추가 시킬지 여부입니다.</param>
        /// <remarks>
        /// 값을 추가할 수 없는 경우에 대한 예외 처리가 포함되어 있지 않습니다.
        /// </remarks>
        public bool AddRange(T range, bool withValue = true)
        {
            if (range == null)
                return false;

            if (_allowDuplicate || !_range.Contains(range))
            {
                _range.Add(range);

                if (withValue)
                    _values.Add(range);

                return true;
            }
            return false;
        }

        /// <summary>
        /// 지정된 값의 컬렉션을 범위에 추가합니다.
        /// </summary>
        /// <param name="value">추가할 값의 컬렉션입니다.</param>
        /// <param name="withValue">값을 같이 추가 시킬지 여부입니다.</param>
        /// <remarks>
        /// null 값에 대한 예외 처리가 포함되어 있지 않습니다.
        /// </remarks>
        public void AddRange(IEnumerable<T> range, bool withValue = true)
        {
            if (range == null)
                return;

            _range.AddRange(range);
            if (withValue)
                _values.AddRange(range);

            if (!_allowDuplicate)
            {
                _range = _range.Distinct().ToList();
                _values = _values.Distinct().ToList();
            }
        }

        /// <summary>
        /// 값(<typeparamref name="T"/>)을 후에 나올 값에 추가합니다.
        /// 추가된 값은 초기화 되지 않고 사라집니다.
        /// </summary>
        /// <param name="value">추가할 값입니다.</param>
        /// <remarks>
        /// 값을 추가할 수 없는 경우에 대한 예외 처리가 포함되어 있지 않습니다.
        /// </remarks>
        public void AddValue(T value)
        {
            if (value == null)
                return;

            _values.Add(value);
        }

        /// <summary>
        /// 지정된 값의 컬렉션을 후에 나올 값에 추가합니다.
        /// 추가된 값은 초기화 되지 않고 사라집니다.
        /// </summary>
        /// <param name="value">추가할 값의 컬렉션입니다.</param>
        /// <remarks>
        /// null 값에 대한 예외 처리가 포함되어 있지 않습니다.
        /// </remarks>
        public void AddValue(IEnumerable<T> value)
        {
            if (value == null)
                return;

            _values.AddRange(value);
        }

        /// <summary>
        /// 범위의 값(<typeparamref name="T"/>)을 삭제합니다.
        /// </summary>
        /// <param name="withValue">값을 같이 제거 시킬지 여부입니다.</param>
        public void RemoveRange(T value, bool withValue = true)
        {
            _range.Remove(value);

            if (withValue)
                _values.Remove(value);
        }

        /// <summary>
        /// 범위의 값(<typeparamref name="T"/>)을 삭제합니다.
        /// </summary>
        /// <param name="withValue">값을 같이 제거 시킬지 여부입니다.</param>
        public void RemoveRange(IEnumerable<T> value, bool withValue = true)
        {
            if (value == null)
                return;

            foreach (T item in value)
            {
                _range.Remove(item);
                if (withValue)
                    _values.Remove(item);
            }
        }

        /// <summary>
        /// 범위를 초기화합니다.
        /// </summary>
        public void ClearRange()
            => _range.Clear();

        /// <summary>
        /// 값의 범위를 초기화합니다.
        /// </summary>
        public void Reset()
        {
            _values.Clear();
            _values.AddRange(_range);
            OnReset?.Invoke();
        }

        /// <summary>
        /// 렌덤으로 범위 안의 T값을 가져옵니다.
        /// 한 범위가 다 지나기 전까지 같은 값을 가져오지 않습니다.
        /// </summary>
        /// <returns>범위가 비어 있을 시 기본값(<typeparamref name="T"/>)을 반환합니다.</returns>
        public T GetValue()
        {
            if (_values.Count <= 0)
                Reset();

            int randIndex = _random.Next(0, _values.Count);
            T value = _values[randIndex];
            _values.Remove(value);

            return value;
        }

        /// <summary>
        /// 범위 안의 값(<typeparamref name="T"/>)을 렌덤으로 가져옵니다.
        /// </summary>
        /// <returns>값(<typeparamref name="T"/>)을 반환합니다. 만약 범위에 아무것도 없다면 default(<typeparamref name="T"/>)를 반환합니다.</returns>
        public T GetRangeInValue()
        {
            if (_range.Count <= 0)
                return default;

            return _range[_random.Next(0, _range.Count)];
        }
    }
}
