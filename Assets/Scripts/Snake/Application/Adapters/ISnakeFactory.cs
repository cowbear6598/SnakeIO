using Snake.Infrastructure.Views;
using UnityEngine;

namespace Snake.Application.Adapters
{
	public interface ISnakeFactory
	{
		SnakePart SpawnBody(Vector2 spawnPosition, Transform parent);

		void RecycleBody(SnakePart part);
	}
}