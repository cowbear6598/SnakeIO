using System;
using Snake.Domain.Adapters;
using TMPro;
using VContainer;
using VContainer.Unity;

namespace Snake.Application.Handlers
{
	public class SnakeUIHandler : IInitializable, IDisposable
	{
		[Inject] private readonly Settings _settings;
		[Inject] private readonly ISnake   _snake;

		private IDisposable _subscription;

		public void Initialize() => _subscription = _snake.SubscribeOnScoreChanged(OnScoreChanged);
		public void Dispose()    => _subscription.Dispose();

		private void OnScoreChanged(int score)
		{
			_settings.ScoreText.text      = $"分數: {score}";
			_settings.BodyLengthText.text = $"長度: {score / 10}";
		}

		[Serializable]
		public class Settings
		{
			public TextMeshProUGUI ScoreText;
			public TextMeshProUGUI BodyLengthText;
		}
	}
}