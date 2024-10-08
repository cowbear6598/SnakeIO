using System;
using Controller;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Snake.Application.Handlers
{
	public class SnakeMoveHandler : ITickable
	{
		[Inject] private readonly Settings           _settings;
		[Inject] private readonly Domain.Snake       _snake;
		[Inject] private readonly IControllerService _controller;

		private float _currentAngle;

		public void Tick()
		{
			var transform = _settings.Transform;
			var moveAxis  = _controller.GetMoveAxis();

			var targetAngle = moveAxis.magnitude > 0.1f
				? Mathf.Atan2(moveAxis.y, moveAxis.x) * Mathf.Rad2Deg
				: _currentAngle;

			var speedMultiplier = 1f;

			if (_snake.IsSprint())
			{
				speedMultiplier = 1.5f;
				_snake.Sprint(Time.deltaTime);
			}

			_currentAngle = Mathf.MoveTowardsAngle(_currentAngle, targetAngle, _settings.RotateSpeed * Time.deltaTime * speedMultiplier);

			var moveDirection = new Vector2(Mathf.Cos(_currentAngle * Mathf.Deg2Rad), Mathf.Sin(_currentAngle * Mathf.Deg2Rad));

			Vector2 currentPosition = transform.localPosition;
			var     newPosition     = currentPosition + moveDirection * (_settings.MoveSpeed * Time.deltaTime * speedMultiplier);

			transform.SetLocalPositionAndRotation(newPosition, Quaternion.Euler(0, 0, _currentAngle));
		}

		[Serializable]
		public class Settings
		{
			public Transform Transform;

			public float MoveSpeed   = 5;
			public float RotateSpeed = 180;
		}
	}
}