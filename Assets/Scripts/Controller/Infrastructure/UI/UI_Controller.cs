using UnityEngine;
using UnityEngine.UI;

namespace Controller.Infrastructure.UI
{
	public class UI_Controller : MonoBehaviour
	{
		[SerializeField] private Button _sprintBtn;

		public void SetSprintButtonActive(bool isActive) => _sprintBtn.interactable = isActive;
	}
}