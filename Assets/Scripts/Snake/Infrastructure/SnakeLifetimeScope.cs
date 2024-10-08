using Snake.Application.Handlers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Snake.Infrastructure
{
	public class SnakeLifetimeScope : LifetimeScope
	{
		[SerializeField] private SnakeMoveHandler.Settings       _moveSettings;
		[SerializeField] private SnakeBodyStoreHandler.Settings  _bodyStoreSettings;
		[SerializeField] private SnakeBodyFollowHandler.Settings _bodyFollowSettings;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterInstance(_moveSettings);
			builder.RegisterInstance(_bodyStoreSettings);
			builder.RegisterInstance(_bodyFollowSettings);

			builder.Register<Snake.Domain.Snake>(Lifetime.Scoped)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<SnakeMoveHandler>(Lifetime.Scoped)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<SnakeBodyStoreHandler>(Lifetime.Scoped)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<SnakeBodyFollowHandler>(Lifetime.Scoped)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<SnakeEatHandler>(Lifetime.Scoped)
			       .AsImplementedInterfaces()
			       .AsSelf();

			builder.Register<SnakeUIHandler>(Lifetime.Scoped)
			       .AsImplementedInterfaces()
			       .AsSelf();
		}
	}
}