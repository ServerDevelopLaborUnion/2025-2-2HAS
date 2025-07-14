using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets._00.Work.YHB.Scripts.Core
{
	[CreateAssetMenu(fileName = "InputSO", menuName = "SO/Input", order = 0)]
	public class InputSO : ScriptableObject, InputControlls.IPlayerActions
	{
		private InputControlls _controlls;

		private void OnEnable()
		{
			if (_controlls == null)
			{
				_controlls = new InputControlls();
				_controlls.Player.SetCallbacks(this);
			}
			_controlls.Player.Enable();
		}

		private void OnDisable()
		{
			_controlls?.Player.Disable();
		}

		#region Player Input

		public event Action<bool> OnAttackKStatusChangeEvent;
		public event Action<bool> OnJumpStatusChangeEvent;
		public event Action<bool> OnSpreintStatusChangeEvent;

		public event Action<Vector2> OnMoveKeyPressedEvent;
		public event Action<Vector2> OnMoveValueChangedEvent;
		public event Action<Vector2> OnLookChangedEvent;

		public Vector2 MovementDirection { get; private set; }

		public void OnAttack(InputAction.CallbackContext context)
		{
			if (context.performed)
				OnAttackKStatusChangeEvent?.Invoke(true);

			if (context.canceled)
				OnAttackKStatusChangeEvent?.Invoke(false);
		}

		public void OnJump(InputAction.CallbackContext context)
		{
			if (context.performed)
				OnJumpStatusChangeEvent?.Invoke(true);

			if (context.canceled)
				OnJumpStatusChangeEvent?.Invoke(false);
		}

		public void OnLook(InputAction.CallbackContext context)
		{
			Vector2 lookInputVector = context.ReadValue<Vector2>();
			if (context.performed)
				OnLookChangedEvent?.Invoke(lookInputVector);
		}

		public void OnMove(InputAction.CallbackContext context)
		{
			Vector2 movementInputVector = context.ReadValue<Vector2>();

			if (context.performed)
				OnMoveKeyPressedEvent?.Invoke(movementInputVector);

			MovementDirection = movementInputVector;
			OnMoveValueChangedEvent?.Invoke(MovementDirection);
		}

		public void OnSprint(InputAction.CallbackContext context)
		{
			if (context.performed)
				OnSpreintStatusChangeEvent?.Invoke(true);

			if (context.canceled)
				OnSpreintStatusChangeEvent?.Invoke(false);
		}

		#endregion
	}
}
