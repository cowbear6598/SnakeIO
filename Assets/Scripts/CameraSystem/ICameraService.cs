using UnityEngine;

namespace CameraSystem
{
	public interface ICameraService
	{
		void SetFollower(Transform target);
	}
}