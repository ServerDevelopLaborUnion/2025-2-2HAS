using Assets._00.Work.YHB.Scripts.ExecuteBehaviour.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.ExecuteBehaviour.Behaviours.Moves
{
	[CreateAssetMenu(fileName = "JumpBehaviour", menuName = "SO/ScriptableBehaviour/Behaviour/Move/Jump", order = 0)]
	public class JumpBehaviourSO : ScriptableBehaviourSO
	{
		protected override bool LogicExecute<T>(T data)
		{
			if (data is not EntityMovementData movementData)
				return false;
			
			return movementData.entityMovement.Jump();
		}
	}
}
