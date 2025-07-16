using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.Entities.GroundCheckers
{
	public abstract class GroundChecker : MonoBehaviour
	{
		[SerializeField] protected LayerMask groundLayer;
		public abstract bool CheckGround();
		protected abstract void OnDrawGizmosSelected();
	}
}
