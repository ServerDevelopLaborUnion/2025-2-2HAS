using System.Collections.Generic;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.ExecuteBehaviour
{
	[CreateAssetMenu(fileName = "BehaviourCompositeSO", menuName = "SO/ScriptableBehaviour/Composite", order = 0)]
	public abstract class CompositeBehaviourSO : ScriptableBehaviourSO
	{
		[Header("Behaviour")]
		public List<ScriptableBehaviourSO> executeBeforeBehaviourList;
		public List<ScriptableBehaviourSO> nextBehaviourList;
		public bool HaveNextBehaviour => nextBehaviourList.Count > 0;

		public abstract bool CanExecuteNext<T>(T data);
		public override bool Execute<T>(T data)
		{
			Debug.Log($"{this.name} : Execute");

			foreach (ScriptableBehaviourSO behaviour in executeBeforeBehaviourList)
				behaviour.Execute(data);

			return TryExecuteNext<T>(data);
		}

		/// <summary>
		/// 다음에 실행 시킬 Behaviour실행
		/// </summary>
		protected bool TryExecuteNext<T>(T data)
		{
			Debug.Log($"{this.name} : Try Execute Next Behaviour");

			bool canExecute = CanExecuteNext<T>(data);
			if (canExecute && HaveNextBehaviour)
				foreach (ScriptableBehaviourSO behaviour in nextBehaviourList)
					behaviour.Execute(data);

			Debug.Log($"{this.name} : End / Execute{(canExecute ? "" : " not")} Next Behaviour");

			return canExecute;
		}
	}
}
