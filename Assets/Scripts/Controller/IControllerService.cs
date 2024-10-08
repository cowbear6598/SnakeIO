using UnityEngine;

namespace Controller
{
	public interface IControllerService
	{
		Vector2 GetMoveAxis();

		bool IsSprint();

		void SetSprintButtonActive(bool isActive);
	}
}