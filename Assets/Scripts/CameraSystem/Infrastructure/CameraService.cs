using CameraSystem.Application.Handlers;
using UnityEngine;
using VContainer;

namespace CameraSystem.Infrastructure
{
	public class CameraService : ICameraService
	{
		[Inject] private readonly CameraFollowHandler _followHandler;

		public void SetFollower(Transform target) => _followHandler.SetFollower(target);
	}
}