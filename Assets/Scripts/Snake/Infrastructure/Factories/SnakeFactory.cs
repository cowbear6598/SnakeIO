using System;
using Snake.Application.Adapters;
using Snake.Infrastructure.Views;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Snake.Infrastructure.Factories
{
	public class SnakeFactory : IInitializable, ITickable, ISnakeFactory
	{
		[Inject] private readonly Settings        _settings;
		[Inject] private readonly IObjectResolver _resolver;

		private IObjectPool<SnakePart> _bodyPool;
		private Transform              _bodyGroup;

		public void Initialize()
		{
			_bodyGroup = new GameObject("UnusedSnakeBodies").transform;

			_bodyPool = new ObjectPool<SnakePart>(
				OnCreateSnakeBody,
				OnReuseSnakeBody,
				OnReleaseSnakeBody,
				OnDestroySnakeBody,
				true, 100
			);

			var tempBody = new SnakePart[100];

			for (var i = 0; i < 100; i++)
			{
				var body = _bodyPool.Get();
				tempBody[i] = body;
			}

			foreach (var bodyPart in tempBody)
			{
				_bodyPool.Release(bodyPart);
			}
		}

		// TODO: DELETE
		public void Tick()
		{
			if (Keyboard.current.aKey.wasPressedThisFrame)
			{
				_resolver.Instantiate(_settings.SnakePrefab);
			}
		}

		private SnakePart OnCreateSnakeBody()
		{
			var bodyPart = Object.Instantiate(_settings.BodyPartPrefab, _bodyGroup);
			bodyPart.gameObject.SetActive(false);

			return bodyPart;
		}

		private void OnReuseSnakeBody(SnakePart bodyPart)
		{
			bodyPart.gameObject.SetActive(true);
		}

		private void OnReleaseSnakeBody(SnakePart bodyPart)
		{
			bodyPart.gameObject.SetActive(false);
			bodyPart.transform.SetParent(_bodyGroup);
		}

		private void OnDestroySnakeBody(SnakePart bodyPart)
		{
			Object.Destroy(bodyPart.gameObject);
		}

		public SnakePart SpawnBody(Vector2 position, Transform parent)
		{
			var body = _bodyPool.Get();

			body.transform.position = position;
			body.transform.SetParent(parent);

			return body;
		}

		public void RecycleBody(SnakePart body) => _bodyPool.Release(body);

		[Serializable]
		public class Settings
		{
			public GameObject    SnakePrefab;
			public SnakeBodyView BodyPartPrefab;
		}
	}
}