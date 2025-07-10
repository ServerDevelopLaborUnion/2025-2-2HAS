using System.Collections.Generic;
using System.Linq;

namespace AgamaLibrary.Methods
{
    public static class LidraryMethod
    {
        #region ICollection

        /// <summary>
        /// IColliction 타입에 value가 중복되는지 확인하고 추가합니다.
        /// </summary>
        /// <param name="value">추가할 값입니다.</param>
        /// <returns>추가에 성공하면 true, 실패하면 false를 반환합니다.</returns>
        public static bool TryAdd<T>(this ICollection<T> range, T value)
        {
            if (range.Contains(value))
                return false;

            range.Add(value);
            return true;
        }

        public static bool MargeTo<T>(this ICollection<T> margeTarget, ICollection<T> margeValue)
        {
            bool success = false;

            foreach (T item in margeValue)
            {
                if (item != null && !margeTarget.Contains(item))
                {
                    success = true;
                    margeTarget.Add(item);
                }
            }

            return success;
        }

        #endregion

        #region Dictionary

        /// <summary>
        /// 딕셔너리에 주어진 키와 값을 추가하거나, 키가 이미 존재할 경우 값을 업데이트합니다.
        /// </summary>
        /// <remarks>
        /// 키가 이미 존재하면 기존 값을 새 값으로 대체하며, 존재하지 않으면 새 키와 값을 추가합니다.
        /// </remarks>
        public static void AddOrUpdate<K, V>(this Dictionary<K, V> dict, K key, V value)
            => dict[key] = value;

        /// <summary>
        /// 주어진 값에 대한 첫 번째 키를 찾습니다.
        /// </summary>
        /// <param name="dict">값을 검색할 딕셔너리</param>
        /// <param name="val">찾고자 하는 값</param>
        /// <returns>
        /// 찾은 키를 반환합니다. 값이 존재하지 않을 경우, 기본값(<typeparamref name="K"/>)을 반환합니다.
        /// </returns>
        /// <remarks>
        /// 이 메서드는 값이 중복된 경우 첫 번째로 발견된 키를 반환합니다.
        /// </remarks>
        public static K FindFirstKeyByValue<K, V>(this Dictionary<K, V> dict, V val)
            => dict.FirstOrDefault(entry => EqualityComparer<V>.Default.Equals(entry.Value, val)).Key;

        /// <summary>
        /// 주어진 딕셔너리를 반전시킵니다. 각 키와 값을 교환하여 새로운 딕셔너리를 생성합니다.
        /// </summary>
        /// <param name="dict">반전시킬 원본 딕셔너리</param>
        /// <returns>키와 값이 반전된 새 딕셔너리</returns>
        /// <remarks>
        /// 원본 딕셔너리에서 값이 고유한 경우에만 유효하게 작동합니다.
        /// 값이 중복되면 마지막 값이 키로 저장됩니다.
        /// </remarks>
        public static Dictionary<V, K> Invert<K, V>(this Dictionary<K, V> dict)
            => dict.ToDictionary(valuePair => valuePair.Value, valuePair => valuePair.Key);

        /// <summary>
        /// 두 개의 딕셔너리를 병합합니다.
        /// </summary>
        /// <param name="target">병합될 대상 딕셔너리</param>
        /// <param name="adder">병합할 딕셔너리</param>
        /// <param name="firstTarget">
        /// true인 경우, 키가 중복될 때 대상 딕셔너리의 값을 유지합니다.
        /// false인 경우, 추가할 딕셔너리의 값으로 덮어씁니다.
        /// </param>
        public static void MergeTo<K, V>(this Dictionary<K, V> target, Dictionary<K, V> adder, bool firstTarget = true)
        {
            foreach (K kValue in adder.Keys)
                target[kValue] = firstTarget && target.ContainsKey(kValue) ? target[kValue] : adder[kValue];

            target.Where(pair => EqualityComparer<V>.Default.Equals(pair.Value, pair.Value)).Select(pair => pair.Key).ToList();
        }

        /// <summary>
        /// 지정된 값과 일치하는 값이 있는 모든 키-값을 딕셔너리에서 제거합니다.
        /// </summary>
        /// <param name="value">제거할 값입니다.</param>
        /// <remarks>
        /// 이 메서드는 지정된 값과 일치하는 키와 해당 키의 값을 딕셔너리에서 모두 제거합니다.
        /// </remarks>
        public static void DeleteByValue<K, V>(this Dictionary<K, V> dict, V value)
            => dict.Where(pair => EqualityComparer<V>.Default.Equals(pair.Value, value)).Select(pair => pair.Key).ToList().ForEach(key => dict.Remove(key));

        /// <summary>
        /// 딕셔너리에서 지정된 값과 일치하는 모든 값을 새 값으로 변경합니다.
        /// </summary>
        /// <param name="valueForChange">변경할 기존 값입니다.</param>
        /// <param name="value">새로 설정할 값입니다.</param>
        /// <remarks>
        /// 이 메서드는 딕셔너리에서 <paramref name="valueForChange"/>와 일치하는 값을 가진 항목을 찾아 해당 값을 <paramref name="value"/>로 업데이트합니다.
        /// </remarks>
        public static void ChangeValueTo<K, V>(this Dictionary<K, V> dict, V valueForChage, V value)
            => dict.Where(pair => EqualityComparer<V>.Default.Equals(pair.Value, valueForChage)).Select(pair => pair.Key).ToList().ForEach(key => dict[key] = value);

        /// <summary>
        /// 주어진 값과 일치하는 모든 키-값 쌍을 찾아 새로운 딕셔너리로 반환합니다.
        /// </summary>
        /// <param name="findValue">찾고자 하는 값</param>
        /// <returns>값이 일치하는 모든 키, 값을 포함하는 새로운 딕셔너리</returns>
        public static Dictionary<K, V> GetOverlapValue<K, V>(this Dictionary<K, V> dict, V findValue)
            => dict.Where(pair => EqualityComparer<V>.Default.Equals(pair.Value, findValue)).ToDictionary(pair => pair.Key, pair => pair.Value) ?? new Dictionary<K, V>();

        #endregion

        public static bool TryGetValue<T>(this HashSet<T> set, T equalValue, out T actualValue)
        {
            foreach (var item in set)
            {
                if (set.Comparer.Equals(item, equalValue))
                {
                    actualValue = item;
                    return true;
                }
            }
            actualValue = default!;
            return false;
        }
    }
}