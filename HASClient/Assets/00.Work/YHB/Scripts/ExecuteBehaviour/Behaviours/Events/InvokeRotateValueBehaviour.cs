using Assets._00.Work.YHB.Scripts.Events;
using Assets._00.Work.YHB.Scripts.ExecuteBehaviour.DataTypes;
using DewmoLib.Utiles;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.ExecuteBehaviour.Behaviours.Events
{
	[CreateAssetMenu(fileName = "InvokeRotateValueBehaviour", menuName = "SO/ScriptableBehaviour/Behaviour/Event/InveokeRotateValue", order = 0)]
	public class InvokeRotateValueBehaviour : ScriptableBehaviourSO
	{
		[SerializeField] private EventChannelSO gameEvent;

		protected override bool LogicExecute<T>(T data)
		{
			if (data is not RotateValueData rotateData)
				return false;

			GameObjectChangeEvents.RotateEvent.entityMovement = rotateData.entityMovement;
			GameObjectChangeEvents.RotateEvent.rotateValue = rotateData.rotateValue;

			gameEvent.InvokeEvent(GameObjectChangeEvents.RotateEvent);
			return true;
		}
	}
}
