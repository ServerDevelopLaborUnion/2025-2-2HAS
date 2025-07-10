using AgamaLibrary.Unity.Methods;
using System;
using UnityEngine;

namespace AgamaLibrary.Unity.Creationals
{
    public class GameObjectBuilder
    {
        private GameObject targetGameObject;
        //private Dictionary<string, Action<GameObjectBuilder>> presets = new Dictionary<string, Action<GameObjectBuilder>>();
        //private readonly string presetDefaultKey;

        public GameObjectBuilder(GameObject obj = null)//, string presetDefult = "Defult")
        {
            targetGameObject = obj ?? new GameObject();
            //presetDefaultKey = presetDefult;
        }

        public GameObjectBuilder StartBuild(GameObject obj = null)
        {
            targetGameObject = obj ?? new GameObject();
            return this;
        }

        public GameObjectBuilder SetName(string name)
        {
            UnityEngine.Debug.Assert(!string.IsNullOrEmpty(name), $"GameObject name can not be null or empty. name : {name}");
            targetGameObject.name = name;
            return this;
        }

        public GameObjectBuilder SetStatic(bool isStatic)
        {
            targetGameObject.isStatic = isStatic;
            return this;
        }

        public GameObjectBuilder SetTag(string tag)
        {
            UnityEngine.Debug.Assert(!string.IsNullOrEmpty(tag), $"GameObject tag can not be null or empty. tag : {tag}");
            targetGameObject.tag = tag;
            return this;
        }

        public GameObjectBuilder SetLayer(int layer)
        {
            targetGameObject.layer = layer;
            return this;
        }

        public GameObjectBuilder SetParent(Transform parent)
        {
            targetGameObject.transform.parent = parent;
            return this;
        }

        public GameObjectBuilder SetPosition(Vector3 position)
        {
            targetGameObject.transform.position = position;
            return this;
        }

        public GameObjectBuilder SetLocalPosition(Vector3 position)
        {
            targetGameObject.transform.localPosition = position;
            return this;
        }

        public GameObjectBuilder SetRotation(Vector3 rotation)
        {
            targetGameObject.transform.rotation = Quaternion.Euler(rotation);
            return this;
        }

        public GameObjectBuilder SetScale(float scale)
            => SetScale(Vector3.one * scale);

        public GameObjectBuilder SetScale(Vector3 scale)
        {
            targetGameObject.transform.localScale = scale;
            return this;
        }

        public GameObjectBuilder SetActive(bool active)
        {
            targetGameObject.SetActive(active);
            return this;
        }

        /// <summary>
        /// 조건에 따라 설정 또는 대체 설정을 실행합니다.
        /// </summary>
        /// <param name="condition">설정을 실행할 조건입니다.</param>
        /// <param name="setting">조건이 true일 경우 실행할 설정입니다.</param>
        /// <param name="elseSetting">조건이 false일 경우 실행할 설정입니다.</param>
        /// <returns>빌더 인스턴스</returns>
        public GameObjectBuilder If(Func<bool> condition, Action<GameObjectBuilder> setting, Action<GameObjectBuilder> elseSetting = null)
        {
            bool shouldApplySetting = condition?.Invoke() ?? false;

            if (shouldApplySetting)
                setting?.Invoke(this);
            else
                elseSetting?.Invoke(this);

            return this;
        }

        /// <summary>
        /// 지정한 횟수만큼 설정을 반복 실행합니다.
        /// </summary>
        /// <param name="loopCount">반복할 횟수입니다.</param>
        /// <param name="setting">반복마다 실행할 설정입니다. 현재 인덱스를 포함합니다.</param>
        /// <param name="startNumber">반복을 시작할 인덱스입니다. 기본값은 0입니다.</param>
        /// <returns>빌더 인스턴스</returns>
        public GameObjectBuilder For(int loopCount, Action<GameObjectBuilder, int> setting, int startNumber = 0)
        {
            for (int i = startNumber; i < startNumber + loopCount; ++i)
                setting?.Invoke(this, i);

            return this;
        }

        /// <summary>
        /// 조건이 참인 동안 설정을 반복 실행합니다.
        /// </summary>
        /// <param name="condition">반복을 계속할 조건입니다.</param>
        /// <param name="setting">반복마다 실행할 설정입니다.</param>
        /// <returns>빌더 인스턴스</returns>
        public GameObjectBuilder While(Func<bool> condition, Action<GameObjectBuilder> setting)
        {
            while (condition?.Invoke() ?? false)
                setting?.Invoke(this);

            return this;
        }

        /// <summary>
        /// 예외 처리를 포함한 안전한 작업을 수행합니다.
        /// </summary>
        /// <param name="tryAction">시도할 작업입니다.</param>
        /// <param name="catchAction">예외 발생 시 실행할 작업입니다. 예외 객체를 인자로 전달받습니다.</param>
        /// <returns>빌더 인스턴스</returns>
        public GameObjectBuilder TryCatch(Action<GameObjectBuilder> tryAction, Action<GameObjectBuilder, Exception> catchAction = null)
        {
            try
            {
                tryAction?.Invoke(this);
            }
            catch (Exception ex)
            {
                catchAction?.Invoke(this, ex);
            }

            return this;
        }

        /// <summary>
        /// 대상 GameObject에 동작을 실행합니다.
        /// </summary>
        /// <param name="action">GameObject에 적용할 동작입니다.</param>
        /// <returns>빌더 인스턴스</returns>
        public GameObjectBuilder ActObject(Action<GameObject> action)
        {
            action?.Invoke(targetGameObject);

            return this;
        }

        /// <summary>
        /// GameObject에 컴포넌트를 추가합니다.
        /// </summary>
        /// <typeparam name="T">추가할 컴포넌트의 타입</typeparam>
        /// <param name="force">추가할 컴포넌트가 있어도 추가할지에 대한 여부입니다.</param>
        /// <returns>빌더 인스턴스</returns>
        public GameObjectBuilder TryAddComponent<T>(bool force = false) where T : Component
        {
            if (force || !targetGameObject.TryGetComponent(out T component))
                targetGameObject.AddComponent<T>();
            return this;
        }

        /// <summary>
        /// GameObject에 컴포넌트를 추가합니다.
        /// </summary>
        /// <typeparam name="T">추가할 컴포넌트의 타입</typeparam>
        /// <param name="force">추가할 컴포넌트가 있어도 추가할지에 대한 여부입니다.</param>
        /// <returns>빌더 인스턴스</returns>
        public GameObjectBuilder TryAddComponent<T, A>() where A : Component, T
        {
            targetGameObject.transform.TryGetOrAddComponent<T, A>();
            return this;
        }

        /// <summary>
        /// GameObject에 지정된 컴포넌트를 가져오고, 해당 컴포넌트로 필요한 설정을 합니다.
        /// </summary>
        /// <remarks>
        /// 만약 컴포넌트를 가져오지 못했다면, 오브젝트에 컴포넌트를 추가하고 설정합니다.
        /// </remarks>
        /// <typeparam name="T">가져올 컴포넌트의 타입</typeparam>
        /// <param name="func">가져온 컴포넌트에 대한 설정을 수행하는 함수</param>
        /// <returns>빌더 인스턴스</returns>
        public GameObjectBuilder SetComponent<T>(Action<T> func) where T : Component
        {
            func?.Invoke(targetGameObject.transform.ForceGetComponent<T>());
            return this;
        }

        /// <summary>
        /// GameObject에 지정된 컴포넌트를 가져오고, 해당 컴포넌트로 필요한 작업을 합니다.
        /// </summary>
        /// <remarks>
        /// 만약 컴포넌트를 가져오지 못했다면, 아무런 설정을 하지 않습니다.
        /// </remarks>
        /// <typeparam name="T"> 가져올 컴포넌트의 타입</typeparam>
        /// <param name="func">가져온 컴포넌트에 대한 설정을 수행하는 함수</param>
        /// <returns>빌더 인스턴스</returns>
        public GameObjectBuilder SetToGetComponent<T>(Action<T> func) where T : Component
        {
            if (targetGameObject.transform.TryGetComponent(out T component))
                func?.Invoke(component);
            return this;
        }

        /// <summary>
        /// 자식을 추가합니다.
        /// </summary>
        /// <param name="target">추가할 자식</param>
        /// <returns>빌드 인스턴스</returns>
        public GameObjectBuilder SetToChild(Transform target)
        {
            target.parent = targetGameObject.transform;
            return this;
        }

        /// <summary>
        /// 자식을 추가합니다.
        /// </summary>
        /// <param name="target">추가할 자식</param>
        /// <returns>빌드 인스턴스</returns>
        public GameObjectBuilder SetToChild(GameObject target)
        {
            return SetToChild(target.transform);
        }

        /*#region Presets

        /// <summary>
        /// 저장된 프리셋의 키 목록을 반환합니다.
        /// </summary>
        /// <returns>프리셋 키 목록</returns>
        public IEnumerable<string> GetPresetKeys()
        {
            return presets.Keys;
        }

        /// <summary>
        /// 기본 키를 사용하여 프리셋을 저장합니다.
        /// </summary>
        /// <param name="preset">저장할 프리셋</param>
        public void SavePreset(Action<GameObjectBuilder> preset)
        {
            presets[presetDefaultKey] = preset;
        }

        /// <summary>
        /// 지정된 키를 사용하여 프리셋을 저장합니다.
        /// </summary>
        /// <param name="key">프리셋을 저장할 키</param>
        /// <param name="preset">저장할 프리셋</param>
        public void SavePreset(string key, Action<GameObjectBuilder> preset)
        {
            presets[key] = preset;
        }

        /// <summary>
        /// 지정된 키의 프리셋을 삭제합니다.
        /// </summary>
        /// <param name="key">삭제할 프리셋의 키</param>
        /// <exception cref="KeyNotFoundException">키가 존재하지 않을 경우 발생</exception>
        public void RemovePreset(string key)
        {
            if (!presets.Remove(key))
                throw new KeyNotFoundException($"Preset can't found key '{key}'.");
        }

        /// <summary>
        /// 지정된 키에 해당하는 프리셋이 존재하는지 확인합니다.
        /// </summary>
        /// <param name="key">확인할 프리셋의 키</param>
        /// <returns>프리셋 존재 여부</returns>
        public bool HasPreset(string key)
        {
            return presets.ContainsKey(key);
        }

        /// <summary>
        /// 지정된 키의 프리셋을 적용합니다.
        /// </summary>
        /// <param name="key">적용할 프리셋의 키 (옵션)</param>
        /// <returns>빌더 인스턴스</returns>
        /// <exception cref="KeyNotFoundException">프리셋이 존재하지 않으면 발생</exception>
        public GameObjectBuilder ApplyPreset(string key = null)
        {
            key = key ?? presetDefaultKey;

            if (!presets.ContainsKey(key))
                throw new KeyNotFoundException($"Preset can't found key '{key}'.");
            presets[key](this);
            return this;
        }

#if UNITY_EDITOR
        public GameObjectBuilder DebugPresets()
        {
            foreach (var key in presets.Keys)
            {
                UnityEngine.Debug.Log($"Preset: {key}");
            }
            return this;
        }
#endif

        #endregion*/

#if UNITY_EDITOR
        public GameObjectBuilder Debug(object obj)
        {
            UnityEngine.Debug.Log(obj);
            return this;
        }

        /// <summary>
        /// 지정된 디버그 동작을 실행하고 Unity 콘솔에 출력합니다.
        /// </summary>
        /// <param name="func">실행할 디버그 동작</param>
        /// <returns>빌더 인스턴스</returns>
        public GameObjectBuilder Debug(Action func)
        {
            func();
            return this;
        }
#endif


        /// <summary>
        /// GameObject를 생성하고 선택적으로 빌더 상태를 리셋합니다.
        /// </summary>
        /// <param name="reset">true일 경우, 빌드를 완료한 후 빌더 상태를 새로운 GameObject로 리셋합니다.</param>
        /// <returns>생성된 GameObject.</returns>
        public GameObject Build(bool reset = false)
        {
            GameObject result = targetGameObject;

            if (reset)
                targetGameObject = new GameObject();

            return result;
        }
    }
}