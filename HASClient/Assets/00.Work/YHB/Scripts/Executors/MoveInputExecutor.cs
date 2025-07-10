using Assets._00.Work.YHB.Scripts.Core;
using Assets._00.Work.YHB.Scripts.Entities;
using Assets._00.Work.YHB.Scripts.ExecuteBehaviour;
using Assets._00.Work.YHB.Scripts.ExecuteBehaviour.DataTypes;
using System;
using System.Security.Principal;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.Executors
{
	public class MoveInputExecutor : MonoBehaviour
	{
		[Header("Value")]
		[SerializeField] private InputSO inputSO;
		[SerializeField] private ScriptableBehaviourSO moveInputBehaviour;

		[Header("Value")]
		[SerializeField] private Transform entityCamera;
		[SerializeField] private EntityMovement entityMovement;

		private EntityMovementData _moveData;

		private void Awake()
		{
			_moveData = new EntityMovementData();
			_moveData.entityMovement = entityMovement;

			inputSO.OnMoveValueChangedEvent += HandleMoveValueChangedEvent;
		}

		private void OnDestroy()
		{
			inputSO.OnMoveValueChangedEvent -= HandleMoveValueChangedEvent;
		}

		private void HandleMoveValueChangedEvent(Vector2 vector)
		{
			_moveData.entityRotation = entityCamera.transform.rotation;
			_moveData.moveDirection = inputSO.MovementDirection;
			moveInputBehaviour.Execute<EntityMovementData>(_moveData);
		}
	}
}
