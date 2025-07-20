using Assets._00.Work.YHB.Scripts.ExecuteBehaviour.DataTypes;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.ExecuteBehaviour.Behaviours.Moves
{
	[CreateAssetMenu(fileName = "RotateBehaviour", menuName = "SO/ScriptableBehaviour/Behaviour/Move/Rotation", order = 0)]
	public class RotationBehaviourSO : ScriptableBehaviourSO
	{
		protected override bool LogicExecute<T>(T data)
		{
			if (data is not RotateValueData rotateValue)
				return false;

			rotateValue.entityMovement.SetRotationDirection(rotateValue.rotateValue);

			return true;
		}
	}
}
