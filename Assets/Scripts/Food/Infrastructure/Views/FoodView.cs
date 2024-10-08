using Food.Domain;
using Food.Infrastructure.Factory;
using Snake.Application.Adapters;
using Snake.Application.Result;
using Snake.Domain;
using UnityEngine;
using VContainer;

namespace Food.Infrastructure.Views
{
	public class FoodView : MonoBehaviour, IEatable
	{
		[Inject] private readonly FoodFactory _factory;

		[SerializeField] private SpriteRenderer _render;

		[SerializeField] private Color[] _colors;
		[SerializeField] private int[]   _scores;

		private FoodType _foodType;

		private int _instanceID;

		private void Start()
		{
			_instanceID = gameObject.GetInstanceID();
		}

		public void Spawn(Vector2 position, FoodType foodType)
		{
			_foodType = foodType;

			_render.color = _colors[(int)_foodType];

			transform.localScale = Vector3.one * (0.25f * ((int)_foodType + 1));

			transform.position = position;
		}

		public EatResult Eat()
		{
			var result = new EatResult(
				EatableType.Food,
				_instanceID,
				_scores[(int)_foodType]
			);

			_factory.RecycleFood(this);

			return result;
		}
	}
}