using Assets._00.Work.YHB.Scripts.Core;
using Assets._00.Work.YHB.Scripts.Entities;
using Assets._00.Work.YHB.Scripts.ExecuteBehaviour;
using Assets._00.Work.YHB.Scripts.ExecuteBehaviour.DataTypes;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Assets._00.Work.YHB.Scripts.Executors
{
	public class CameraInputExecutor : MonoBehaviour
	{
		[Header("Value")]
		[SerializeField] private InputSO inputSO;
		[SerializeField] private ScriptableBehaviourSO cameraRotateInputBehaviour;

		[Header("Set")]
		[SerializeField] private EntityMovement entityMovementComp;
		[SerializeField] private Transform cameraParent;
		[SerializeField] private float rotationSensitivity = 1f;
		[SerializeField] private float rotationXMin;
		[SerializeField] private float rotationXMax;

		private CameraValueChangeData cameraData;

		private void Awake()
		{
			inputSO.OnLookChangedEvent += HandleLookChangedEvent;

			cameraData = new CameraValueChangeData();
			cameraData.entityMovementComp = entityMovementComp;
			cameraData.cameraParent = cameraParent;
			cameraData.rotationXMin = rotationXMin;
			cameraData.rotationXMax = rotationXMax;
		}

		private void OnDestroy()
		{
			inputSO.OnLookChangedEvent -= HandleLookChangedEvent;
		}

		private void HandleLookChangedEvent(Vector2 vector)
		{
			cameraData.cameraRotateValue.x = -vector.y;
			cameraData.cameraRotateValue.y = vector.x;

			cameraData.cameraRotateValue *= rotationSensitivity;

			cameraRotateInputBehaviour.Execute<CameraValueChangeData>(cameraData);
		}
	}
}
