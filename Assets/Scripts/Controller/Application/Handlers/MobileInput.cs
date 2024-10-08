using System;
using UnityEngine;
using VContainer.Unity;

namespace Controller.Application.Handlers
{
	public class MobileInput : IInitializable, IDisposable
	{
		private readonly Controls controls = new();

		public void Initialize() => controls.Enable();
		public void Dispose()    => controls.Disable();

		public Vector2 GetMoveAxis() => controls.Player.Move.ReadValue<Vector2>();

		public bool IsSprint() => Convert.ToBoolean(controls.Player.Sprint.ReadValue<float>());
	}
}