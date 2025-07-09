using UnityEngine;
using static Unity.VisualScripting.AnnotationUtility;

namespace Assets._00.Work.YHB.Scripts.Entities
{
	public class EntityMovement : MonoBehaviour, IEntityResolver
	{
		[SerializeField] private float moveSpeed = 5; // 스탯 기반일 필요가 없을 듯
		[SerializeField] private float gravity = -9.8f;

		private CharacterController _characterControllerComp;

		public bool CanMovement { get; set; } = true;
		private bool _canRotation = true;
		public bool CanRotation
		{
			get => _canRotation;
			private set => _canRotation = value;
		}

		public bool IsGround => _characterControllerComp.isGrounded;
		private Vector3 _velocity;
		public Vector3 Velocity => _velocity;

		private Vector3 _movementDirection;
		private Vector3 _lookTargetRotation;

		private float _verticalVelocity;

		public void Initialize(EntityComponentRegistry registry)
		{
			_characterControllerComp = registry.ResolveComponent<CharacterController>();
			Debug.Assert(_characterControllerComp != null, $"{typeof(CharacterController)} can not be found.");
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

		public void SetRotationDirection(Vector2 direction)
		{
			_lookTargetRotation = new Vector3(direction.x, _movementDirection.y, direction.y).normalized;
			CanRotation = true;
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
				_velocity *= moveSpeed * Time.fixedDeltaTime;
			}
		}

		private void LookAtRotation()
		{
			if (CanRotation)
			{
				float rotationSpeed = 8f;
				Quaternion targetRotation = Quaternion.LookRotation(_lookTargetRotation);
				_characterControllerComp.transform.rotation = Quaternion.Lerp(_characterControllerComp.transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
			}
		}

		private void ApplyGravity()
		{
			if (IsGround && _verticalVelocity < 0)
				_verticalVelocity = -0.03f;
			else
				_verticalVelocity += gravity * Time.fixedDeltaTime;

			float velocitySpeed = 8f;
			_velocity.y = Mathf.Lerp(_velocity.y, _verticalVelocity, velocitySpeed * Time.fixedDeltaTime);
		}

		private void Move()
		{
			_characterControllerComp.Move(_velocity);
		}

		public void StopImmediately()
		{
			_movementDirection = Vector3.zero;
			StopRotation();
		}
	}
}
