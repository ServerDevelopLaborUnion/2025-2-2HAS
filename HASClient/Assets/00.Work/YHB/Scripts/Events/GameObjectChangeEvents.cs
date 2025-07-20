using Assets._00.Work.YHB.Scripts.Entities;
using DewmoLib.Utiles;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.Events
{
	public class GameObjectChangeEvents
	{
		public static RotateEvent RotateEvent = new RotateEvent();
		public static MoveSpeedChangeEvent MoveSpeedChangeEvent = new MoveSpeedChangeEvent();
	}

	public class RotateEvent : GameEvent
	{
		public EntityMovement entityMovement;
		public Quaternion rotateValue;
	}

	// 상호참조라 에바이긴 한테 우회하면 필요이상으로 복잡해 질듯 하여 상호참조로 놔둠.
	public class MoveSpeedChangeEvent : GameEvent
	{
		public EntityMovement entityMovement;
		public float previousMoveSpeed;
		public float newMoveSpeed;

		public MoveSpeedChangeEvent Initialize(EntityMovement movement, float previousValue, float newVlaue)
		{
			this.entityMovement = movement;
			this.previousMoveSpeed = previousValue;
			this.newMoveSpeed = newVlaue;
			return this;
		}
	}
}
