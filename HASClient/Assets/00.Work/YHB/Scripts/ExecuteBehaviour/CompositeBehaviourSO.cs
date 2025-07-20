using System.Collections.Generic;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.ExecuteBehaviour
{
	[CreateAssetMenu(fileName = "Composite_BehaviourSO", menuName = "SO/ScriptableBehaviour/Composite", order = 0)]
	public abstract class CompositeBehaviourSO : ScriptableBehaviourSO
	{
		[Header("Behaviour")]
		public List<ScriptableBehaviourSO> executeBeforeBehaviourList;
		public List<ScriptableBehaviourSO> nextBehaviourList;
		public bool HaveNextBehaviour => nextBehaviourList.Count > 0;

		public abstract bool CanExecuteNext<T>(T data);

		protected void ExecuteBeforeBehaviour<T>(T data)
		{
			foreach (ScriptableBehaviourSO behaviour in executeBeforeBehaviourList)
				behaviour.Execute(data);
		}

		protected bool GetReturnValueFromBeforeBehaviour<T>(T data)
		{
			bool returnValue = true;
			foreach (ScriptableBehaviourSO behaviour in executeBeforeBehaviourList)
				returnValue = returnValue && (behaviour.Execute(data) == true);

			return returnValue;
		}

		/// <summary>
		/// 다음에 실행 시킬 Behaviour실행
		/// </summary>
		protected bool TryExecuteNext<T>(T data)
		{
			bool canExecute = CanExecuteNext<T>(data);
			bool returnValue = true;

			if (canExecute && HaveNextBehaviour)
				foreach (ScriptableBehaviourSO behaviour in nextBehaviourList)
					returnValue = returnValue && (behaviour.Execute(data) == true);

			return returnValue;
		}
	}
}
