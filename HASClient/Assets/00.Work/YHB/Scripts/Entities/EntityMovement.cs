using Assets._00.Work.YHB.Scripts.Entities.GroundCheckers;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.Entities
{
	public class EntityMovement : MonoBehaviour, IEntityResolver
	{
		[SerializeField] private GroundChecker groundChecker;
		[SerializeField] private float moveSpeed = 5; // 스탯 기반일 필요가 없을 듯
		[SerializeField] private float jumpPower = 5;
		[SerializeField] private int maxJumpCount = 2;

		private Rigidbody _rigidComp;

		public bool CanMovement { get; set; } = true;
		private bool _canRotation = true;
		public bool CanRotation
		{
			get => _canRotation;
			private set => _canRotation = value;
		}

		public bool IsGround => groundChecker.CheckGround();
		private Vector3 _velocity;
		public Vector3 Velocity => _velocity;

		private Vector3 _movementDirection;
		private Vector3 _lookTargetRotation;

		private int _currentJumpCount;

		public void Initialize(EntityComponentRegistry registry)
		{
			_rigidComp = registry.ResolveComponent<Rigidbody>();
			Debug.Assert(_rigidComp != null, $"{typeof(Rigidbody)} can not be found.");
		}

		private void FixedUpdate()
		{
			CalculateMovement();
			ApplyGravity();

			LookAtRotation();
			Move();
		}

		/// <summary>
		/// 절대 좌표계 기준으로 움직일 방향을 설정합니다.
		/// </summary>
		public void SetMovementDirection(Vector2 direction)
		{
			_movementDirection = new Vector3(direction.x, _movementDirection.y, direction.y).normalized;
		}

		/// <summary>
		/// 절대 좌표계 기준으로 움직일 방향을 설정합니다.
		/// </summary>
		public void SetMovementDirection(Vector2 direction, Quaternion rotation)
		{
			_movementDirection = rotation * new Vector3(direction.x, _movementDirection.y, direction.y).normalized;
		}

		public void SetRotationDirection(Vector2 direction, Quaternion rotation)
		{
			_lookTargetRotation = rotation * new Vector3(direction.x, _movementDirection.y, direction.y).normalized;
			CanRotation = true;
		}

		public bool Jump()
		{
			// 21억번 눌러서 오버플로우내면 그건 솔직히 대단하니까 인정해주자.
			if (++_currentJumpCount >= maxJumpCount)
				return false;

			_rigidComp.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

			return true;
		}

		public void StopRotation()
		{
			CanRotation = false;
		}

		private void CalculateMovement()
		{
			if (CanMovement)
			{
				_velocity = _movementDirection; // Quaternion은 교환 법칙이 성립하지 않는다.
				_velocity *= moveSpeed;
			}
		}

		private void LookAtRotation()
		{
			if (CanRotation)
			{
				float rotationSpeed = 8f;
				Quaternion targetRotation = Quaternion.LookRotation(_lookTargetRotation);
				_rigidComp.transform.rotation = Quaternion.Lerp(_rigidComp.transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
			}
		}

		private void ApplyGravity()
		{
			if (IsGround)
				_currentJumpCount = 0;
		}

		private void Move()
		{
			_rigidComp.linearVelocity = _velocity;
		}

		public void StopImmediately()
		{
			_movementDirection = Vector3.zero;
			StopRotation();
		}
	}
}
