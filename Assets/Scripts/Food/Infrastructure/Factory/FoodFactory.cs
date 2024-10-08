using System;
using Food.Domain;
using Food.Infrastructure.Views;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Food.Infrastructure.Factory
{
	public class FoodFactory : IInitializable
	{
		[Inject] private readonly Settings        _settings;
		[Inject] private readonly IObjectResolver _container;

		private Transform _foodGroup;

		private IObjectPool<FoodView> _foodPool;

		public void Initialize()
		{
			_foodGroup = new GameObject("FoodGroup").transform;

			_foodPool = new ObjectPool<FoodView>(
				OnCreateFood,
				OnReuseFood,
				OnReleaseFood,
				OnDestroyFood,
				true,
				_settings.TotalFoodCount,
				_settings.TotalFoodCount
			);

			for (var i = 0; i < _settings.TotalFoodCount; i++)
			{
				SpawnFood();
			}
		}

		private void SpawnFood()
		{
			var food = _foodPool.Get();

			var position = new Vector2(
				UnityEngine.Random.Range(-20, 20),
				UnityEngine.Random.Range(-20, 20)
			);

			var foodType = (FoodType)UnityEngine.Random.Range(0, 3);

			food.Spawn(position, foodType);
		}

		public void RecycleFood(FoodView foodView)
		{
			_foodPool.Release(foodView);
			SpawnFood();
		}

		#region Food Pool

		private FoodView OnCreateFood()
		{
			var food = _container.Instantiate(_settings.FoodPrefab, _foodGroup);
			food.gameObject.SetActive(false);

			return food;
		}

		private void OnReuseFood(FoodView food)
		{
			food.gameObject.SetActive(true);
		}

		private void OnReleaseFood(FoodView food)
		{
			food.gameObject.SetActive(false);
		}

		private void OnDestroyFood(FoodView food)
		{
			Object.Destroy(food.gameObject);
		}

		#endregion

		[Serializable]
		public class Settings
		{
			public FoodView FoodPrefab;
			public int      TotalFoodCount = 50;
		}
	}
}