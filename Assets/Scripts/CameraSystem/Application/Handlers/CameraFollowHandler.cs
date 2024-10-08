using System;
using Cinemachine;
using UnityEngine;
using VContainer;

namespace CameraSystem.Application.Handlers
{
	public class CameraFollowHandler
	{
		[Inject] private readonly Settings _settings;

		public void SetFollower(Transform target) => _settings.VirtualCamera.Follow = target;

		[Serializable]
		public class Settings
		{
			public CinemachineVirtualCamera VirtualCamera;
		}
	}
}