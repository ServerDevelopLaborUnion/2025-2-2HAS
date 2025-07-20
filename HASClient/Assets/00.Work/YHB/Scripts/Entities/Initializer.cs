using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.Entities
{
	public class Initializer : MonoBehaviour
	{
		private void Awake()
		{
			IIntialize[] initializes = GetComponentsInChildren<IIntialize>();
			foreach (IIntialize initialize in initializes)
				initialize.Initialize();
		}
	}
}
