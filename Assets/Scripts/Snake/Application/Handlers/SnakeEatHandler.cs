using System;
using Snake.Application.Adapters;
using Snake.Application.Result;
using Snake.Domain;
using Snake.Domain.Adapters;
using UnityEngine;
using VContainer;

namespace Snake.Application.Handlers
{
	public class SnakeEatHandler : ISnakeEatService
	{
		[Inject] private readonly ISnake                _snake;
		[Inject] private readonly SnakeBodyStoreHandler _bodyStoreHandler;

		public void Eat(EatResult result)
		{
			switch (result.Type)
			{
				case EatableType.Food:
					EatFood(result);
					break;
				case EatableType.SnakeBody:
					EatSnakeBody(result);
					break;
				case EatableType.SnakeHead:
					break;
				case EatableType.Wall:
					break;
			}
		}
		private void EatSnakeBody(EatResult result)
		{
			var isMine = _bodyStoreHandler.HasBody(result.InstanceID);

			if (isMine)
			{
				Debug.Log("吃到自己的身體");
				_snake.ChangeScore(result.Score);
			}
			else
			{
				Debug.Log("吃到別人的身體");
			}
		}

		private void EatFood(EatResult result)
		{
			Debug.Log($"吃食物 - {result.Score}");
			_snake.ChangeScore(result.Score);
		}
	}
}