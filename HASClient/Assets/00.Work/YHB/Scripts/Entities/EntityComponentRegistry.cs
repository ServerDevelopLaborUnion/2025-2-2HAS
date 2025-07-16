using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.Entities
{
	public class EntityComponentRegistry : MonoBehaviour
	{
		private Dictionary<Type, Component> _components = new Dictionary<Type, Component>();

		public T ResolveComponent<T>(bool throwIfMissing = false, bool findInChildren = true) where T : Component
		{
			_components.TryGetValue(typeof(T), out Component firstFound);
			T foundComponent = firstFound as T ?? GetAssignableFromComponent<T>();

			if (foundComponent != null)
				return foundComponent;

			if (findInChildren)
				foundComponent = GetComponentInChildren<T>();
			else
				foundComponent = GetComponent<T>();

			if (foundComponent == null)
				if (throwIfMissing)
					throw new MissingComponentException($"couldn't find {typeof(T)}component");
				else
					return null;

			_components[typeof(T)] = foundComponent;
			return foundComponent;
		}

		public P ResolveComponent<P, C>(C comp) where P : Component
												where C : P
		{
			P foundComp = GetAssignableFromComponent<P>();

			if (foundComp == null)
			{
				_components[typeof(P)] = comp;
				_components[comp.GetType()] = comp;
				foundComp = comp;
			}

			return foundComp;
		}

		public T ResolveComponent<T>(T comp, bool force = true) where T : Component
		{
			T foundComp = GetAssignableFromComponent<T>();
			if (foundComp == null || force)
			{
				_components[typeof(T)] = comp;
				return comp;
			}
			return foundComp;
		}

		private T GetAssignableFromComponent<T>() where T : Component
			=> _components.Values.FirstOrDefault(comp => typeof(T).IsAssignableFrom(comp.GetType())) as T;
	}
}
