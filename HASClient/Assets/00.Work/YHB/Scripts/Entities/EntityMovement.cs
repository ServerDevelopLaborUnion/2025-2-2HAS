using Assets._00.Work.YHB.Scripts.Entities.GroundCheckers;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.Entities
{
	public class EntityMovement : MonoBehaviour, IEntityResolver
	{
		[Header("move")]
		[SerializeField] private float moveSpeed = 5; // 스탯 기반일 필요가 없을 듯

		[Header("jump")]
		[SerializeField] private GroundChecker groundChecker;
		[SerializeField] private float gravityScale = -9.8f;

		[Header("jump")]
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
		private Quaternion _lookTargetRotation;

		private int _currentJumpCount;
		private float _verticalVelocity;

		public void Initialize(EntityComponentRegistry registry)
		{
			_rigidComp = registry.ResolveComponent<Rigidbody>();
			Debug.Assert(_rigidComp != null, $"{typeof(Rigidbody)} can not be found.");
			_rigidComp.useGravity = false;
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
			_movementDirection = new Vector3(direction.x, 0, direction.y).normalized;
			_movementDirection.y = _verticalVelocity;
		}

		/// <summary>
		/// 절대 좌표계 기준으로 움직일 방향을 설정합니다.
		/// </summary>
		public void SetMovementDirection(Vector2 direction, Quaternion rotation)
		{
			_movementDirection = rotation * new Vector3(direction.x, 0, direction.y).normalized;
			_movementDirection.y = _verticalVelocity;
		}

		public void SetRotationDirection(Quaternion rotation)
		{
			_lookTargetRotation = rotation;
			CanRotation = true;
		}

		public bool Jump()
		{
			// 21억번 눌러서 오버플로우내면 그건 솔직히 대단하니까 인정해주자.
			if (++_currentJumpCount >= maxJumpCount)
				return false;

			_verticalVelocity = jumpPower;

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

			if (IsGround)
				_currentJumpCount = 0;

			_velocity.y = _verticalVelocity;
		}

		private void LookAtRotation()
		{
			if (CanRotation)
			{
				float rotationSpeed = 8f;
				_rigidComp.transform.rotation = Quaternion.Lerp(_rigidComp.transform.rotation, _lookTargetRotation, rotationSpeed * Time.fixedDeltaTime);
			}
		}

		private void Move()
		{
			_rigidComp.linearVelocity = _velocity;
		}

		private void ApplyGravity()
		{
			if (IsGround && _verticalVelocity < 0)
				_verticalVelocity = -0.03f;
			else
				_verticalVelocity += gravityScale * Time.fixedDeltaTime;
		}

		public void StopImmediately()
		{
			_movementDirection = Vector3.zero;
			_velocity = Vector3.zero;
			StopRotation();
		}
	}
}
