using Assets._00.Work.YHB.Scripts.ExecuteBehaviour.DataTypes;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.ExecuteBehaviour.Behaviours.Camera
{
	[CreateAssetMenu(fileName = "CameraRotationBehaviour", menuName = "SO/ScriptableBehaviour/Behaviour/Camera/Rotation", order = 0)]
	public class CameraRotateBehaviour : ScriptableBehaviourSO
	{
		protected override bool LogicExecute<T>(T data)
		{
			if (data is not CameraValueChangeData cameraData)
				return false;

			Vector3 cameraRotationValue = cameraData.cameraRotateValue;
			Vector3 cameraRotation = cameraData.cameraParent.rotation.eulerAngles + cameraRotationValue;
			float angle = Mathf.Repeat(cameraRotation.x + 180f, 360f) - 180f; // warp - around방지
			cameraRotation.x = Mathf.Clamp(angle, cameraData.rotationXMin, cameraData.rotationXMax);
			cameraRotation.z = 0;

			Quaternion rotation = Quaternion.Euler(cameraRotation);
			cameraData.cameraParent.rotation = rotation;

			return true;
		}
	}
}
