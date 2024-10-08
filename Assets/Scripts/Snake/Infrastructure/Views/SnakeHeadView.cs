using CameraSystem;
using Snake.Application.Adapters;
using UnityEngine;
using VContainer;

namespace Snake.Infrastructure.Views
{
	public class SnakeHeadView : SnakePart
	{
		[Inject] private readonly ICameraService  _cameraService;
		[Inject] private readonly IObjectResolver _resolver;

		private ISnakeEatService _eatService;

		private void Awake()
		{
			_cameraService.SetFollower(transform);
		}

		private void Start()
		{
			_eatService = _resolver.Resolve<ISnakeEatService>();
		}

		private void Eat(GameObject other)
		{
			var eatable = other.GetComponent<IEatable>();

			if (eatable == null)
			{
				Debug.LogError("物件沒有掛上 IEatable");
				return;
			}

			var result = eatable.Eat();
			_eatService.Eat(result);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			Eat(other.gameObject);
		}

		private void OnCollisionStay2D(Collision2D other)
		{
			Eat(other.gameObject);
		}
	}
}