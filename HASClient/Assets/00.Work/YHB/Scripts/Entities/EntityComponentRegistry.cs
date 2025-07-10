using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.Entities
{
	public class EntityComponentRegistry : MonoBehaviour
	{
		private Dictionary<Type, Component> _components = new Dictionary<Type, Component>();

		public T ResolveComponent<T>() where T : Component
		{
			if (_components.TryGetValue(typeof(T), out Component component))
				return component as T;

			T foundComponent = GetComponentInChildren<T>();

			// null이면 캐쉬된 컴포넌트에 추가 안 함.
			if  (foundComponent == null)
				return null;

			_components.Add(typeof(T), foundComponent);
			return foundComponent;
		}
	}
}
