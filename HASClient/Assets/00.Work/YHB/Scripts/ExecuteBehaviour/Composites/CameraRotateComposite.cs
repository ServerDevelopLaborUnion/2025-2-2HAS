using Assets._00.Work.YHB.Scripts.ExecuteBehaviour.DataTypes;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.ExecuteBehaviour.Composites
{
	[CreateAssetMenu(fileName = "Composite_CameraRotate", menuName = "SO/ScriptableBehaviour/Composite/CameraRotate", order = 0)]

	public class CameraRotateComposite : CompositeBehaviourSO
	{
		private RotateValueData _rotatationValueData;
		private RotateValueData RotationValueData
		{
			get
			{
				if (_rotatationValueData == null)
					_rotatationValueData = new RotateValueData();
				return _rotatationValueData;
			}
			set => _rotatationValueData = value;
		}

		public override bool CanExecuteNext<T>(T data)
		{
			return true;
		}

		protected override bool LogicExecute<T>(T data)
		{
			ExecuteBeforeBehaviour(data);

			if (data is not CameraValueChangeData cameraValue)
				return false;
			Debug.Log("Data");

			Vector3 rotation = cameraValue.cameraParent.rotation.eulerAngles;
			rotation.x = 0;
			rotation.z = 0;

			if (rotation == Vector3.zero)
				return false;
			Debug.Log("0");

			RotationValueData.rotateValue = Quaternion.Euler(rotation);
			RotationValueData.entityMovement = cameraValue.entityMovementComp;

			return TryExecuteNext(RotationValueData);
		}
	}
}
