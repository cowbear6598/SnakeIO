using UnityEngine;

namespace Snake.Infrastructure.Views
{
	public abstract class SnakePart : MonoBehaviour
	{
		protected int _instanceID;

		private void Awake()
		{
			_instanceID = gameObject.GetInstanceID();
		}
	}
}