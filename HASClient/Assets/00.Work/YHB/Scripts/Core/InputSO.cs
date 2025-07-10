using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		public Vector2 MovementDirection { get; private set; }
		public Vector2 LookDirection { get; private set; }

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
			LookDirection = lookInputVector;
		}

		public void OnMove(InputAction.CallbackContext context)
		{
			Vector2 movementInputVector = context.ReadValue<Vector2>();
			MovementDirection = movementInputVector;
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
