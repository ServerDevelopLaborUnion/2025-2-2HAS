using Assets._00.Work.YHB.Scripts.ExecuteBehaviour.DataTypes;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.ExecuteBehaviour.Behaviours.Moves
{
	[CreateAssetMenu(fileName = "RotateBehaviour", menuName = "SO/ScriptableBehaviour/Behaviour/Move/Rotation", order = 0)]
	public class RotationBehaviourSO : ScriptableBehaviourSO
	{
		public override bool Execute<T>(T data)
		{
			if (data is not EntityMovementData movementData)
				return false;

			movementData.entityMovement.SetRotationDirection(movementData.moveDirection);
			return true;
		}
	}
}
