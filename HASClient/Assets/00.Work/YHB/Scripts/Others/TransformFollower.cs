using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.Others
{
	// 개선 방안이 안 떠올라 어짜피 나중에 관전화 등을 하려면 타겟을 바꾸고 돌아오기 등을 꼭해야함.
	public class TransformFollower : MonoBehaviour
	{
		[SerializeField] private Transform target;
		[SerializeField] private Vector3 offset;

		public void SetTarget(Transform newTarget)
		{
			target = newTarget;
		}

		private void Update()
		{
			if (target == null)
				return;
            transform.position = target.position + offset;
		}
	}
}
