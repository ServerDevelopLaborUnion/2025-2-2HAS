using System.Linq;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.ExecuteBehaviour.Composites
{
	[CreateAssetMenu(fileName = "MoveComposite", menuName = "SO/ScriptableBehaviour/Composite/Move", order = 0)]
	public class MoveCompositeSO : CompositeBehaviourSO
	{
		public override bool CanExecuteNext<T>(T data)
		{
			return true;
		}

		protected override void DebugExecute<T>(T data, bool logicResult)
		{
			base.DebugExecute(data, logicResult);

			Debug.Log($"{this.name} : data type = {data.GetType().ToString().Split('.').Last()} / Logic Success : {logicResult}");
		}
	}
}
