using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.ExecuteBehaviour
{
	[CreateAssetMenu(fileName = "BehaviourSO", menuName = "SO/ScriptableBehaviour/Conditional", order = 0)]
	public class ConditionalCompositeSO : ScriptableBehaviourSO
	{
		[Header("Scriptable Behaviour")]
		[SerializeField] private ScriptableBehaviourSO conditionBehaviour;
		[SerializeField] private List<ScriptableBehaviourSO> sucessBehaviourList;
		[SerializeField] private List<ScriptableBehaviourSO> failureBehaviourList;

		protected override bool LogicExecute<T>(T data)
		{
			bool success = conditionBehaviour.Execute(data);
			List<ScriptableBehaviourSO> loopBehaviourList;

			if (success)
				loopBehaviourList = sucessBehaviourList;
			else
				loopBehaviourList = failureBehaviourList;

			foreach (ScriptableBehaviourSO behaviour in loopBehaviourList)
				behaviour.Execute(data);

			return success;
		}
	}
}
