using Snake.Application.Adapters;
using Snake.Application.Result;
using Snake.Domain;

namespace Snake.Infrastructure.Views
{
	public class SnakeBodyView : SnakePart, IEatable
	{
		public EatResult Eat()
		{
			var result = new EatResult(
				EatableType.SnakeBody,
				_instanceID,
				-1
			);

			return result;
		}
	}
}