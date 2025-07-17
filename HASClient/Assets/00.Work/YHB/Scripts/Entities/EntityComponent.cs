using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.Entities
{
	public class EntityComponent : MonoBehaviour, IEntityResolver
	{
		public bool IsInitialize { get; private set; }

		public virtual void Initialize(EntityComponentRegistry registry)
		{
			IsInitialize = true;
		}
	}
}
