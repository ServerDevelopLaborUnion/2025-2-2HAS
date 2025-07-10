using Assets._00.Work.YHB.Scripts.Core;
using Assets._00.Work.YHB.Scripts.Entities;
using Assets._00.Work.YHB.Scripts.ExecuteBehaviour;
using Assets._00.Work.YHB.Scripts.ExecuteBehaviour.DataTypes;
using System.Security.Principal;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.Executors
{
	public class MoveInputExecutor : MonoBehaviour
	{
		[SerializeField] private InputSO inputSO;
		[SerializeField] private ScriptableBehaviourSO moveInputBehaviour;

		[Header("Value")]
		[SerializeField] private Entity entity;
		[SerializeField] private EntityMovement entityMovement;

		private EntityMovementData _moveData;

		private void Awake()
		{
			_moveData = new EntityMovementData();
			_moveData.entityMovement = entityMovement;
		}

		private void Update()
		{
			_moveData.entityRotation = entity.transform.rotation;
			_moveData.moveDirection = inputSO.MovementDirection;
			moveInputBehaviour.Execute<EntityMovementData>(_moveData);
		}
	}
}
