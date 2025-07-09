using Assets._00.Work.YHB.Scripts.ExecuteBehaviour.DataTypes;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.ExecuteBehaviour.Behaviours.Moves
{
	[CreateAssetMenu(fileName = "MoveBehaviour", menuName = "SO/ScriptableBehaviour/Behaviour/Move/Move", order = 0)]
	public class MoveBehaviourSO : ScriptableBehaviourSO
	{
		public override bool Execute<T>(T data)
		{
			if (data is not EntityMovementData movementData)
				return false;

			movementData.entityMovement.SetMovementDirection(movementData.moveDirection, movementData.entityRotation);
			return true;
		}
	}
}
