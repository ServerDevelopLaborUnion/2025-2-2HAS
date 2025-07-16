using Assets._00.Work.YHB.Scripts.ExecuteBehaviour.DataTypes;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.ExecuteBehaviour.Behaviours.Moves
{
	[CreateAssetMenu(fileName = "RotateBehaviour", menuName = "SO/ScriptableBehaviour/Behaviour/Move/Rotation", order = 0)]
	public class RotationBehaviourSO : ScriptableBehaviourSO
	{
		protected override bool LogicExecute<T>(T data)
		{
			if (data is not CameraValueChangeData cameraValue)
				return false;

			Vector3 rotation = cameraValue.cameraParent.rotation.eulerAngles;
			rotation.x = 0;
			rotation.z = 0;

			if (rotation == Vector3.zero)
				return false;

			cameraValue.entityMovementComp.SetRotationDirection(Quaternion.Euler(rotation));
			return true;
		}
	}
}
