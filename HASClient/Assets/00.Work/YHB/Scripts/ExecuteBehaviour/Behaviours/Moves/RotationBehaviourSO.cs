using Assets._00.Work.YHB.Scripts.ExecuteBehaviour.DataTypes;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.ExecuteBehaviour.Behaviours.Moves
{
	[CreateAssetMenu(fileName = "RotateBehaviour", menuName = "SO/ScriptableBehaviour/Behaviour/Move/Rotation", order = 0)]
	public class RotationBehaviourSO : ScriptableBehaviourSO
	{
		protected override bool LogicExecute<T>(T data)
		{
			if (data is not EntityMovementData movementData)
				return false;

			if (movementData.moveDirection == Vector2.zero)
				return false;

			Vector3 rotation = movementData.moveRotation.eulerAngles;
			rotation.x = 0;
			rotation.z = 0;
			movementData.entityMovement.SetRotationDirection(movementData.moveDirection, Quaternion.Euler(rotation));
			return true;
		}
	}
}
