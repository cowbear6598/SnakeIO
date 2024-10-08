using System;

namespace Snake.Domain.Adapters
{
	public interface ISnake
	{
		bool IsSprint();

		void ChangeScore(int amount);

		IDisposable SubscribeOnScoreChanged(Action<int> callback);
	}
}