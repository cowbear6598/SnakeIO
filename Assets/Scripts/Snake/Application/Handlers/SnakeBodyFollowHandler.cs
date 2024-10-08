using System;
using Snake.Domain.Adapters;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Snake.Application.Handlers
{
	public class SnakeBodyFollowHandler : ITickable
	{
		[Inject] private readonly Settings              _settings;
		[Inject] private readonly SnakeBodyStoreHandler _bodyStoreHandler;
		[Inject] private readonly ISnake                _snake;

		public void Tick()
		{
			var bodies = _bodyStoreHandler.GetBodies();

			var speedMultiplier = _snake.IsSprint() ? 1.5f : 1f;
			var lerpSpeed       = _settings.FollowSpeed * Time.deltaTime * speedMultiplier;

			for (var i = 1; i < bodies.Count; i++)
			{
				var prevPosition = bodies[i].transform.position;
				var newPosition  = bodies[i - 1].transform.position;

				var direction      = (newPosition - prevPosition).normalized;
				var targetPosition = newPosition - direction * _bodyStoreHandler.GetBodySpace();

				var position = Vector3.Lerp(prevPosition, targetPosition, lerpSpeed);
				var angle    = Mathf.Atan2(newPosition.y - prevPosition.y, newPosition.x - prevPosition.x) * Mathf.Rad2Deg;

				bodies[i].transform.SetPositionAndRotation(position, Quaternion.Euler(0, 0, angle));
			}
		}

		[Serializable]
		public class Settings
		{
			public float FollowSpeed = 5;
		}
	}
}