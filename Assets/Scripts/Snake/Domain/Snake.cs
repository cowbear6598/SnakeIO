using System;
using Controller;
using Snake.Domain.Adapters;
using UniRx;
using UnityEngine;
using VContainer;

namespace Snake.Domain
{
	public class Snake : ISnake
	{
		[Inject] private readonly IControllerService _controllerService;

		private readonly ReactiveProperty<int> _score = new(0);

		private float _sprintTime;

		public void Sprint(float deltaTime)
		{
			_sprintTime += deltaTime;

			if (_sprintTime < 0.2f)
				return;

			_sprintTime = 0;
			ChangeScore(-1);
		}

		public void ChangeScore(int score)
		{
			_score.Value = Mathf.Clamp(_score.Value + score, 0, int.MaxValue);

			_controllerService.SetSprintButtonActive(_score.Value != 0);
		}

		#region Public Methods

		public IDisposable SubscribeOnScoreChanged(Action<int> callback) => _score.Subscribe(callback);

		public bool IsSprint() => _controllerService.IsSprint() && _score.Value > 0;

		#endregion
	}
}