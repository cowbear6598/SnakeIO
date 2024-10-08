using Snake.Application.Result;

namespace Snake.Application.Adapters
{
	public interface ISnakeEatService
	{
		void Eat(EatResult result);
	}
}