using UnityEngine;

namespace AgamaLibrary.Unity.Methods
{
    public static class LidraryUnityMethod
    {
        /// <summary>
        /// Transform에서 <typeparamref name="T"/> 타입의 컴포넌트를 가져오거나 없으면 추가합니다.
        /// </summary>
        /// <typeparam name="T">가져오거나 추가할 컴포넌트 타입입니다.</typeparam>
        /// <param name="transform">컴포넌트를 가져오거나 추가할 Transform입니다.</param>
        /// <returns>찾은 또는 새로 추가된 <typeparamref name="T"/> 타입의 컴포넌트입니다.</returns>
        public static T ForceGetComponent<T>(this Transform transform) where T : Component
            => transform.TryGetComponent(out T comp) ? comp : transform.gameObject.AddComponent<T>();

        /// <summary>
        /// Transform에서 <typeparamref name="T"/> 타입의 컴포넌트를 가져오거나 없으면 <typeparamref name="A"/> 타입의 컴포넌트를 추가합니다.
        /// </summary>
        /// <typeparam name="T">가져오거나 추가할 컴포넌트 타입입니다.</typeparam>
        /// <typeparam name="A"><typeparamref name="T"/>의 서브클래스 타입입니다.</typeparam>
        /// <param name="transform">컴포넌트를 가져오거나 추가할 Transform입니다.</param>
        /// <returns>찾은 <typeparamref name="T"/> 또는 새로 추가된 <typeparamref name="A"/>를 <typeparamref name="T"/> 타입으로 가져옵니다.</returns>

        public static T TryGetOrAddComponent<T, A>(this Transform transform)
            where A : Component, T
            => transform.TryGetComponent(out T comp) ? comp : transform.gameObject.AddComponent<A>();
    }
}