using Assets._00.Work.YHB.Scripts.ExecuteBehaviour.DataTypes;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.ExecuteBehaviour.Behaviours.Moves
{
	[CreateAssetMenu(fileName = "MoveBehaviour", menuName = "SO/ScriptableBehaviour/Behaviour/Move/Move", order = 0)]
	public class MoveBehaviourSO : ScriptableBehaviourSO
	{
		protected override bool LogicExecute<T>(T data)
		{
			if (data is not EntityMovementData movementData)
				return false;

			Quaternion rotation = Quaternion.Euler(0, movementData.moveRotation.eulerAngles.y, 0);
			movementData.entityMovement.SetMovementDirection(movementData.moveDirection, rotation);
			return true;
		}
	}
}
