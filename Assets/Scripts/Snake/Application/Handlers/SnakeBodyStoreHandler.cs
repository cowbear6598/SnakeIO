using System;
using System.Collections.Generic;
using System.Linq;
using Snake.Application.Adapters;
using Snake.Domain.Adapters;
using Snake.Infrastructure.Views;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Snake.Application.Handlers
{
	public class SnakeBodyStoreHandler : IInitializable, IDisposable, ITickable
	{
		[Inject] private readonly Settings      _settings;
		[Inject] private readonly ISnakeFactory _factory;
		[Inject] private readonly ISnake        _snake;

		private Transform _bodyParent;

		private readonly Dictionary<int, SnakePart> _bodies = new();

		private int _currentBodyCount;

		private IDisposable _subscription;

		private LayerMask _bodyLayer;
		private LayerMask _ignoreLayer;

		public void Initialize()
		{
			_subscription = _snake.SubscribeOnScoreChanged(OnScoreChanged);

			_bodyParent = new GameObject("SnakeBodies").transform;

			_bodies.Add(_settings.SnakeHead.gameObject.GetInstanceID(), _settings.SnakeHead);

			_bodyLayer   = LayerMask.NameToLayer("SnakeBody");
			_ignoreLayer = LayerMask.NameToLayer("SnakeIgnore");

			for (var i = 0; i < _settings.DefaultBodyParts; i++)
				Grow(i == 0);
		}

		public void Dispose()
		{
			_subscription.Dispose();
		}

		private void OnScoreChanged(int score)
		{
			var newBodyCount = score / 10;

			if (_currentBodyCount == newBodyCount)
				return;

			if (_currentBodyCount < newBodyCount)
				Grow();
			else if (_currentBodyCount > newBodyCount)
				Shrink();

			_currentBodyCount = newBodyCount;
		}

		private void Grow(bool ignore = false)
		{
			var lastBodyTrans = _bodies.Last().Value.transform;
			var lastPosition  = lastBodyTrans.position;
			var direction     = lastBodyTrans.right;

			var spawnPosition = lastPosition - direction * _settings.BodySpace;

			var snakeBody = _factory.SpawnBody(spawnPosition, _bodyParent);
			snakeBody.gameObject.layer = ignore ? _ignoreLayer : _bodyLayer;

			_bodies.Add(snakeBody.gameObject.GetInstanceID(), snakeBody);
		}

		private void Shrink()
		{
			if (_bodies.Count <= 1)
				return;

			var lastBody = _bodies.Last();
			_bodies.Remove(lastBody.Key);
			_factory.RecycleBody(lastBody.Value);
		}

		public IList<SnakePart> GetBodies()    => _bodies.Values.ToList();
		public float            GetBodySpace() => _settings.BodySpace;

		[Serializable]
		public class Settings
		{
			public SnakeHeadView SnakeHead;
			public int           DefaultBodyParts = 6;
			public float         BodySpace        = 0.5f;
		}

		// TODO: DELETE
		public void Tick()
		{
			if (Keyboard.current.wKey.wasReleasedThisFrame)
			{
				_snake.ChangeScore(10);
			}
		}

		public bool HasBody(int instanceID) => _bodies.ContainsKey(instanceID);
	}
}