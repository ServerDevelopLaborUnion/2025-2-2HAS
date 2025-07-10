using AgamaLibrary.Unity.Methods;
using System.Collections;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.Entities
{
	public class EntityInjector : MonoBehaviour
	{
		private EntityComponentRegistry _registry;

		private void Awake()
		{
			_registry = transform.ForceGetComponent<EntityComponentRegistry>();

			IEntityResolver[] resolvers = GetComponentsInChildren<IEntityResolver>();
			foreach (IEntityResolver resolver in resolvers)
				resolver.Initialize(_registry);
		}
	}
}
