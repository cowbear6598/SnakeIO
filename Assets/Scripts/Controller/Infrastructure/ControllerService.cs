using Controller.Application.Handlers;
using Controller.Infrastructure.UI;
using UnityEngine;
using VContainer;

namespace Controller.Infrastructure
{
	public class ControllerService : IControllerService
	{
		[Inject] private MobileInput    _input;
		[Inject] private UI_Controller _uiController;

		public Vector2 GetMoveAxis() => _input.GetMoveAxis();

		public bool IsSprint() => _input.IsSprint();

		public void SetSprintButtonActive(bool isActive) => _uiController.SetSprintButtonActive(isActive);
	}
}