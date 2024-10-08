using Snake.Domain;

namespace Snake.Application.Result
{
	public struct EatResult
	{
		public readonly EatableType Type;
		public readonly int         InstanceID;
		public readonly int         Score;

		public EatResult(EatableType type, int instanceID, int score)
		{
			Type       = type;
			Score      = score;
			InstanceID = instanceID;
		}
	}
}