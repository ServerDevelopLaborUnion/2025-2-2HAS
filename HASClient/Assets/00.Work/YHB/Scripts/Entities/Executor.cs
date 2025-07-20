using Assets._00.Work.YHB.Scripts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.Executors
{
	public abstract class Executor : MonoBehaviour, IIntialize
	{
		public bool IsInitialize { get; private set; }

		public virtual void Initialize()
		{
			IsInitialize = true;
		}

		public virtual void Release()
		{
			IsInitialize = false;
		}
	}
}
