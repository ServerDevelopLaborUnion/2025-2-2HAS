using System.Linq;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.ExecuteBehaviour.Composites
{
	[CreateAssetMenu(fileName = "Composite_Move", menuName = "SO/ScriptableBehaviour/Composite/Move", order = 0)]
	public class DefaultCompositeSO : CompositeBehaviourSO
	{
		public override bool CanExecuteNext<T>(T data)
		{
			return true;
		}

		protected override bool LogicExecute<T>(T data)
		{
			ExecuteBeforeBehaviour(data);
			return TryExecuteNext(data);
		}
	}
}
