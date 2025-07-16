using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.Entities.GroundCheckers
{
	public class BoxGroundChecker : GroundChecker
	{
		[SerializeField] private Vector3 boxSize;

		public override bool CheckGround()
		{
			return 0 < Physics.OverlapBox(transform.position, boxSize / 2, Quaternion.identity, groundLayer).Length;
		}

		protected override void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(transform.position, boxSize);
		}
	}
}
