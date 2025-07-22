using Assets._00.Work.YHB.Scripts.Events;
using Assets._00.Work.YHB.Scripts.ExecuteBehaviour.DataTypes;
using DewmoLib.Utiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.ExecuteBehaviour.Behaviours.Events
{
	[CreateAssetMenu(fileName = "InveokeMoveDirectionValueBehaviour", menuName = "SO/ScriptableBehaviour/Behaviour/Event/InveokeMoveDirectionValue", order = 0)]
	public class InvokeMoveDirectionBehaviourSO : ScriptableBehaviourSO
	{
		[SerializeField] private EventChannelSO gameEvent;

		protected override bool LogicExecute<T>(T data)
		{
			if (data is not EntityMovementData moveData)
				return false;

			GameObjectChangeEvents.MoveDirectionChangeEvent.entityMovement = moveData.entityMovement;
			GameObjectChangeEvents.MoveDirectionChangeEvent.inputDirection = moveData.moveDirection;
			GameObjectChangeEvents.MoveDirectionChangeEvent.rotation = moveData.moveRotation;

			gameEvent.InvokeEvent(GameObjectChangeEvents.MoveDirectionChangeEvent);
			return true;
		}
	}
}
